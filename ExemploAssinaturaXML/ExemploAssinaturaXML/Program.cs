using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Security.Cryptography.Xml;

namespace ExemploAssinaturaXML {
	class Program {
		static void Main(string[] args) {

			X509Store store = new X509Store("MY", StoreLocation.LocalMachine);
			store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

			X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
			var x509 = collection.Find(X509FindType.FindBySubjectName, "note-oberdan.pca.com.br", true);
			X509Certificate2 cert = x509[0];

			RSA rsa = (RSA)cert.PrivateKey;

			XmlDocument doc = new XmlDocument();
			doc.InnerXml = "<teste vl=\"1\"></teste>";


			SignedXml SignedDocument = new SignedXml();
			KeyInfo keyInfo = new KeyInfo();

			keyInfo.AddClause(new System.Security.Cryptography.Xml.KeyInfoX509Data(cert));
			SignedDocument = new System.Security.Cryptography.Xml.SignedXml(doc);

			//Seta chaves
			SignedDocument.SigningKey = rsa;
			SignedDocument.KeyInfo = keyInfo;

			//Cria referencia
			Reference reference = new Reference();
			reference.Uri = String.Empty;

			//Adiciona transformacao a referencia
			reference.AddTransform(new
			System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform());
			reference.AddTransform(new
			System.Security.Cryptography.Xml.XmlDsigC14NTransform(false));

			//Adiciona referencia ao xml
			SignedDocument.AddReference(reference);

			//Calcula Assinatura
			SignedDocument.ComputeSignature();

			//Pega representação da assinatura
			XmlElement xmlDigitalSignature = SignedDocument.GetXml();

			//Adiciona ao doc XML
			doc.DocumentElement.AppendChild(doc.ImportNode(xmlDigitalSignature, true));

			doc.Save(@"C:\testes\ExemploAssinaturaXML\ExemploAssinaturaXML\xmlAssinado3.xml");

		
		}
	}
}
