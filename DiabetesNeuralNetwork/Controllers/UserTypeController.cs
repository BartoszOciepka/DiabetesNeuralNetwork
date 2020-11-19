using DiabetesNeuralNetwork.Models;
using DiabetesNeuralNetwork.ORM;
using LinqToDB;
using System.Collections.Generic;
using System.Linq;

namespace DiabetesNeuralNetwork.Controllers
{
	class UserTypeController
	{
		public static List<UserType> GetAll()
		{
			using (var db = new DBDiabetes())
			{
				var query = from p in db.UserType
							select p;
				
				return query.ToList();
			}
		}

		public static UserType GetById(int id)
		{
			using (var db = new DBDiabetes())
			{
				var query = from p in db.UserType
							where p.userTypeID==id
							select p;

				return query.ToList().First();
			}
		}
		public static List<int> GetAllUserTypeIDs()
		{
			using (var db = new DBDiabetes())
			{
				var query = from p in db.UserType
							select p.userTypeID;

				return query.ToList();
			}
		}

		public static void Remove(int id)
		{
			using (var db = new DBDiabetes())
			{
				db.UserType
				  .Where(p => p.userTypeID == id)
				  .Delete();
			}
		}
	}
}
