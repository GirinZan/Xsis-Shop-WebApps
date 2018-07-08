using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using XSIS.Shop.ViewModel;
using XSIS.Shop.Repository;

namespace XSIS.Shop.WebApi.Controllers
{
    public class OrderItemApiController : ApiController
    {
        OrderItemRepository service = new OrderItemRepository();
        [HttpGet]
        [Route("api/OrderItemApi/temporaryList")]
        public OrderViewModel temporaryList(OrderItemViewModel itemVM, List<OrderItemViewModel> ListItem)
        {
            var result = service.temporaryList(itemVM, ListItem);
            return result;
        }
    }
}
