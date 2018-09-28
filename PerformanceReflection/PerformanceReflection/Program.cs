using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using System.Collections;
using System.Reflection.Emit;
using System.Linq.Expressions;

namespace PerformanceReflection {
	class Program {
		private const int _qtdObjects = 1000 * 1000;

		static void Main(string[] args) {
			Console.WriteLine("Total de objetos: {0}", _qtdObjects.ToString("#,###"));
			Console.WriteLine();

			Stopwatch timer = new Stopwatch();

			//Mapeamento manual
			timer.Start();
			List<Pessoa> list1 = new List<Pessoa>();
			ManualMapping(list1);
			timer.Stop();
			TimeSpan manual = timer.Elapsed;
			Console.WriteLine(string.Format("                Mapeamento manual: {0}", manual));

			//Mapeamento com reflection
			timer.Reset();
			timer.Start();
			List<Pessoa> list2 = new List<Pessoa>();
			CachedReflectionMapping<Pessoa>(list2);
			timer.Stop();
			TimeSpan reflection = timer.Elapsed;
			Console.WriteLine(string.Format("Mapeamento reflection (com cache): {0}", reflection));

			Console.WriteLine(string.Format("                        Diferença: {0}", reflection - manual));

			Console.WriteLine();

		}

		private static void CachedReflectionMapping<T>(List<T> list) where T : class, new() {

			//***** Em cache, dicionário estático, por tipo da entidade
			PropertyInfo pID = typeof(T).GetProperty("idPessoa");
			PropertyInfo pNome = typeof(T).GetProperty("nome");
			PropertyInfo pIdade = typeof(T).GetProperty("idade");

			var accessorId = ReflectionHelper.BuildSetAccessor(pID.GetSetMethod());
			var accessorNome = ReflectionHelper.BuildSetAccessor(pNome.GetSetMethod());
			var accessorIdade = ReflectionHelper.BuildSetAccessor(pIdade.GetSetMethod());

			ReflectionHelper.ItemFactory<T> factory = new ReflectionHelper.ItemFactory<T>();
			//***** Em cache, dicionário estático, por tipo da entidade

			for (int i = 0; i < _qtdObjects; i++) {
				T entity = factory.GetNewItem();
				list.Add(entity);

				int id = i;
				string nome = string.Format("Pessoa {0}", i);
				int idade = i + 1;

				//id
				accessorId(entity, id);
				//nome
				accessorNome(entity, nome);
				//idade
				accessorIdade(entity, idade);
			}
		}

		private static void ManualMapping(List<Pessoa> list) {
			for (int i = 0; i < _qtdObjects; i++) {
				Pessoa entity = new Pessoa();
				list.Add(entity);

				int id = i;
				string nome = string.Format("Pessoa {0}", i);
				int idade = i + 1;

				//id
				entity.idPessoa = id;
				//nome
				entity.nome = nome;
				//idade
				entity.idade = idade;
			}
		}
	}

	public class Pessoa {
		public int idPessoa { get; set; }

		public string nome { get; set; }

		public int idade { get; set; }
	}

	public static class ReflectionHelper {
		public static Action<object, object> BuildSetAccessor(MethodInfo method) {
			var obj = Expression.Parameter(typeof(object), "o");
			var value = Expression.Parameter(typeof(object));

			Expression<Action<object, object>> expr =
				Expression.Lambda<Action<object, object>>(
					Expression.Call(
						Expression.Convert(obj, method.DeclaringType),
						method,
						Expression.Convert(value, method.GetParameters()[0].ParameterType)),
					obj,
					value);

			return expr.Compile();
		}

		public class ItemFactory<T> where T : new() {
			public T GetNewItem() {
				return new T();
			}
		}

	}
}
