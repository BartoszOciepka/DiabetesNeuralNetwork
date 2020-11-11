using DiabetesNeuralNetwork.Models;
using LinqToDB;

namespace DiabetesNeuralNetwork.ORM
{
	public class DBDiabetes : LinqToDB.Data.DataConnection
	{
		public DBDiabetes() : base("DiabetesDatabase") { }

		public ITable<UserType> UserType => GetTable<UserType>();
		public ITable<User> User => GetTable<User>();
		
	}
}
