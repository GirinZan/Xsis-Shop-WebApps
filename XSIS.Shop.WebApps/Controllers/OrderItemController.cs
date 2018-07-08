using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using XSIS.Shop.Model;
using XSIS.Shop.ViewModel;
using XSIS.Shop.Repository;

namespace XSIS.Shop.WebApps.Controllers
{
    public class OrderItemController : Controller
    {
        private OrderItemRepository service = new OrderItemRepository();
        // GET: OrderItem
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(service.GetProduct(), "Id", "ProductName");
            TempData.Keep();
            return PartialView();
        }
        public ActionResult Add(OrderItemViewModel itemVM)
        {
            Session["itemVM"] = itemVM;
            Session["List"] = TempData["List"];
            var order = service.temporaryList(itemVM, (List<OrderItemViewModel>)TempData["List"]);
            TempData["List"] = order.orderItem;
            TempData.Keep();
            return Json(order, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Get(OrderViewModel order)
        {
            List<OrderItemViewModel> list =  (List<OrderItemViewModel>)TempData["List"];
            TempData.Keep();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RemoveItem(int id)
        {
            List<OrderItemViewModel> list = service.RemoveItem(id, (List<OrderItemViewModel>)TempData["List"]);
            TempData["List"] = list;
            TempData.Keep();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}