using LinqToDB.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace DiabetesNeuralNetwork.ORM
{
	public class ConnectionStringSettings : IConnectionStringSettings
	{
		public string ConnectionString { get; set; }
		public string Name { get; set; }
		public string ProviderName { get; set; }
		public bool IsGlobal => false;
	}

	public class LinqToDbSettings : ILinqToDBSettings
	{
		public IEnumerable<IDataProviderSettings> DataProviders => Enumerable.Empty<IDataProviderSettings>();

		public string DefaultConfiguration => "MySql";
		public string DefaultDataProvider => "MySql";

		public IEnumerable<IConnectionStringSettings> ConnectionStrings
		{
			get
			{
				yield return
					new ConnectionStringSettings
					{
						Name = "DiabetesDatabase",
						ProviderName = "MySql",
						ConnectionString = @"Server=localhost;Database=diabetes;Uid=root;Pwd=root;"
					};
			}
		}
	}
}
