using LinqToDB.Mapping;

namespace DiabetesNeuralNetwork
{
	[Table(Name = "User")]
	public class User
	{
		[PrimaryKey, Identity]
		public int userID;
		[Column(Name = "name"), NotNull]
		public string name;
		[Column(Name = "surname"), NotNull]
		public string surname;
		[Column(Name = "email"), NotNull]
		public string email;
		[Column(Name = "userTypeID"), NotNull]
		public int userTypeID;
		[Column(Name = "password"), NotNull]
		public string password;

		public User()
		{

		}
		public User(string name, string surname, string email, string password, int userTypeID)
		{
			this.name = name;
			this.surname = surname;
			this.email = email;
			this.password = password;
			this.userTypeID = userTypeID;
		}

		public override string ToString()
		{
			return this.name + " " + this.surname + " " + this.email;
		}
	}
}
