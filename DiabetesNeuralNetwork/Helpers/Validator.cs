using System;

namespace DiabetesNeuralNetwork
{
	class Validator
	{
		public static void ValidateUser(string name, string surname, string email, string password)
		{
			ValidateName(name);
			ValidateSurname(surname);
			ValidateSurname(email);
			ValidatePassword(password);
		}
		public static void ValidateName(string name)
		{
			if (name == "") throw new Exception("Name cannot be empty");
		}

		public static void ValidateSurname(string surname)
		{
			if (surname == "") throw new Exception("Surname cannot be empty");
		}
		public static void ValidateEmail(string email)
		{
			var addr = new System.Net.Mail.MailAddress(email);
			if (!(addr.Address == email)) throw new Exception("Incorrect email address format");
		}

		public static void ValidatePassword(string password)
		{
			if (password == "") throw new Exception("Password cannot be empty");
		}
	}
}
