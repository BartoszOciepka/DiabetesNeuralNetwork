using DiabetesNeuralNetwork.Controllers;
using System;

namespace DiabetesNeuralNetwork
{
	public class Validator
	{
		public static void ValidateUser(string name, string surname, string email, string password, int userTypeID)
		{
			ValidateName(name);
			ValidateSurname(surname);
			ValidateEmail(email);
			ValidatePassword(password);
			ValidateUserTypeID(userTypeID);
		}
		public static void ValidateName(string name)
		{
			if (name == "") throw new Exception("Name cannot be empty");
			if (name.Length > 255) throw new Exception("Name cannot be longer than 255 characters");
		}

		public static void ValidateSurname(string surname)
		{
			if (surname == "") throw new Exception("Surname cannot be empty");
			if (surname.Length > 255) throw new Exception("Surname cannot be loonger than 255 characters");
		}
		public static void ValidateEmail(string email)
		{
			var addr = new System.Net.Mail.MailAddress(email);
			if (!(addr.Address == email)) throw new Exception("Incorrect email address format");
			if (UserController.IsUserWithEmail(email)) throw new Exception("User with this email already exists in system");
		}

		public static void ValidatePassword(string password)
		{
			if (password == "") throw new Exception("Password cannot be empty");
		}

		public static void ValidateUserTypeID(int userTypeID)
		{
			if (UserTypeController.GetAllUserTypeIDs().IndexOf(userTypeID) == -1) throw new Exception("Incorrect user type ID");
		}
	}
}
