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
    public class CustomersController : Controller
    {
        private string ApiUrl = WebConfigurationManager.AppSettings["XSIS.Shop.API"];

        // GET: Customers
        public ActionResult Index()
        {
            // Get All Customer API Akses http://localhost:2099/api/CustomerApi/ without parameter
            string ApiEndPoint = ApiUrl + "api/CustomerApi/";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;

            string ListResult = response.Content.ReadAsStringAsync().Result.ToString();
            List<CustomerViewModel> result = JsonConvert.DeserializeObject<List<CustomerViewModel>>(ListResult);
            return View(result.ToList());
        }
        [HttpPost]
        public ActionResult Index(FormCollection input)
        {
            string ApiEndPoint = ApiUrl + "api/CustomerApi/Search/" + input["Name"] + "|" + input["Region"] + "|" + input["Mail"];
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;

            string ListResult = response.Content.ReadAsStringAsync().Result.ToString();
            List<CustomerViewModel> result = JsonConvert.DeserializeObject<List<CustomerViewModel>>(ListResult);

            return View(result.ToList());
        }
        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;

            //API Akses http://localhost:3460/api/CustomerApi/id
            string apiEndPoint = ApiUrl + "api/CustomerApi/Get/" + idx;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiEndPoint).Result;
            string result = response.Content.ReadAsStringAsync().Result.ToString();
            CustomerViewModel custVM = JsonConvert.DeserializeObject<CustomerViewModel>(result);

            if (custVM == null)
            {
                return HttpNotFound();
            }
            return View(custVM);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }
       
        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerViewModel item)
        {
            
            if (ModelState.IsValid)
            {
                //Cek Nama Lengkap
                string apiEndPoint = ApiUrl + "api/CustomerApi/ValidateName/" + item.FirstName + "/" + item.LastName;
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.GetAsync(apiEndPoint).Result;
                string result = response.Content.ReadAsStringAsync().Result.ToString();
                bool CekNama = bool.Parse(result);
                bool CekEmail = false;
                //Cek Email
                if (!(string.IsNullOrEmpty(item.Email) || string.IsNullOrWhiteSpace(item.Email)))
                {
                    string apiEndPoint1 = ApiUrl + "api/CustomerApi/ValidateEmail/" + item.Email;
                    HttpClient client1 = new HttpClient();
                    HttpResponseMessage response1 = client1.GetAsync(apiEndPoint1).Result;
                    string result1 = response1.Content.ReadAsStringAsync().Result.ToString();
                    CekEmail = bool.Parse(result1);
                }

                bool benar = true;
                if (CekNama)
                {
                    ModelState.AddModelError("","Maaf nama lengkap sudah terdaftar di database");
                    benar = false;
                }
                if (CekEmail)
                {
                    ModelState.AddModelError("", "Maaf email sudah terdaftar di database");
                    benar = false;
                }
                if (benar)
                {
                    // Create Customer
                    string json = JsonConvert.SerializeObject(item);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    string apiEndPoint2 = ApiUrl + "api/CustomerApi/";
                    HttpResponseMessage response2 = client.PostAsync(apiEndPoint2, byteContent).Result;
                    string result2 = response2.Content.ReadAsStringAsync().Result.ToString();
                    int success = int.Parse(result2);
                    if (success == 1)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(item);
                    }
                }
                else
                {
                    return View(item);
                }
            }
            
            return View(item);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;
            string apiEndPoint = ApiUrl + "api/CustomerApi/Get/" + idx;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiEndPoint).Result;
            string result = response.Content.ReadAsStringAsync().Result.ToString();
            CustomerViewModel custVM = JsonConvert.DeserializeObject<CustomerViewModel>(result);
            if (custVM == null)
            {
                return HttpNotFound();
            }
            return View(custVM);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerViewModel item)
        {
            if (ModelState.IsValid)
            {
                // Update/Edit Customer
                string json = JsonConvert.SerializeObject(item);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                string apiEndPoint = ApiUrl + "api/CustomerApi/";
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
                    return View(item);
                }
            }

            return View(item);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;
            string apiEndPoint = ApiUrl + "api/CustomerApi/Get/" + idx;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiEndPoint).Result;
            string result = response.Content.ReadAsStringAsync().Result.ToString();
            CustomerViewModel custVM = JsonConvert.DeserializeObject<CustomerViewModel>(result);
            if (custVM == null)
            {
                return HttpNotFound();
            }
            return View(custVM);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string apiEndPoint = ApiUrl + "api/CustomerApi/" + id;
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
