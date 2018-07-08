using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using XSIS.Shop.ViewModel;
using XSIS.Shop.Repository;
using XSIS.Shop.Model;

namespace XSIS.Shop.WebApi.Controllers
{
    public class ProductApiController : ApiController
    {
        private ProductRepository service = new ProductRepository();
        [HttpGet]
        [Route("api/ProductApi/")]
        public List<ProductViewModel> Get ()
        {
            var result = service.GetAllProduct();
            return result;
        }
        [HttpGet]
        [Route("api/ProductApi/{id}")]
        public ProductViewModel Get(int id)
        {
            var result = service.GetProductById(id);
            return result;
        }
        [HttpPost]
        [Route("api/ProductApi/")]
        public int Post(ProductViewModel item)
        {
            try
            {
                service.CreateProduct(item);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        [HttpPut]
        [Route("api/ProductApi/")]
        public int Put(ProductViewModel item)
        {
            try
            {
                service.EditProduct(item);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        [HttpDelete]
        [Route("api/ProductApi/{id}")]
        public int Delete(int id)
        {
            try
            {
                service.DeleteProduct(id);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        [HttpGet]
        [Route("api/ProductApi/Supplier")]
        public List<SupplierViewModel> Supplier()
        {
            var result = service.GetAllSupplier();
            return result;
        }

    }
}
