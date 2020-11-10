using System;
using System.Security.Cryptography;
using System.Text;

namespace DiabetesNeuralNetwork
{
	class RSAHelper
	{
		public static string encode(string message)
		{
			byte[] data = Encoding.ASCII.GetBytes(message);
			try
			{
				byte[] encr;
				using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
				{
					RSA.ImportParameters(RSA.ExportParameters(false));
					encr = RSA.Encrypt(data, false);
				}
				return encr.ToString();
			}
			catch (CryptographicException e)
			{
				Console.WriteLine(e.Message);
				throw new Exception("Exception encoding data");
			}

		}

		public static string decode(string message)
		{
			byte[] data = Encoding.ASCII.GetBytes(message);
			try
			{
				byte[] encr;
				using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
				{
					RSA.ImportParameters(RSA.ExportParameters(false));
					encr = RSA.Decrypt(data, false);
				}
				return encr.ToString();
			}
			catch (CryptographicException e)
			{
				Console.WriteLine(e.Message);
				throw new Exception("Exception decrypting data");
			}
		}
	}
}
