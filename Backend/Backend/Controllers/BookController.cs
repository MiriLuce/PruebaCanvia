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
    public class BookController: ControllerBase
    {
        private static String errorMessage = "Lo sentimos, surgió un error. ";
        private readonly String dbConnection;

        public BookController(IConfiguration configuration)
        {
            dbConnection = configuration.GetValue<string>("ConnectionStrings:BDPruebaCanvia");
        }

        [HttpPost]
        [Route("create")]
        public ServiceResult Create(Books book)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                BookRepository repository = new BookRepository(dbConnection);
                book.setFormatDate();
                book = repository.Create(book);
                if (book != null) book.getFormatDate();
                result.data = book;
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
                BookRepository repository = new BookRepository(dbConnection);
                Books book = repository.Detail(id);
                if (book != null) book.getFormatDate();
                result.data = book;
            }
            catch (Exception ex)
            {
                result.error = errorMessage + ex.Message;
            }
            return result;
        }

        [HttpPut]
        [Route("modify")]
        public ServiceResult Modify(Books book)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                BookRepository repository = new BookRepository(dbConnection);
                book.setFormatDate();
                book = repository.Modify(book);
                if (book != null) book.getFormatDate();
                result.data = book;
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
                BookRepository repository = new BookRepository(dbConnection);
                AuthorRepository authorRepository = new AuthorRepository(dbConnection);
                List<Books> list = repository.List();
                list.ForEach(book => {
                    book.getFormatDate();
                    book.author = authorRepository.Detail(book.authorId);
                });
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
                BookRepository repository = new BookRepository(dbConnection);
                List<Books> list = repository.PaginatedList(page);
                list.ForEach(book => book.getFormatDate());
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
                BookRepository repository = new BookRepository(dbConnection);
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
                BookRepository repository = new BookRepository(dbConnection);
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
