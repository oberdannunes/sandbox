using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Authentication;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ExemploLdap {
    class Program {
        private const string C_ADServer = "adserver";
        private const string C_ADFilter = "OU=users,DC=domain,DC=com,DC=br";

        static void Main(string[] args) {
            string userName = @"domain\user";
            string nomeCompleto = null;

            //Verificação se o usuário existe
            bool usuarioExiste = PesquisaUsuarioAD(userName, out nomeCompleto);

            //Validação de usuário e senha
            string passWord = "123";
            bool dadosCorretos = ValidaUsuarioAD(userName, passWord);
        }

        private static bool PesquisaUsuarioAD(string userName, out string fullName) {
            fullName = null;
            var userNameParts = userName.Split('\\');

            if (userNameParts.Length > 1) {
                userName = userNameParts[1];
            }

            using (var domainContext = new PrincipalContext(ContextType.Domain, C_ADServer, C_ADFilter)) {
                using (var foundUser = UserPrincipal.FindByIdentity(domainContext, IdentityType.SamAccountName, userName)) {
                    if (foundUser != null) {
                        fullName = foundUser.Name;
                        return true;
                    } else {
                        return false;
                    }
                }
            }
        }

        private static bool ValidaUsuarioAD(string userName, string passWord) {
            var userNameParts = userName.Split('\\');

            if (userNameParts.Length > 1) {
                userName = userNameParts[1];
            }

            using (var domainContext = new PrincipalContext(ContextType.Domain, C_ADServer, C_ADFilter)) {
                return domainContext.ValidateCredentials(userName, passWord);
            }
        }
    }
}
