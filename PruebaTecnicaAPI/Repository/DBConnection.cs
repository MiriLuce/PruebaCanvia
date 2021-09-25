using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PruebaTecnicaAPI.Repository
{
    public class DBConnection
    {
        private SqlConnection connection;

        public DBConnection(String connectionString)
        {
            connection = new SqlConnection();
            connection.ConnectionString = connectionString;
        }


        public void ExecuteNonResult(String storeProcedure, List<SqlParameter> parameters = null) 
        {
            try
            {
                SqlCommand command = new SqlCommand(storeProcedure, connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open(); 
            
                if (parameters != null && parameters.Count > 0)
                    foreach (SqlParameter parameter in parameters)
                        command.Parameters.Add(parameter);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
        public Result ExecuteObjectResult<Result>(String storeProcedure, List<SqlParameter> parameters = null) where Result : new()
        {
            Result result = default(Result);
            try
            {
                SqlCommand command = new SqlCommand(storeProcedure, connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();

                if (parameters != null && parameters.Count > 0)
                    foreach (SqlParameter parameter in parameters)
                        command.Parameters.Add(parameter);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    result = ReadObject<Result>(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        public List<Result> ExecuteListResult<Result>(String storeProcedure, List<SqlParameter> parameters = null) where Result : new()
        {
            List<Result> listResult = new List<Result>();
            try
            {
                SqlCommand command = new SqlCommand(storeProcedure, connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();

                if (parameters != null && parameters.Count > 0)
                    foreach (SqlParameter parameter in parameters)
                        command.Parameters.Add(parameter);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    listResult.Add(ReadObject<Result>(reader));
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return listResult;
        }

        private Result ReadObject<Result>(SqlDataReader reader) where Result: new()
        {
            Result result = new Result();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                String propertyName = reader.GetName(i);
                PropertyInfo propertyInfo = result.GetType().GetProperty(propertyName);
                if (propertyInfo != null)  
                {
                    propertyInfo.SetValue(result, reader[propertyName], null);
                }
            }
            return result;
        }
    }
}
