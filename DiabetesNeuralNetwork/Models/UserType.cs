using System.Collections.Generic;
using LinqToDB.Mapping;

namespace DiabetesNeuralNetwork.Models
{
	[Table(Name = "UserType")]
	public class UserType
	{
		[PrimaryKey, Identity]
		public int userTypeID;
		[Column(Name = "name"), NotNull]
		public string name { get; set; } 
		List<User> users { get; set; }

		public override string ToString()
		{
			return this.userTypeID + " " + this.name;
		}
	}
}
