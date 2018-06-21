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
    public class CustomerRepository
    {
        //Select * from Customer
        public List<CustomerViewModel> GetAllCustomer()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                List<CustomerViewModel> Pelanggan = new List<CustomerViewModel>();
                foreach (var item in db.Customer.ToList())
                {
                    CustomerViewModel customer = new CustomerViewModel();
                    customer.Id = item.Id;
                    customer.FirstName = item.FirstName;
                    customer.LastName = item.LastName;
                    customer.City = item.City;
                    customer.Country = item.Country;
                    customer.Phone = item.Phone;
                    customer.Email = item.Email;

                    Pelanggan.Add(customer);
                }
                return (Pelanggan);
            }
        }
        // Select * from Customer where Id = id
        public CustomerViewModel GetCustomerById(int id)
        {

            using (ShopDBEntities db = new ShopDBEntities())
            {
                Customer item = db.Customer.Find(id);
                CustomerViewModel customer = new CustomerViewModel();
                customer.Id = item.Id;
                customer.FirstName = item.FirstName;
                customer.LastName = item.LastName;
                customer.City = item.City;
                customer.Country = item.Country;
                customer.Phone = item.Phone;
                customer.Email = item.Email;
                return (customer);
            } 
        }
        public void AddNewCustomer (CustomerViewModel item)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Customer customer = new Customer();
                customer.FirstName = item.FirstName;
                customer.LastName = item.LastName;
                customer.City = item.City;
                customer.Country = item.Country;
                customer.Phone = item.Phone;
                customer.Email = item.Email;
                db.Customer.Add(customer);
                db.SaveChanges();
            }
        }
        public void UpdateCustomer(CustomerViewModel item)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Customer customer = new Customer();
                customer.Id = item.Id;
                customer.FirstName = item.FirstName;
                customer.LastName = item.LastName;
                customer.City = item.City;
                customer.Country = item.Country;
                customer.Phone = item.Phone;
                customer.Email = item.Email;
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public void DeleteCustomer (int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Customer item = db.Customer.Find(id);
                db.Customer.Remove(item);
                db.SaveChanges();
            }
        }

        //For search bar 
        public List<CustomerViewModel> GetAllCustomerSearch(string name, string region, string mail)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                List<CustomerViewModel> Pelanggan = new List<CustomerViewModel>();
                foreach (var item in db.Customer.ToList())
                {
                    if (string.IsNullOrEmpty(item.Email))
                    {
                        item.Email = " ";
                    }
                    if (
                        ((item.FirstName.ToLower() + " " + item.LastName.ToLower()).Contains(name.ToLower()) || string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name)) &&
                        (string.IsNullOrEmpty(region) || string.IsNullOrWhiteSpace(region) || (item.City.ToLower() + " " + item.Country.ToLower()).Contains(region.ToLower())) &&
                        (string.IsNullOrWhiteSpace(mail) || string.IsNullOrEmpty(mail) || item.Email.ToLower().Contains(mail.ToLower()) )
                        )
                    {
                        CustomerViewModel customer = new CustomerViewModel();
                        customer.Id = item.Id;
                        customer.FirstName = item.FirstName;
                        customer.LastName = item.LastName;
                        customer.City = item.City;
                        customer.Country = item.Country;
                        customer.Phone = item.Phone;
                        customer.Email = item.Email;

                        Pelanggan.Add(customer);
                    }
                }
                return (Pelanggan);
            }

        }
        public bool ValidateName(string NamaDepan, string NamaBelakang)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var namaLengkap = db.Customer.Where(a => (a.FirstName + " " + a.LastName).ToLower().Equals((NamaDepan + " " + NamaBelakang).ToLower())).SingleOrDefault();
                if (namaLengkap == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public bool ValidateEmail(string email)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var list = db.Customer.ToList();
                foreach (var item in list)
                {

                    if (string.IsNullOrEmpty(item.Email))
                    {
                        item.Email = " ";
                    }
                    if (email.ToLower() == item.Email.ToLower())
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
