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
    public class OrderItemController : ControllerBase
    {

        private static String ErrorMessage = "Lo sentimos, surgió un error. ";
        private readonly String dbConnection;

        public OrderItemController(IConfiguration configuration)
        {
            dbConnection = configuration.GetValue<string>("ConnectionStrings:BDPruebaCanvia");
        }

        [HttpPost]
        [Route("create")]
        public ServiceResult Create(OrderItems orderItem)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                OrderItemRepository repository = new OrderItemRepository(dbConnection);
                orderItem = repository.Create(orderItem);
                result.data = orderItem;
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
                OrderItemRepository repository = new OrderItemRepository(dbConnection);
                OrderItems orderItem = repository.Detail(id);
                result.data = orderItem;
            }
            catch (Exception ex)
            {
                result.error = ErrorMessage + ex.Message;
            }
            return result;
        }

        [HttpPut]
        [Route("modify")]
        public ServiceResult Modify(OrderItems orderItem)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                OrderItemRepository repository = new OrderItemRepository(dbConnection);
                orderItem = repository.Modify(orderItem);
                result.data = orderItem;
            }
            catch (Exception ex)
            {
                result.error = ErrorMessage + ex.Message;
            }
            return result;
        }

        [HttpGet]
        [Route("list")]
        public ServiceResult List(int orderId)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                OrderItemRepository repository = new OrderItemRepository(dbConnection);
                List<OrderItems> list = repository.List(orderId);
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
        public ServiceResult PaginatedList(int orderId, int page)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                OrderItemRepository repository = new OrderItemRepository(dbConnection);
                List<OrderItems> list = repository.PaginatedList(orderId, page);
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
                OrderItemRepository repository = new OrderItemRepository(dbConnection);
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
                OrderItemRepository repository = new OrderItemRepository(dbConnection);
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
