using PruebaTecnicaAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnicaAPI.Repository
{
    public class AuthorRepository
    {
        private String _dbConnection;

        public AuthorRepository(String dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Authors Create(Authors author)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@firstName", author.firstName));
            parameters.Add(new SqlParameter("@lastName", author.lastName));
            parameters.Add(new SqlParameter("@birthdate", author.birthdate));
            parameters.Add(new SqlParameter("@deathDate", author.deathDate));
            parameters.Add(new SqlParameter("@country", author.country));

            DBConnection connection = new DBConnection(_dbConnection);
            return connection.ExecuteObjectResult<Authors>("AuthorCreate", parameters);
        }

        public Authors Detail(int authorId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@authorId", authorId));

            DBConnection connection = new DBConnection(_dbConnection);
            return connection.ExecuteObjectResult<Authors>("AuthorDetail", parameters);
        }

        public Authors Modify(Authors author)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@authorId", author.authorId));
            parameters.Add(new SqlParameter("@firstName", author.firstName));
            parameters.Add(new SqlParameter("@lastName", author.lastName));
            parameters.Add(new SqlParameter("@birthdate", author.birthdate));
            parameters.Add(new SqlParameter("@deathDate", author.deathDate));
            parameters.Add(new SqlParameter("@country", author.country));

            DBConnection connection = new DBConnection(_dbConnection);
            return connection.ExecuteObjectResult<Authors>("AuthorModify", parameters);
        }

        public List<Authors> List()
        {
            DBConnection connection = new DBConnection(_dbConnection);
            return connection.ExecuteListResult<Authors>("AuthorListAll");
        }

        public List<Authors> PaginatedList(int page)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@page", page));

            DBConnection connection = new DBConnection(_dbConnection);
            return connection.ExecuteListResult<Authors>("AuthorList", parameters);
        }

        public void Delete(int authorId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@authorId", authorId));

            DBConnection connection = new DBConnection(_dbConnection);
            connection.ExecuteNonResult("AuthorDelete", parameters);
        }

        public void LogicDelete(int authorId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@authorId", authorId));

            DBConnection connection = new DBConnection(_dbConnection);
            connection.ExecuteNonResult("AuthorLogicDelete", parameters);
        }
    }
}
