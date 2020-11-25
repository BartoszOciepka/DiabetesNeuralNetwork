using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DiabetesNeuralNetwork;

namespace DiabetesNeuralNetworkTests
{
	[TestClass]
	public class RSATest
	{
		[TestMethod]
		public void TestRSACorrectlyEncryptsAndDecrypts()
		{
			string testString = "Ala ma kota";

			string encryptedText = RSAHelper.encrypt(testString);

			Assert.AreEqual(testString, RSAHelper.decrypt(encryptedText), false, "Original text and same text after encrypt and decrypt are not the same");
		}
		[TestMethod]
		public void TestRSAEncryptsTheSameEachTime()
		{
			string testString = "Ala ma kota";

			string encryptedText = RSAHelper.encrypt(testString);
			string anotherEncryptedText = RSAHelper.encrypt(testString);

			Assert.AreEqual(RSAHelper.decrypt(encryptedText), RSAHelper.decrypt(anotherEncryptedText), false, "Not the same results when encrypting and decrypting the same string");
		}
		[TestMethod]
		public void testUserValidationCorrect()
		{
			string name = "test";
			string surname = "test";
			string password = "test";

			Validator.ValidateName(name);
			Validator.ValidateSurname(surname);
			Validator.ValidatePassword(password);
		}

		[TestMethod]
		[ExpectedException(typeof(Exception), "Empty name should not pass validation")]
		public void testUserValidationIncorrect()
		{
			Validator.ValidateName("");
		}
	}
}
