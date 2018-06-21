using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using XSIS.Shop.Model;
using XSIS.Shop.ViewModel;
using XSIS.Shop.Repository;
using System.Net.Http;
using Newtonsoft.Json;
using System.Web.Configuration;

namespace XSIS.Shop.WebApps.Controllers
{
    public class OrdersController : Controller
    {
        private ShopDBEntities db = new ShopDBEntities();
        private OrderRepository service = new OrderRepository();
        private string ApiUrl = WebConfigurationManager.AppSettings["XSIS.Shop.API"];
        // GET: Orders
        public ActionResult Index()
        {
            string ApiEndPoint = ApiUrl + "api/OrderApi";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;
            string resultList = response.Content.ReadAsStringAsync().Result.ToString();
            List<OrderViewModel> result = JsonConvert.DeserializeObject<List<OrderViewModel>>(resultList);

            ApiEndPoint = ApiUrl + "api/OrderApi/Customer";
            response = client.GetAsync(ApiEndPoint).Result;
            resultList = response.Content.ReadAsStringAsync().Result.ToString();
            var listCustomer = JsonConvert.DeserializeObject<List<CustomerViewModel>>(resultList);
            ViewBag.CustomerId = new SelectList(listCustomer, "Id", "NamaLengkap");
            return View(result.ToList());
        }
        [HttpPost]
        public ActionResult Index(FormCollection input)
        {
            string ApiEndPoint = ApiUrl + "api/OrderApi/Search/" + input["OrderNumber"] +"|"+ input["Date"].Replace("/","-") + "|" + input["CustomerId"];
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;
            string resultList = response.Content.ReadAsStringAsync().Result.ToString();
            List<OrderViewModel> result = JsonConvert.DeserializeObject<List<OrderViewModel>>(resultList);

            ApiEndPoint = ApiUrl + "api/OrderApi/Customer";
            response = client.GetAsync(ApiEndPoint).Result;
            resultList = response.Content.ReadAsStringAsync().Result.ToString();
            var listCustomer = JsonConvert.DeserializeObject<List<CustomerViewModel>>(resultList);
            ViewBag.CustomerId = new SelectList(listCustomer, "Id", "NamaLengkap");
            return View(result.ToList());
        }
        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;
            string ApiEndPoint = ApiUrl + "api/OrderApi/" + idx;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;
            string resultList = response.Content.ReadAsStringAsync().Result.ToString();
            var result = JsonConvert.DeserializeObject<OrderViewModel>(resultList);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            string ApiEndPoint = ApiUrl + "api/OrderApi/Create/";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;
            string resultList = response.Content.ReadAsStringAsync().Result.ToString();
            var OrderVM = JsonConvert.DeserializeObject<OrderViewModel>(resultList);
            TempData["List"] = new List<OrderItemViewModel>();
            TempData.Keep();

            ApiEndPoint = ApiUrl + "api/OrderApi/Customer";
            response = client.GetAsync(ApiEndPoint).Result;
            resultList = response.Content.ReadAsStringAsync().Result.ToString();
            var listCustomer = JsonConvert.DeserializeObject<List<CustomerViewModel>>(resultList);
            ViewBag.CustomerId = new SelectList(listCustomer, "Id", "NamaLengkap");
            return View(OrderVM);
        }
        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderViewModel order)
        {
            string ApiEndPoint;
            HttpClient client = new HttpClient();
            HttpResponseMessage response;
            if (ModelState.IsValid)
            {
                order.orderItem = (List<OrderItemViewModel>)TempData["List"];

                string json = JsonConvert.SerializeObject(order);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                ApiEndPoint = ApiUrl + "api/OrderApi/";
                response = client.PostAsync(ApiEndPoint, byteContent).Result;
                string result = response.Content.ReadAsStringAsync().Result.ToString();
                var success = bool.Parse(result);
                if (success == true)
                {
                    return RedirectToAction("Index");
                }
            }
            ApiEndPoint = ApiUrl + "api/OrderApi/Customer";
            response = client.GetAsync(ApiEndPoint).Result;
            string resultList = response.Content.ReadAsStringAsync().Result.ToString();
            var listCustomer = JsonConvert.DeserializeObject<List<CustomerViewModel>>(resultList);
            ViewBag.CustomerId = new SelectList(listCustomer, "Id", "NamaLengkap");
            return View("Create", order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;
            OrderViewModel order = service.GetOrderById(idx);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(service.GetCustomer(), "Id", "NamaLengkap", order.CustomerId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                service.EditOrder(order);
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(service.GetCustomer(), "Id", "NamaLengkap", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;
            OrderViewModel order = service.GetOrderById(idx);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            service.DeleteOrder(id);
            return RedirectToAction("Index");
        }
    }
}
