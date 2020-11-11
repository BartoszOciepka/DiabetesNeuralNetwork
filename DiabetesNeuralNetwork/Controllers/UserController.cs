using DiabetesNeuralNetwork.ORM;
using LinqToDB;
using System.Collections.Generic;
using System.Linq;

namespace DiabetesNeuralNetwork.Controllers
{
	class UserController
	{
		public static List<User> GetAll()
		{
			using (var db = new DBDiabetes())
			{
				var query = from p in db.User
							select p;

				return query.ToList();
			}
		}

		public static void Insert(User user)
		{
			using (var db = new DBDiabetes())
			{
				db.Insert(user);
			}
		}

		public static User FindUserByEmailAndPassword(string email, string password)
		{
			using (var db = new DBDiabetes())
			{
				var query = from p in db.User
							where p.email == email
							where p.password == password
							select p;
				return query.ToList().FirstOrDefault();
			}
		}
	}
}
