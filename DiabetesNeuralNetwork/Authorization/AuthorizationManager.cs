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

			var password = readPassword();

			User user = UserController.FindUserByEmailAndPassword(email, RSAHelper.encrypt(password));
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
				string password = readPassword();


				Validator.ValidateUser(name, surname, email, password, userTypeID);
				User user = new User(name, surname, email, RSAHelper.encrypt(password), userTypeID);
				UserController.Insert(user);
				LogInUser(user);
				Console.WriteLine("Successfully registered new user");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

        private static string readPassword()
        {
			var password = string.Empty;
			ConsoleKey key;
			do
			{
				var keyInfo = Console.ReadKey(intercept: true);
				key = keyInfo.Key;

				if (key == ConsoleKey.Backspace && password.Length > 0)
				{
					Console.Write("\b \b");
					password = password.Remove(password.Length - 1);
				}
				else if (!char.IsControl(keyInfo.KeyChar))
				{
					Console.Write("*");
					password += keyInfo.KeyChar;
				}
			} while (key != ConsoleKey.Enter);
			Console.WriteLine();
			
			return password;
		}

        public static void LogOut()
		{
			LoginStatus.IsLoggedIn = false;
			LoginStatus.LoggedInUser = null;
			Console.WriteLine("Successfully logged out");
		}



	}
}
