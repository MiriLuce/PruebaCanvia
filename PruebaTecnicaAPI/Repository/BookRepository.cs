using PruebaTecnicaAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnicaAPI.Repository
{
    public class BookRepository
    {
        private String _dbConnection;

        public BookRepository(String dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Books Create(Books book)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@authorId", book.authorId));
            parameters.Add(new SqlParameter("@title", book.title));
            parameters.Add(new SqlParameter("@genre", book.genre));
            parameters.Add(new SqlParameter("@stock", book.stock));
            parameters.Add(new SqlParameter("@price", book.price));
            parameters.Add(new SqlParameter("@publicationDate", book.publicationDate));

            DBConnection connection = new DBConnection(_dbConnection);
            return connection.ExecuteObjectResult<Books>("BookCreate", parameters);
        }

        public Books Detail(int bookId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@bookId", bookId));

            DBConnection connection = new DBConnection(_dbConnection);
            return connection.ExecuteObjectResult<Books>("BookDetail", parameters);
        }

        public Books Modify(Books book)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@bookId", book.bookId));
            parameters.Add(new SqlParameter("@authorId", book.authorId));
            parameters.Add(new SqlParameter("@title", book.title));
            parameters.Add(new SqlParameter("@genre", book.genre));
            parameters.Add(new SqlParameter("@stock", book.stock));
            parameters.Add(new SqlParameter("@price", book.price));
            parameters.Add(new SqlParameter("@publicationDate", book.publicationDate));


            DBConnection connection = new DBConnection(_dbConnection);
            return connection.ExecuteObjectResult<Books>("BookModify", parameters);
        }

        public List<Books> List()
        {
            DBConnection connection = new DBConnection(_dbConnection);
            return connection.ExecuteListResult<Books>("BookListAll");
        }

        public List<Books> PaginatedList(int page)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@page", page));

            DBConnection connection = new DBConnection(_dbConnection);
            return connection.ExecuteListResult<Books>("BookList", parameters);
        }

        public void Delete(int bookId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@bookId", bookId));

            DBConnection connection = new DBConnection(_dbConnection);
            connection.ExecuteNonResult("BookDelete", parameters);
        }

        public void LogicDelete(int bookId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@bookId", bookId));

            DBConnection connection = new DBConnection(_dbConnection);
            connection.ExecuteNonResult("BookLogicDelete", parameters);
        }
    }
}
