using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace APIService_003.DAL.Connection
{
	public class ConnectDB
	{
		private string SqlEndpoint = "";
		private SqlConnection cnn;
		private IConfiguration _configuration ;
		public ConnectDB(IConfiguration configuration)
		{
			_configuration = configuration;
        }

		public SqlConnection ConnectionToDataBase()
		{
			string connectionString = _configuration["ConnectionStrings:DefaultConnection"];
			cnn = new SqlConnection(connectionString);
			return cnn;
		} 
	}
}

