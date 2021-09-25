using PruebaTecnicaAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnicaAPI.Repository
{
    public class OrderRepository
    {
        private String _dbConnection;

        public OrderRepository(String dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Orders Create(Orders order)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@customerId", order.customerId));
            parameters.Add(new SqlParameter("@shippedDate", order.shippedDate));

            DBConnection connection = new DBConnection(_dbConnection);
            return connection.ExecuteObjectResult<Orders>("OrderCreate", parameters);
        }

        public Orders Detail(int orderId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@orderId", orderId));

            DBConnection connection = new DBConnection(_dbConnection);
            return connection.ExecuteObjectResult<Orders>("OrderDetail", parameters);
        }

        public Orders Validate(Orders order)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@orderId", order.orderId));
            parameters.Add(new SqlParameter("@customerId", order.customerId));
            parameters.Add(new SqlParameter("@shippedDate", order.shippedDate));

            DBConnection connection = new DBConnection(_dbConnection);
            return connection.ExecuteObjectResult<Orders>("OrderValidated", parameters);
        }

        public Orders Calculate(int orderId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@orderId", orderId));

            DBConnection connection = new DBConnection(_dbConnection);
            return connection.ExecuteObjectResult<Orders>("OrderCalculated", parameters);
        }

        public List<Orders> List()
        {
            DBConnection connection = new DBConnection(_dbConnection);
            return connection.ExecuteListResult<Orders>("OrderListAll");
        }

        public List<Orders> PaginatedList(int page)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@page", page));

            DBConnection connection = new DBConnection(_dbConnection);
            return connection.ExecuteListResult<Orders>("OrderList", parameters);
        }

        public void Delete(int orderId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@orderId", orderId));

            DBConnection connection = new DBConnection(_dbConnection);
            connection.ExecuteNonResult("OrderDelete", parameters);
        }

        public void LogicDelete(int orderId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@orderId", orderId));

            DBConnection connection = new DBConnection(_dbConnection);
            connection.ExecuteNonResult("OrderLogicDelete", parameters);
        }
    }
}
