using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace TesteWS {
	/// <summary>
	/// Summary description for Servico1
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	public class Servico1 : System.Web.Services.WebService {

		[WebMethod]
		public string HelloWorld1() {
			return "Hello World - Serviço 1";
		}
	}
}
