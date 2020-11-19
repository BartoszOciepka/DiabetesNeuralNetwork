using DiabetesNeuralNetwork.ORM;
using LinqToDB.Data;
using System;

namespace DiabetesNeuralNetwork
{
	class Program
	{
		static void Main(string[] args)
		{

			DataConnection.DefaultSettings = new LinqToDbSettings();
			runMainMenu();
			Console.ReadKey();
		}

		public static void runMainMenu()
		{
			Console.Clear();
			if (!LoginStatus.IsLoggedIn)
			{
				Console.WriteLine("Hello stranger");
			}
			else
			{
				Console.WriteLine("Hello " + LoginStatus.LoggedInUser.name + " " + LoginStatus.LoggedInUser.surname);
			}
			Console.WriteLine();
			Console.WriteLine("MENU");
			Console.WriteLine("1. Train dataset");
			Console.WriteLine("2. Add data to dataset");
			Console.WriteLine("3. Test data");
			if (LoginStatus.IsLoggedIn) Console.WriteLine("4. Log out");
			else Console.WriteLine("4. Log in");
			Console.WriteLine("5. Register");
			Console.WriteLine("6. Exit program");
			try
			{
				int userPick = Int32.Parse(Console.ReadLine());
				if (userPick <= 0 || userPick >= 7) {
					Console.Write("Value not in range. Please try again");
					Console.ReadLine();
					runMainMenu();
				}

				switch (userPick)
				{
					// Training dataset
					case 1:
						NeuralNetwork.trainNetwork();
						break;
					// Adding data to dataset
					case 2:
						NeuralNetwork.addToDataset();
						break;
					// Test data
					case 3:
						NeuralNetwork.testData();
						break;
					//Log in / Log out
					case 4:
						if (!LoginStatus.IsLoggedIn) AuthorizationManager.LogIn();
						else AuthorizationManager.LogOut();
						break;
					//Register user
					case 5:
						AuthorizationManager.Register();
						break;
					// Exit program
					case 6:
						Environment.Exit(0);
						break;
				}
				Console.ReadLine();
				runMainMenu();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Incorrect input. Please try again");
				Console.ReadLine();
				runMainMenu();
			}
		}
	}
}
