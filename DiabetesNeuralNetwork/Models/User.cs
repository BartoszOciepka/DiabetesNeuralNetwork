using DiabetesNeuralNetwork.Models;

namespace DiabetesNeuralNetwork
{
	class User
	{
		public string name;
		public string surname;
		public string email;
		public UserType userType;

		User(string name, string surname, string email, UserType userType)
		{
			this.name = name;
			this.surname = surname;
			this.email = email;
			this.userType = userType;
		}
	}
}
