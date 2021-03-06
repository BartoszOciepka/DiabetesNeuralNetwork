﻿using DiabetesNeuralNetwork.ORM;
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

		public static bool IsUserWithEmail(string email)
		{
			using (var db = new DBDiabetes())
			{
				var query = from p in db.User
							where p.email == email
							select p;

				return query.ToList().Any();
			}
		}

		public static User FindUserByEmailAndPassword(string email, string password)
		{
			using (var db = new DBDiabetes())
			{
				var query = from p in db.User
							where p.email == email
							select p;

				User userFromDB = query.ToList().FirstOrDefault();
				if (userFromDB == null) return null;
				else
				{
					if (RSAHelper.decrypt(userFromDB.password) == RSAHelper.decrypt(password))
					{
						return userFromDB;
					}
					else return null;
				}
			}
		}
	}
}
