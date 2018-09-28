using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSClient {
	class Program {
		static void Main(string[] args) {
			//ServiceReference1.Servico1SoapClient cli1 = new ServiceReference1.Servico1SoapClient();
			//Console.WriteLine("Log serviço 1: " + cli1.HelloWorld1());

			//ServiceReference2.Servico2SoapClient cli2 = new ServiceReference2.Servico2SoapClient();
			//Console.WriteLine("Log serviço 2: " + cli2.HelloWorld2());

			WebReference1.Servico1 cli1 = new WebReference1.Servico1();
			Console.WriteLine("Log serviço 1: " + cli1.HelloWorld1());

			WebReference2.Servico2 cli2 = new WebReference2.Servico2();
			Console.WriteLine("Log serviço 2: " + cli2.HelloWorld2());

			ServiceReference3.Servico3Client cli3 = new ServiceReference3.Servico3Client();
			Console.WriteLine("Log serviço 3: " + cli3.HelloWorld3());
		}
	}
}
