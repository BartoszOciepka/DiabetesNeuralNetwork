using DiabetesNeuralNetwork.Controllers;
using DiabetesNeuralNetwork.Models;
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

			writeMainMenu();

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
					// Test data
					case 1:
						NeuralNetwork.testData();
						break;
					//Log in / Log out
					case 2:
						if (!LoginStatus.IsLoggedIn) AuthorizationManager.LogIn();
						else AuthorizationManager.LogOut();
						break;
					//Register user
					case 3:
						AuthorizationManager.Register();
						break;
					// Exit program
					case 4:
						Environment.Exit(0);
						break;

					//for doctor, not for patient
					// Training dataset
					case 5:
						NeuralNetwork.trainNetwork();
						break;
					// Adding data to dataset
					case 6:
						NeuralNetwork.addToDataset();
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

        private static void writeMainMenu()
        {
			string userTypeName = "";

			if (LoginStatus.IsLoggedIn)
			{
				Console.WriteLine("Hello " + LoginStatus.LoggedInUser.name + " " + LoginStatus.LoggedInUser.surname);
				int userTypeID = LoginStatus.LoggedInUser.userTypeID;
				UserType userType = UserTypeController.GetById(userTypeID);
				userTypeName = userType.name;
				Console.WriteLine("Role: " + userType.name);    // patient doctor
			}
			else
			{
				Console.WriteLine("Hello stranger");
			}

			Console.WriteLine();
			Console.WriteLine("MENU");
			Console.WriteLine("1. Test data");              // patient
			if (LoginStatus.IsLoggedIn) Console.WriteLine("2. Log out");
			else Console.WriteLine("2. Log in");            // patient
			Console.WriteLine("3. Register");           // patient
			Console.WriteLine("4. Exit program");           // patient
			if (userTypeName == "doctor")
			{
				Console.WriteLine();
				Console.WriteLine("5. Train dataset");
				Console.WriteLine("6. Add data to dataset");
			}
		}
    }
}
