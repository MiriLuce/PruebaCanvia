using Microsoft.AspNetCore.Http;
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
    public class CustomerController : ControllerBase
    {
        private static String ErrorMessage = "Lo sentimos, surgió un error. ";
        private readonly String dbConnection;

        public CustomerController(IConfiguration configuration)
        {
            dbConnection = configuration.GetValue<string>("ConnectionStrings:BDPruebaCanvia");
        }

        [HttpPost]
        [Route("create")]
        public ServiceResult Create(Customers customer)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                CustomerRepository repository = new CustomerRepository(dbConnection);
                result.data = repository.Create(customer);
            }
            catch (Exception ex)
            {
                result.error = ErrorMessage + ex.Message;
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
                CustomerRepository repository = new CustomerRepository(dbConnection);
                result.data = repository.Detail(id);
            }
            catch (Exception ex)
            {
                result.error = ErrorMessage + ex.Message;
            }
            return result;
        }

        [HttpPut]
        [Route("modify")]
        public ServiceResult Modify(Customers customer)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                CustomerRepository repository = new CustomerRepository(dbConnection);
                result.data = repository.Modify(customer);
            }
            catch (Exception ex)
            {
                result.error = ErrorMessage + ex.Message;
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
                CustomerRepository repository = new CustomerRepository(dbConnection);
                result.data = repository.List();
            }
            catch (Exception ex)
            {
                result.error = ErrorMessage + ex.Message;
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
                CustomerRepository repository = new CustomerRepository(dbConnection);
                result.data = repository.PaginatedList(page);
            }
            catch (Exception ex)
            {
                result.error = ErrorMessage + ex.Message;
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
                CustomerRepository repository = new CustomerRepository(dbConnection);
                repository.Delete(id);
            }
            catch (Exception ex)
            {
                result.error = ErrorMessage + ex.Message;
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
                CustomerRepository repository = new CustomerRepository(dbConnection);
                repository.LogicDelete(id);
            }
            catch (Exception ex)
            {
                result.error = ErrorMessage + ex.Message;
            }
            return result;
        }
    }
}
