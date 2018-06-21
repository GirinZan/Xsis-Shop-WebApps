using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XSIS.Shop.Repository;
using XSIS.Shop.ViewModel;

namespace XSIS.Shop.WebApi.Controllers
{
    public class SupplierApiController : ApiController
    {
        private SupplierRepository service = new SupplierRepository();
        [HttpGet]
        public List<SupplierViewModel> Get()
        {
            var result = service.GetAllSupplier();
            return result;
        }
        public SupplierViewModel Get(int id)
        {
            var result = service.GetSupplierById(id);
            return result;
        }
        [HttpPost]
        public int Post(SupplierViewModel supplier)
        {
            try
            {
                service.CreateSupplier(supplier);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        [HttpPut]
        public int Put(SupplierViewModel supplier)
        {
            try
            {
                service.EditSupplier(supplier);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        [HttpDelete]
        public int Delete(int id)
        {
            try
            {
                service.DeleteSupplier(id);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
