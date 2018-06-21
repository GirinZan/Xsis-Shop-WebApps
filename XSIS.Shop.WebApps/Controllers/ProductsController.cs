using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using System.Web.Configuration;
using XSIS.Shop.Repository;
using XSIS.Shop.ViewModel;
using XSIS.Shop.Model;
using System.Net.Http;
using Newtonsoft.Json;

namespace XSIS.Shop.WebApps.Controllers
{
    public class ProductsController : Controller
    {
        private string ApiUrl = WebConfigurationManager.AppSettings["XSIS.Shop.API"];
        ProductRepository service = new ProductRepository();
        // GET: Products
        public ActionResult Index()
        {
            string ApiEndPoint = ApiUrl + "api/ProductApi";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;
            string list = response.Content.ReadAsStringAsync().Result.ToString();
            List < ProductViewModel > result = JsonConvert.DeserializeObject<List<ProductViewModel>>(list);
            return View(result.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;
            string ApiEndPoint = ApiUrl + "api/ProductApi/Get/" + idx;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;
            string list = response.Content.ReadAsStringAsync().Result.ToString();
            ProductViewModel result = JsonConvert.DeserializeObject<ProductViewModel>(list);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            string ApiEndPoint = ApiUrl + "api/ProductApi/Supplier/";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;
            string list = response.Content.ReadAsStringAsync().Result.ToString();
            List<SupplierViewModel> result = JsonConvert.DeserializeObject<List<SupplierViewModel>>(list);
            ViewBag.SupplierId = new SelectList(result.ToList(), "Id", "CompanyName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel item)
        {
            if (ModelState.IsValid)
            {
                string json = JsonConvert.SerializeObject(item);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                string ApiEndPoint = ApiUrl + "api/ProductApi";
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.PostAsync(ApiEndPoint, byteContent).Result;
                string result = response.Content.ReadAsStringAsync().Result.ToString();
                int success = int.Parse(result);
                if (success == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(item);
                }
            }
            string ApiEndPoint1 = ApiUrl + "api/ProductApi/Supplier";
            HttpClient client1 = new HttpClient();
            HttpResponseMessage response1 = client1.GetAsync(ApiEndPoint1).Result;
            string list = response1.Content.ReadAsStringAsync().Result.ToString();
            List<SupplierViewModel> result1 = JsonConvert.DeserializeObject<List<SupplierViewModel>>(list);
            ViewBag.SupplierId = new SelectList(result1.ToList(), "Id", "CompanyName", item.SupplierId);
            return View(item);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;
            string ApiEndPoint = ApiUrl + "api/ProductApi/Get/" + idx;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;
            string list = response.Content.ReadAsStringAsync().Result.ToString();
            ProductViewModel result = JsonConvert.DeserializeObject<ProductViewModel>(list);
            if (result == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplierId = new SelectList(service.GetAllSupplier(), "Id", "CompanyName");
            return View(result);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductViewModel item)
        {
            if (ModelState.IsValid)
            {
                service.EditProduct(item);
                return RedirectToAction("Index");
            }
            ViewBag.SupplierId = new SelectList(service.GetAllSupplier(), "Id", "CompanyName", item.SupplierId);
            return View(item);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;
            string ApiEndPoint = ApiUrl + "api/ProductApi/Get/" + idx;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;
            string list = response.Content.ReadAsStringAsync().Result.ToString();
            ProductViewModel result = JsonConvert.DeserializeObject<ProductViewModel>(list);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            service.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}
