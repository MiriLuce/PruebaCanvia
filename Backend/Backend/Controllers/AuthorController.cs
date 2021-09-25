using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PruebaTecnicaAPI.Models;
using PruebaTecnicaAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnicaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController: ControllerBase
    {
        private static String errorMessage = "Lo sentimos, surgió un error. ";
        private readonly String dbConnection;

        public AuthorController(IConfiguration configuration)
        {
            dbConnection = configuration.GetValue<string>("ConnectionStrings:BDPruebaCanvia");
        }

        [HttpPost]
        [Route("create")]
        public ServiceResult Create(Authors author)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                AuthorRepository repository = new AuthorRepository(dbConnection);
                author.setFormatDate();
                author = repository.Create(author);
                if (author != null) author.getFormatDate();
                result.data = author;
            }
            catch (Exception ex)
            {
                result.error = errorMessage + ex.Message;
            }
            return result;
        }

        [HttpGet]
        [Route("detail")]
        public ServiceResult Detail(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                AuthorRepository repository = new AuthorRepository(dbConnection);
                Authors author = repository.Detail(id);
                if (author != null) author.getFormatDate();
                result.data = author;
            }
            catch (Exception ex)
            {
                result.error = errorMessage + ex.Message;
            }
            return result;
        }

        [HttpPut]
        [Route("modify")]
        public ServiceResult Modify(Authors author)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                AuthorRepository repository = new AuthorRepository(dbConnection);
                author.setFormatDate();
                author = repository.Modify(author);
                if (author != null) author.getFormatDate();
                result.data = author;
            }
            catch (Exception ex)
            {
                result.error = errorMessage + ex.Message;
            }
            return result;
        }
        
        [HttpGet]
        [Route("list")]
        public ServiceResult List()
        {
            ServiceResult result = new ServiceResult();
            try
            {
                AuthorRepository repository = new AuthorRepository(dbConnection);
                List<Authors> list = repository.List();
                list.ForEach(author => author.getFormatDate());
                result.data = list;
            }
            catch (Exception ex)
            {
                result.error = errorMessage + ex.Message;
            }
            return result;
        }

        [HttpGet]
        [Route("paginatedlist")]
        public ServiceResult PaginatedList(int page)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                AuthorRepository repository = new AuthorRepository(dbConnection);
                List<Authors> list = repository.PaginatedList(page);
                list.ForEach(author => author.getFormatDate());
                result.data = list;
            }
            catch (Exception ex)
            {
                result.error = errorMessage + ex.Message;
            }
            return result;
        }

        [HttpDelete]
        [Route("delete")]
        public ServiceResult Delete(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                AuthorRepository repository = new AuthorRepository(dbConnection);
                repository.Delete(id);
            }
            catch (Exception ex)
            {
                result.error = errorMessage + ex.Message;
            }
            return result;
        }

        [HttpDelete]
        [Route("logicdelete")]
        public ServiceResult LogicDelete(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                AuthorRepository repository = new AuthorRepository(dbConnection);
                repository.LogicDelete(id);
            }
            catch (Exception ex)
            {
                result.error = errorMessage + ex.Message;
            }
            return result;
        }
    }
}
