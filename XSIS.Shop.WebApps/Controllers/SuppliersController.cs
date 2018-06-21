using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using XSIS.Shop.Repository;
using XSIS.Shop.ViewModel;
using System.Web.Configuration;
using System.Net.Http;
using Newtonsoft.Json;

namespace XSIS.Shop.WebApps.Controllers
{
    public class SuppliersController : Controller
    {
        private string ApiUrl = WebConfigurationManager.AppSettings["XSIS.Shop.API"];
        // GET: Suppliers
        public ActionResult Index()
        {
            string ApiEndPoint = ApiUrl + "api/SupplierApi";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;
            string resultList = response.Content.ReadAsStringAsync().Result.ToString();
            List<SupplierViewModel> result = JsonConvert.DeserializeObject<List<SupplierViewModel>>(resultList);
            return View(result.ToList());
        }

        // GET: Suppliers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;
            string ApiEndPoint = ApiUrl + "api/SupplierApi/Get/" + idx;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;
            string resultVM = response.Content.ReadAsStringAsync().Result.ToString();
            SupplierViewModel result = JsonConvert.DeserializeObject<SupplierViewModel>(resultVM);
            if (result == null)
            {
                return HttpNotFound();
            }
            
            return View(result);
        }

        // GET: Suppliers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SupplierViewModel supplier)
        {
            if (ModelState.IsValid)
            {
                string json = JsonConvert.SerializeObject(supplier);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                string apiEndPoint = ApiUrl + "api/SupplierApi/";
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.PostAsync(apiEndPoint, byteContent).Result;
                string result = response.Content.ReadAsStringAsync().Result.ToString();
                int success = int.Parse(result);
                if (success == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(supplier);
                }
            }

            return View(supplier);
        }

        // GET: Suppliers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;
            string ApiEndPoint = ApiUrl + "api/SupplierApi/Get/" + idx;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;
            string resultVM = response.Content.ReadAsStringAsync().Result.ToString();
            SupplierViewModel result = JsonConvert.DeserializeObject<SupplierViewModel>(resultVM);
            if (result == null)
            {
                return HttpNotFound();
            }
            
            return View(result);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SupplierViewModel supplier)
        {
            if (ModelState.IsValid)
            {
                string json = JsonConvert.SerializeObject(supplier);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                string apiEndPoint = ApiUrl + "api/SupplierApi/";
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.PutAsync(apiEndPoint, byteContent).Result;
                string result = response.Content.ReadAsStringAsync().Result.ToString();
                int success = int.Parse(result);
                if (success == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(supplier);
                }
            }
            return View(supplier);
        }

        // GET: Suppliers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;
            string ApiEndPoint = ApiUrl + "api/SupplierApi/Get/" + idx;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;
            string resultVM = response.Content.ReadAsStringAsync().Result.ToString();
            SupplierViewModel result = JsonConvert.DeserializeObject<SupplierViewModel>(resultVM);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string apiEndPoint = ApiUrl + "api/SupplierApi/" + id;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.DeleteAsync(apiEndPoint).Result;
            string result = response.Content.ReadAsStringAsync().Result.ToString();
            int success = int.Parse(result);
            if (success == 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}
