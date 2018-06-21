using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSIS.Shop.Model;
using XSIS.Shop.ViewModel;
using System.Data.Entity;

namespace XSIS.Shop.Repository
{
    public class ProductRepository
    {
        public List<ProductViewModel> GetAllProduct()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var product = db.Product.Include(p => p.Supplier);
                List<ProductViewModel> ListView = new List<ProductViewModel>();
                foreach (var item in product.ToList())
                {
                    ProductViewModel produk = new ProductViewModel();
                    produk.Id = item.Id;
                    produk.ProductName = item.ProductName;
                    produk.SupplierId = item.SupplierId;
                    produk.UnitPrice = item.UnitPrice;
                    produk.Package = item.Package;
                    produk.IsDiscontinued = item.IsDiscontinued;
                    produk.SupplierName = item.Supplier.CompanyName;

                    ListView.Add(produk);
                }
                return (ListView);
            }
        }
        public ProductViewModel GetProductById (int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Product item = db.Product.Find(id);
                ProductViewModel produk = new ProductViewModel();
                produk.Id = item.Id;
                produk.ProductName = item.ProductName;
                produk.SupplierId = item.SupplierId;
                produk.UnitPrice = item.UnitPrice;
                produk.Package = item.Package;
                produk.IsDiscontinued = item.IsDiscontinued;
                produk.SupplierName = item.Supplier.CompanyName;
                return (produk);
            }
        }
        public List<SupplierViewModel> GetAllSupplier()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                List<SupplierViewModel> ListView = new List<SupplierViewModel>();
                foreach (var item in db.Supplier.ToList())
                {
                    SupplierViewModel r1 = new SupplierViewModel();
                    r1.CompanyName = item.CompanyName;
                    r1.Id = item.Id;
                    r1.ContactName = item.ContactName;
                    r1.ContactTitle = item.ContactTitle;
                    r1.City = item.City;
                    r1.Country = item.Country;
                    r1.Phone = item.Phone;
                    r1.Fax = item.Fax;

                    ListView.Add(r1);
                }
                return (ListView);
            }
        }
        public void CreateProduct (ProductViewModel item)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Product produk = new Product();
                produk.ProductName = item.ProductName;
                produk.SupplierId = item.SupplierId;
                produk.UnitPrice = item.UnitPrice;
                produk.Package = item.Package;
                produk.IsDiscontinued = item.IsDiscontinued;
                db.Product.Add(produk);
                db.SaveChanges();
            }
        }
        public void EditProduct (ProductViewModel item)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Product produk = new Product();
                produk.Id = item.Id;
                produk.ProductName = item.ProductName;
                produk.SupplierId = item.SupplierId;
                produk.UnitPrice = item.UnitPrice;
                produk.Package = item.Package;
                produk.IsDiscontinued = item.IsDiscontinued;
                db.Entry(produk).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public void DeleteProduct (int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Product product = db.Product.Find(id);
                db.Product.Remove(product);
                db.SaveChanges();
            }
        }

    }
}
