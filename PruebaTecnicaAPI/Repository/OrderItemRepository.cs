using PruebaTecnicaAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnicaAPI.Repository
{
    public class OrderItemRepository
    {
        private String _dbConnection;

        public OrderItemRepository(String dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public OrderItems Create(OrderItems orderItem)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@orderId", orderItem.orderId));
            parameters.Add(new SqlParameter("@bookId", orderItem.bookId));
            parameters.Add(new SqlParameter("@quantity", orderItem.quantity));

            DBConnection connection = new DBConnection(_dbConnection);
            return connection.ExecuteObjectResult<OrderItems>("OrderItemCreate", parameters);
        }

        public OrderItems Detail(int orderItemId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@orderItemId", orderItemId));

            DBConnection connection = new DBConnection(_dbConnection);
            return connection.ExecuteObjectResult<OrderItems>("OrderItemDetail", parameters);
        }

        public OrderItems Modify(OrderItems orderItem)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@orderItemId", orderItem.orderItemId));
            parameters.Add(new SqlParameter("@bookId", orderItem.bookId));
            parameters.Add(new SqlParameter("@quantity", orderItem.quantity));

            DBConnection connection = new DBConnection(_dbConnection);
            return connection.ExecuteObjectResult<OrderItems>("OrderItemModify", parameters);
        }

        public List<OrderItems> List(int orderId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@orderId", orderId));

            DBConnection connection = new DBConnection(_dbConnection);
            return connection.ExecuteListResult<OrderItems>("OrderItemListAll", parameters);
        }

        public List<OrderItems> PaginatedList(int orderId, int page)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@orderId", orderId));
            parameters.Add(new SqlParameter("@page", page));

            DBConnection connection = new DBConnection(_dbConnection);
            return connection.ExecuteListResult<OrderItems>("OrderItemList", parameters);
        }

        public void Delete(int orderItemId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@orderItemId", orderItemId));

            DBConnection connection = new DBConnection(_dbConnection);
            connection.ExecuteNonResult("OrderItemDelete", parameters);
        }

        public void LogicDelete(int orderItemId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@orderItemId", orderItemId));

            DBConnection connection = new DBConnection(_dbConnection);
            connection.ExecuteNonResult("OrderItemLogicDelete", parameters);
        }
    }
}
