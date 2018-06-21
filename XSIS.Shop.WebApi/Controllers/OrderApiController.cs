using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

using XSIS.Shop.Model;
using XSIS.Shop.ViewModel;
using XSIS.Shop.Repository;

namespace XSIS.Shop.WebApi.Controllers
{
    public class OrderApiController : ApiController
    {
        private ShopDBEntities db = new ShopDBEntities();
        private OrderRepository service = new OrderRepository();

        [HttpGet]
        [Route("api/OrderApi/")]
        public List<OrderViewModel> Get()
        {
            var result = service.GetAllOrder();
            return result;
        }
        [HttpPost]
        [Route("api/OrderApi/")]
        public bool Post(OrderViewModel order)
        {
            try
            {
                service.CreateOrder(order);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }
        [HttpGet]
        [Route("api/OrderApi/{id}")]
        public OrderViewModel Get(int id)
        {
            var result = service.GetOrderById(id);
            return result;
        }
        [HttpGet]
        [Route("api/OrderApi/Customer/")]
        public List<CustomerViewModel> Customer()
        {
            var result = service.GetCustomer();
            return result;
        }
        [HttpGet]
        [Route("api/OrderApi/Search/{id}")]
        public List<OrderViewModel> Search(string id)
        {
            string[] Parameters = id.Split('|');

            string param1 = Parameters[0];
            string param2 = Parameters[1];
            string param3 = Parameters[2];
            var result = service.Search(param1, param2, param3);
            return result;
        }
        [HttpGet]
        [Route("api/OrderApi/Create/")]
        public OrderViewModel Create()
        {
            var result = service.GetCurrentId();
            return result;
        }
    }
}