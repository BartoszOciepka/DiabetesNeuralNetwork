using DiabetesNeuralNetwork.Controllers;
using System;

namespace DiabetesNeuralNetwork
{
	class AuthorizationManager
	{
		public static void LogIn()
		{
			Console.WriteLine("Email:");
			string email = Console.ReadLine();
			Console.WriteLine("Password:");
			string password = Console.ReadLine();
			User user = UserController.FindUserByEmailAndPassword(email, password);
			if(user == null) {
				Console.WriteLine("Incorrect credentials");
			}
			else
			{
				LogInUser(user);
				Console.WriteLine("Hi " + user.name + "! You were successfully logged in.");
			}
		}

		public static void LogInUser(User user)
		{
			LoginStatus.IsLoggedIn = true;
			LoginStatus.LoggedInUser = user;
		}

		public static void Register()
		{
			try
			{
				Console.WriteLine("What user type to register:");
				UserTypeController.GetAll().ForEach(i => Console.WriteLine("{0}\t", i));
				int userTypeID = Int32.Parse(Console.ReadLine());
				Console.WriteLine("Name:");
				string name = Console.ReadLine();
				Console.WriteLine("Surname:");
				string surname = Console.ReadLine();
				Console.WriteLine("Email:");
				string email = Console.ReadLine();
				Console.WriteLine("Password:");
				string password = Console.ReadLine();
				Validator.ValidateUser(name, surname, email, password, userTypeID);
				User user = new User(name, surname, email, password, userTypeID);
				UserController.Insert(user);
				LogInUser(user);
				Console.WriteLine("Successfully registered new user");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public static void LogOut()
		{
			LoginStatus.IsLoggedIn = false;
			LoginStatus.LoggedInUser = null;
			Console.WriteLine("Successfully logged out");
		}
	}
}
