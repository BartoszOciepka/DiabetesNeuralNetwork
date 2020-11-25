using System;
using System.Security.Cryptography;
using System.Text;

namespace DiabetesNeuralNetwork
{
	public class RSAHelper
	{
		public static string containerName = "defaultContainerName";
		public static string encrypt(string message)
		{
			var bytesToEncrypt = Encoding.UTF8.GetBytes(message);

			using (var rsa = new RSACryptoServiceProvider(2048))
			{
				try
				{
					rsa.FromXmlString(RSAHelper.getPublicKeyFromContainer());
					var encryptedData = rsa.Encrypt(bytesToEncrypt, true);
					var base64Encrypted = Convert.ToBase64String(encryptedData);
					return base64Encrypted;
				}
				finally
				{
					rsa.PersistKeyInCsp = false;
				}
			}
		}

		public static string decrypt(string message)
		{
			var bytesToDescrypt = Encoding.UTF8.GetBytes(message);

			using (var rsa = new RSACryptoServiceProvider(2048))
			{
				try
				{
					rsa.FromXmlString(RSAHelper.getPrivateKeyFromContainer());

					var resultBytes = Convert.FromBase64String(message);
					var decryptedBytes = rsa.Decrypt(resultBytes, true);
					var decryptedData = Encoding.UTF8.GetString(decryptedBytes);
					return decryptedData.ToString();
				}
				finally
				{
					rsa.PersistKeyInCsp = false;
				}
			}
		}

		// Run this method before any encryption.
		// This method adds a new key container and saves it on the machine. That way the public and private keys are the same each run.
		public static void addKeysToContainer()
		{
			var parameters = new CspParameters
			{
				KeyContainerName = RSAHelper.containerName
			};

			var rsa = new RSACryptoServiceProvider(parameters);

			Console.WriteLine("Added new key provider to default container");
		}

		public static string getPublicKeyFromContainer()
		{
			var parameters = new CspParameters
			{
				KeyContainerName = containerName
			};
			
			var rsa = new RSACryptoServiceProvider(parameters);
			var publicKey = rsa.ExportParameters(false); //Generowanie klucza publiczny
			string publicKeyString = RSAHelper.GetKeyString(publicKey);

			// Display the key information to the console.
			Console.WriteLine($"Public key retrieved from container");

			return publicKeyString;
		}

		public static string getPrivateKeyFromContainer()
		{
			var parameters = new CspParameters
			{
				KeyContainerName = containerName
			};

			var rsa = new RSACryptoServiceProvider(parameters);
			var privateKey = rsa.ExportParameters(true); //Generowanie klucza publiczny
			string privateKeyString = RSAHelper.GetKeyString(privateKey);

			Console.WriteLine("Private key retrieved from container");

			return privateKeyString;
		}
		public static string GetKeyString(RSAParameters key)
		{

			var stringWriter = new System.IO.StringWriter();
			var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
			xmlSerializer.Serialize(stringWriter, key);
			return stringWriter.ToString();
		}
	}
}
