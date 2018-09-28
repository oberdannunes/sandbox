using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteListArray {
	class Program {
		static void Main(string[] args) {
			int objectCount = 20 * 1000 * 1000;
			int alocStep = 1000;
			long before = 0;

			Console.WriteLine($"Obj count..: {objectCount:N0}");
			

			var sw = Stopwatch.StartNew();

			Console.WriteLine();
			GC.Collect(2, GCCollectionMode.Forced, true, true);
			before = GC.GetTotalMemory(true);
			sw.Restart();
			var resultList = TestList(objectCount, alocStep);
			Console.WriteLine($"List Obj......: {sw.Elapsed}");
			GC.Collect(2, GCCollectionMode.Forced, true, true);
			Console.WriteLine($"Memory........: {GC.GetTotalMemory(true) - before:N0}");

			Console.WriteLine();
			GC.Collect(2, GCCollectionMode.Forced, true, true);
			before = GC.GetTotalMemory(true);
			sw.Restart();
			var resultListArray = TesteListArray(objectCount, alocStep);
			Console.WriteLine($"ListArray Obj.: {sw.Elapsed}");
			GC.Collect(2, GCCollectionMode.Forced, true, true);
			Console.WriteLine($"Memory........: {GC.GetTotalMemory(true) - before:N0}");


			Console.WriteLine();
			GC.Collect(2, GCCollectionMode.Forced, true, true);
			before = GC.GetTotalMemory(true);
			sw.Restart();
			var resultListDate = TestListDate(objectCount, alocStep);
			Console.WriteLine($"List Date.....: {sw.Elapsed}");
			GC.Collect(2, GCCollectionMode.Forced, true, true);
			Console.WriteLine($"Memory........: {GC.GetTotalMemory(true) - before:N0}");

			Console.WriteLine();
			GC.Collect(2, GCCollectionMode.Forced, true, true);
			before = GC.GetTotalMemory(true);
			sw.Restart();
			var resultListArrayDate = TesteListArrayDate(objectCount, alocStep);
			Console.WriteLine($"ListArray Date: {sw.Elapsed}");
			GC.Collect(2, GCCollectionMode.Forced, true, true);
			Console.WriteLine($"Memory........: {GC.GetTotalMemory(true) - before:N0}");

			Console.WriteLine($@"Total :{
				resultList.Count + resultListArray.Count + resultListDate.Count + resultListArrayDate.Count
			}");

			//sw.Restart();
			//TesteArrayRealoc(objectCount, alocStep);
			//Console.WriteLine($"ArrayRealoc: {sw.Elapsed}");

			//sw.Restart();
			//TesteArrayResize(objectCount, alocStep);
			//Console.WriteLine($"ArrayResize: {sw.Elapsed}");



		}

		//private static void TesteArrayRealoc(int objectCount, int alocStep) {
		//	var array = new DateTime[alocStep];

		//	DateTime now = DateTime.Now;

		//	int arrayIndex = 0;
		//	int arrayLength = array.Length;
		//	for (int i = 0; i < objectCount; i++) {
		//		array[arrayIndex] = now.AddSeconds(i);

		//		if (arrayIndex + 1 == arrayLength) {
		//			var newArray = new DateTime[arrayLength + alocStep];
		//			Array.Copy(array, newArray, arrayLength);

		//			array = newArray;
		//			arrayLength = array.Length;
		//		}

		//		arrayIndex++;
		//	}
		//}

		//private static void TesteArrayResize(int objectCount, int alocStep) {
		//	var array = new DateTime[alocStep];

		//	DateTime now = DateTime.Now;

		//	int arrayIndex = 0;
		//	int arrayLength = array.Length;
		//	for (int i = 0; i < objectCount; i++) {
		//		array[arrayIndex] = now.AddSeconds(i);

		//		if (arrayIndex + 1 == arrayLength) {
		//			//var newArray = new DateTime[arrayLength + alocStep];
		//			Array.Resize(ref array, arrayLength + alocStep);
		//			arrayLength = array.Length;
		//		}

		//		arrayIndex++;
		//	}
		//}

		private static List<DateContainer[]> TesteListArray(int objectCount, int alocStep) {
			var array = new DateContainer[alocStep];
			var listArrays = new List<DateContainer[]>(10000);

			//var x =MemoryMappedFile.CreateNew("teste", 2 * 1000 * 1000 * 1000);
			//x.CreateViewAccessor().


			//https://www.infoq.com/articles/Big-Memory-Part-2
			//https://github.com/aumcode/nfx/tree/master/Source/NFX/ApplicationModel/Pile

			DateTime now = DateTime.Now;
			decimal decValue = 123.45m;

			int arrayIndex = 0;
			int arrayLength = array.Length;
			for (int i = 0; i < objectCount; i++) {
				array[arrayIndex] = new DateContainer(now.AddSeconds(i), now.AddSeconds(i + 1), decValue + i, i);

				if (arrayIndex + 1 == arrayLength) {
					listArrays.Add(array);
					array = new DateContainer[alocStep];
					arrayIndex = 0;
				} else {
					arrayIndex++;
				}
			}

			return listArrays;
		}

		private static List<DateTime[]> TesteListArrayDate(int objectCount, int alocStep) {
			var array = new DateTime[alocStep];
			var listArrays = new List<DateTime[]>(10000);

			DateTime now = DateTime.Now;

			int arrayIndex = 0;
			int arrayLength = array.Length;
			for (int i = 0; i < objectCount; i++) {
				array[arrayIndex] = now.AddSeconds(i);

				if (arrayIndex + 1 == arrayLength) {
					listArrays.Add(array);
					array = new DateTime[alocStep];
					arrayIndex = 0;
				} else {
					arrayIndex++;
				}
			}

			return listArrays;
		}

		private static List<DateContainer> TestList(int objectCount, int alocStep) {
			var list = new List<DateContainer>();

			DateTime now = DateTime.Now;
			decimal decValue = 123.45m;

			for (int i = 0; i < objectCount; i++) {
				list.Add(new DateContainer(now.AddSeconds(i), now.AddSeconds(i + 1), decValue + i, i));
			}

			return list;
		}

		private static List<DateTime> TestListDate(int objectCount, int alocStep) {
			var list = new List<DateTime>();

			DateTime now = DateTime.Now;
			decimal decValue = 123.45m;

			for (int i = 0; i < objectCount; i++) {
				list.Add(now.AddSeconds(i));
			}

			return list;
		}

		public struct DateContainer {
			public DateTime Date { get; private set; }
			public DateTime Date2 { get; private set; }
			public decimal DecValue { get; private set; }
			public Int64 Number { get; private set; }

			//public DateContainer[] Next { get; set; }

			public DateContainer(DateTime date, DateTime date2, decimal decValue, Int64 number) {
				this.Date = date;
				this.Date2 = date2;
				this.DecValue = decValue;
				this.Number = number;
			}
		}
	}
}
