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
    public class OrderController : ControllerBase
    {

        private static String ErrorMessage = "Lo sentimos, surgió un error. ";
        private readonly String dbConnection;

        public OrderController(IConfiguration configuration)
        {
            dbConnection = configuration.GetValue<string>("ConnectionStrings:BDPruebaCanvia");
        }

        [HttpPost]
        [Route("create")]
        public ServiceResult Create(Orders order)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                OrderRepository repository = new OrderRepository(dbConnection);
                order.setFormatDate();
                order = repository.Create(order);
                if (order != null) order.getFormatDate();
                result.data = order;
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
                OrderRepository repository = new OrderRepository(dbConnection);
                Orders order = repository.Detail(id);
                if (order != null) order.getFormatDate();
                result.data = order;
            }
            catch (Exception ex)
            {
                result.error = ErrorMessage + ex.Message;
            }
            return result;
        }

        [HttpPut]
        [Route("validate")]
        public ServiceResult Validate(Orders order)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                OrderRepository repository = new OrderRepository(dbConnection);
                order.setFormatDate(); 
                order = repository.Validate(order);
                if (order != null) order.getFormatDate();
                result.data = order;
            }
            catch (Exception ex)
            {
                result.error = ErrorMessage + ex.Message;
            }
            return result;
        }

        [HttpPut]
        [Route("calculate")]
        public ServiceResult Calculate(int orderId)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                OrderRepository repository = new OrderRepository(dbConnection);
                Orders order = repository.Calculate(orderId);
                if (order != null) order.getFormatDate();
                result.data = order;
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
                OrderRepository repository = new OrderRepository(dbConnection);
                List<Orders> list = repository.List();
                list.ForEach(order => order.getFormatDate());
                result.data = list;
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
                OrderRepository repository = new OrderRepository(dbConnection);
                List<Orders> list = repository.PaginatedList(page);
                list.ForEach(order => order.getFormatDate());
                result.data = list;
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
                OrderRepository repository = new OrderRepository(dbConnection);
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
                OrderRepository repository = new OrderRepository(dbConnection);
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
