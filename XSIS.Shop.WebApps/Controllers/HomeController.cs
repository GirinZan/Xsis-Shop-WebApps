using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XSIS.Shop.Model;

namespace XSIS.Shop.WebApps.Controllers
{
    public class HomeController : Controller
    {
        private ShopDBEntities db = new ShopDBEntities();
        public ActionResult Index()
        {
            ViewBag.TotalCustomer = db.Customer.Count();
            ViewBag.TotalSupplier = db.Supplier.Count();
            ViewBag.TotalProduct = db.Product.Count();
            ViewBag.TotalOrder = db.Order.Count();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}