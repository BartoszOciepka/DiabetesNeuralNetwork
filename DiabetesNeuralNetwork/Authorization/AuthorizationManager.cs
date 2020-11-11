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
				LoginStatus.IsLoggedIn = true;
				LoginStatus.LoggedInUser = user;
				Console.WriteLine("Hi " + user.name + "! You were successfully logged in.");
			}
		}

		public static void Register()
		{
			Console.WriteLine("What user type to register:");
			UserTypeController.GetAll().ForEach(i => Console.WriteLine("{0}\t", i));
			int userType = Int32.Parse(Console.ReadLine());
			Console.WriteLine("Name:");
			string name = Console.ReadLine();
			Console.WriteLine("Surname:");
			string surname = Console.ReadLine();
			Console.WriteLine("Email:");
			string email = Console.ReadLine();
			Console.WriteLine("Password:");
			string password = Console.ReadLine();
			User user = new User(name, surname, email, password, userType);
			UserController.Insert(user);
			Console.WriteLine("Successfully registered new user");
		}

		public static void LogOut()
		{
			LoginStatus.IsLoggedIn = false;
			LoginStatus.LoggedInUser = null;
			Console.WriteLine("Successfully logged out");
		}
	}
}
