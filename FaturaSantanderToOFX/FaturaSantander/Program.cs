using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FaturaSantander {
	class Program {
		static void Main(string[] args) {
			CultureInfo ptCi = new CultureInfo("pt-BR");


			if (args.Length < 1) {
				Console.WriteLine("Informe o arquivo de origem");
				return;
			}

			string source = File.ReadAllText(args[0], System.Text.Encoding.Default);

			var rxCartao = new Regex("<span class=\"textoDestaque\">@?(.*) - Cartão");
			var rxLancamentoData = new Regex("<td align='center' height=\"19\"><span class='texto'>([0-9/]+)</span>");
			var rxLancamentoDesc = new Regex("<td align='left'><span class='texto'>(.*)</span>");
			var rxLancamentoValor = new Regex("<td align='center'><span class='texto'>(.*)</span>");

			var matchCartao = rxCartao.Match(source);

			var cartoes = new List<CartaoInfo>();

			while (matchCartao.Success) {
				string cartao = matchCartao.Groups[1].Value;

				cartoes.Add(new CartaoInfo() {
					Cartao = cartao, Start = matchCartao.Index, Length = 0
				});

				matchCartao = matchCartao.NextMatch();
			}

			for (int i = 0, l = cartoes.Count; i < l; i++) {
				if (i < l - 1) {
					cartoes[i].Length = cartoes[i + 1].Start - cartoes[i].Start;
				} else {
					cartoes[i].Length = source.Length - cartoes[i].Start;
				}
			}

			foreach (var cartao in cartoes) {
				Console.WriteLine($"# Cartão: {cartao.Cartao, -20} {cartao.Start, 10} {cartao.Length,10}");

				var matchLancamentoData = rxLancamentoData.Match(source, cartao.Start, cartao.Length);

				while (matchLancamentoData.Success) {
					
					var data = DateTime.ParseExact(matchLancamentoData.Groups[1].Value, "dd/MM/yyyy", ptCi);

					var matchLancamentoDesc = rxLancamentoDesc.Match(source, matchLancamentoData.Index);
					string descricao = matchLancamentoDesc.Groups[1].Value;

					if (descricao != "PAGAMENTO DE FATURA") {
						var lancamento = new LancamentoInfo();
						cartao.Lancamentos.Add(lancamento);
						lancamento.Data = data;
						lancamento.Descricao = descricao;

						var matchLancamentoValor = rxLancamentoValor.Match(source, matchLancamentoDesc.Index);
						lancamento.Valor = decimal.Parse(matchLancamentoValor.Groups[1].Value, ptCi);

						Console.WriteLine($"\t{lancamento.Data:dd/MM/yyyy} - {lancamento.Descricao,-30} - {lancamento.Valor * -1,10:#,##0.00}");
					}


					matchLancamentoData = matchLancamentoData.NextMatch();
				}

				Console.WriteLine($"# Total cartão: {cartao.Lancamentos.Sum(l => l.Valor):#,##0.00}");

				Console.WriteLine();
			}

			Console.WriteLine($"#### Total fatura: {cartoes.Sum(c => c.Lancamentos.Sum(l => l.Valor)):#,##0.00}");

			//Cria os arquivos de saída
			var identificacoes = (from c in cartoes
									   select c.Cartao).Distinct();

			foreach (var item in identificacoes) {

				Spire.Xls.Workbook wb = new Spire.Xls.Workbook();
				wb.Worksheets.Remove(2);
				wb.Worksheets.Remove(1);

				var ws = wb.Worksheets[0];
				int row = 1;
				ws.Range[row, 1].Text = "Descrição";
				ws.Range[row, 2].Text = "Valor";
				ws.Range[row, 3].Text = "Data";
				ws.Range[row, 4].Text = "Cartão";

				row++;
				
				foreach (var cartao in cartoes.Where(c => c.Cartao == item)) {
					foreach (var lancamento in cartao.Lancamentos) {
						//writer.WriteLine($"{lancamento.Descricao};{(lancamento.Valor * -1).ToString(ptCi)};{lancamento.Data:dd/MM/yyyy}");

						//ws.SetCellValue(row, 1, lancamento.Descricao);
						//ws.SetCellValue(row, 2, $"{(lancamento.Valor * -1).ToString(ptCi)}");
						//ws.SetCellValue(row, 3, $"{lancamento.Data:dd/MM/yyyy}");

						ws.Range[row, 1].Text = lancamento.Descricao;
						ws.Range[row, 3].DateTimeValue = lancamento.Data;
						ws.Range[row, 2].NumberValue = (double)(lancamento.Valor * -1);
						ws.Range[row, 4].Text = cartao.Cartao;


						row++;

					}
				}

				wb.SaveToFile($"{item}.xls");
			}
		}

		public class CartaoInfo {
			public string Cartao { get; set; }
			public int Start { get; set; }
			public int Length { get; set; }

			public List<LancamentoInfo> Lancamentos { get; set; }

			public CartaoInfo() {
				Lancamentos = new List<LancamentoInfo>();
			}
		}

		public class LancamentoInfo {
			public DateTime Data { get; set; }
			public string Descricao { get; set; }
			public decimal Valor { get; set; }
		}
	}
}
