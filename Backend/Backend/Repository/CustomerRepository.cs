using PruebaTecnicaAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnicaAPI.Repository
{
    public class CustomerRepository
    {
        private String _dbConnection; 

        public CustomerRepository(String dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Customers Create(Customers customer)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@firstName", customer.firstName));
            parameters.Add(new SqlParameter("@lastName", customer.lastName));
            parameters.Add(new SqlParameter("@phone", customer.phone));
            parameters.Add(new SqlParameter("@email", customer.email));
            parameters.Add(new SqlParameter("@addressStreet", customer.addressStreet));

            DBConnection connection = new DBConnection(_dbConnection);
            return connection.ExecuteObjectResult<Customers>("CustomerCreate", parameters);
        }

        public Customers Detail(int customerId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@customerId", customerId));

            DBConnection connection = new DBConnection(_dbConnection);
            return connection.ExecuteObjectResult<Customers>("CustomerDetail", parameters);
        }

        public Customers Modify(Customers customer)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@customerId", customer.customerId));
            parameters.Add(new SqlParameter("@firstName", customer.firstName));
            parameters.Add(new SqlParameter("@lastName", customer.lastName));
            parameters.Add(new SqlParameter("@phone", customer.phone));
            parameters.Add(new SqlParameter("@email", customer.email));
            parameters.Add(new SqlParameter("@addressStreet", customer.addressStreet));

            DBConnection connection = new DBConnection(_dbConnection);
            return connection.ExecuteObjectResult<Customers>("CustomerModify", parameters);
        }

        public List<Customers> List()
        {
            DBConnection connection = new DBConnection(_dbConnection);
            return connection.ExecuteListResult<Customers>("CustomerListAll");
        }

        public List<Customers> PaginatedList(int page)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@page", page));

            DBConnection connection = new DBConnection(_dbConnection);
            return connection.ExecuteListResult<Customers>("CustomerList", parameters);
        }

        public void Delete(int customerId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@customerId", customerId));

            DBConnection connection = new DBConnection(_dbConnection);
            connection.ExecuteNonResult("CustomerDelete", parameters);
        }

        public void LogicDelete(int customerId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@customerId", customerId));

            DBConnection connection = new DBConnection(_dbConnection);
            connection.ExecuteNonResult("CustomerLogicDelete", parameters);
        }
    }
}
