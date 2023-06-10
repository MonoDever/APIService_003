using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace APIService_003.DAL.Connection
{
    public class ExecuteCommand : ConnectDB
    {
        protected SqlConnection cnn { get; set; }
        public ExecuteCommand(IConfiguration configuration) : base(configuration)
        {
            cnn = ConnectionToDataBase();
        }

        public string CreateUpdateDataByProcedure(string storeName, SqlParameter[] sqlParameters)
        {
            try
            {
                cnn.Open();
                SqlCommand command;
                command = new SqlCommand(storeName, cnn);
                command.CommandType = CommandType.StoredProcedure;
                if (sqlParameters != null)
                {
                    command.Parameters.AddRange(sqlParameters);
                }
                command.ExecuteNonQuery();
                command.Dispose();
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return "Success";
        }
        public string ReadData<T>(string sql, SqlParameter[] sqlParameters)
        {
            #region Implemented ReadData
            var output = "";
            try
            {
                cnn.Open();
                SqlCommand command;
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet ds = new DataSet();
                command = new SqlCommand(sql, cnn);
                command.CommandType = CommandType.StoredProcedure;
                if (sqlParameters != null)
                {
                    command.Parameters.AddRange(sqlParameters);
                }
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                cnn.Close();
                DataTable dt = ds.Tables[0];
                var response = DataMapping.ConvertToList<T>(dt);
                output = JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connect Failed {ex.Message}");
            }
            #endregion Implemented ReadData

            return output;
        }
    }
}

