using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TesteWS {
	public class Servico3 : IServico3 {

		public string HelloWorld3() {
			return "Hello World - Serviço 3";
		}
	}
}
