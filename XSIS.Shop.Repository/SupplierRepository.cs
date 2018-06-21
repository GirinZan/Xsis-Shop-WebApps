using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSIS.Shop.Model;
using XSIS.Shop.ViewModel;

namespace XSIS.Shop.Repository
{

    public class SupplierRepository
    {
        public List<SupplierViewModel> GetAllSupplier()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                List<SupplierViewModel> ListViewModel = new List<SupplierViewModel>();
                foreach (var supplier in db.Supplier.ToList())
                {
                    SupplierViewModel supply = new SupplierViewModel();
                    supply.Id = supplier.Id;
                    supply.CompanyName = supplier.CompanyName;
                    supply.ContactName = supplier.ContactName;
                    supply.ContactTitle = supplier.ContactTitle;
                    supply.City = supplier.City;
                    supply.Country = supplier.Country;
                    supply.Phone = supplier.Phone;
                    supply.Fax = supplier.Fax;

                    ListViewModel.Add(supply);
                }
                return (ListViewModel);
            }
        }
        public SupplierViewModel GetSupplierById(int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                SupplierViewModel supply = new SupplierViewModel();
                Supplier supplier = db.Supplier.Find(id);
                
                supply.Id = supplier.Id;
                supply.CompanyName = supplier.CompanyName;
                supply.ContactName = supplier.ContactName;
                supply.ContactTitle = supplier.ContactTitle;
                supply.City = supplier.City;
                supply.Country = supplier.Country;
                supply.Phone = supplier.Phone;
                supply.Fax = supplier.Fax;
                supply.Produk = (from x in db.Product
                                 where x.SupplierId == id
                                 select new ProductViewModel
                                 {
                                     ProductName = x.ProductName,
                                     UnitPrice = x.UnitPrice,
                                     Package = x.Package,
                                     IsDiscontinued = x.IsDiscontinued
                                 }).ToList();
                return (supply);
            }
        }
        public void CreateSupplier(SupplierViewModel supplier)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Supplier supply = new Supplier();
                supply.CompanyName = supplier.CompanyName;
                supply.ContactName = supplier.ContactName;
                supply.ContactTitle = supplier.ContactTitle;
                supply.City = supplier.City;
                supply.Country = supplier.Country;
                supply.Phone = supplier.Phone;
                supply.Fax = supplier.Fax;
                db.Supplier.Add(supply);
                db.SaveChanges();
            }
        }
        public void EditSupplier(SupplierViewModel supplier)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Supplier supply = new Supplier();
                supply.Id = supplier.Id;
                supply.CompanyName = supplier.CompanyName;
                supply.ContactName = supplier.ContactName;
                supply.ContactTitle = supplier.ContactTitle;
                supply.City = supplier.City;
                supply.Country = supplier.Country;
                supply.Phone = supplier.Phone;
                supply.Fax = supplier.Fax;
                db.Entry(supply).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public void DeleteSupplier(int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Supplier supplier = db.Supplier.Find(id);
                db.Supplier.Remove(supplier);
                db.SaveChanges();
            }
        }
    }
}
