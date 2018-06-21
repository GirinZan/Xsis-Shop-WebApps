using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XSIS.Shop.ViewModel;
using XSIS.Shop.Model;
using System.Data.Entity;

namespace XSIS.Shop.Repository
{
    public class OrderRepository
    {
        public List<OrderViewModel> GetAllOrder()
        {
            using (ShopDBEntities db = new ShopDBEntities()) 
            {
                var orderDB = db.Order.Include(p => p.Customer);
                List<OrderViewModel> list = new List<OrderViewModel>();
                foreach (var item in orderDB.ToList())
                {
                    OrderViewModel order = new OrderViewModel();
                    order.Id = item.Id;
                    order.OrderDate = item.OrderDate;
                    order.OrderNumber = item.OrderNumber;
                    order.CustomerId = item.CustomerId;
                    order.TotalAmount = item.TotalAmount;
                    order.CustomerId = item.CustomerId;
                    order.CustomerName = item.Customer.FirstName + " " + item.Customer.LastName;
                    list.Add(order);
                }
                return list;
            }
        }
        public OrderViewModel GetOrderById(int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Order item = db.Order.Find(id);
                OrderViewModel order = new OrderViewModel();
                order.Id = item.Id;
                order.OrderDate = item.OrderDate;
                order.OrderNumber = item.OrderNumber;
                order.CustomerId = item.CustomerId;
                order.TotalAmount = item.TotalAmount;
                order.CustomerId = item.CustomerId;
                order.CustomerName = item.Customer.FirstName;
                order.orderItem = (from x in db.OrderItem.Include(p => p.Product)
                                   where x.OrderId == id
                                   select new OrderItemViewModel {
                                       Id = x.Id,
                                       OrderId = x.OrderId,
                                       ProductId = x.ProductId,
                                       UnitPrice = x.UnitPrice,
                                       Quantity = x.Quantity,
                                       TotalAmount = x.UnitPrice * x.Quantity,
                                       ProductName = x.Product.ProductName
                                   }).ToList();
                return order;
            }
        }
        public void CreateOrder (OrderViewModel item)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                int Id = (db.Order.ToList().Count != 0) ?
                    (from o in db.Order orderby o.Id descending select o.Id).First() + 1 : 1;
                Order order = new Order();
                order.OrderDate = item.OrderDate;
                order.OrderNumber = item.OrderNumber;
                order.CustomerId = item.CustomerId;
                order.TotalAmount = item.TotalAmount;
                db.Order.Add(order);

                OrderItem orderitem = new OrderItem();
                foreach (var things in item.orderItem)
                {
                    orderitem.OrderId = Id;
                    orderitem.ProductId = things.ProductId;
                    orderitem.UnitPrice = things.UnitPrice;
                    orderitem.Quantity = things.Quantity;
                    db.OrderItem.Add(orderitem);
                    db.SaveChanges();
                }
            }
        }
        public void EditOrder(OrderViewModel item)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Order order = new Order();
                order.Id = item.Id;
                order.OrderDate = item.OrderDate;
                order.OrderNumber = item.OrderNumber;
                order.CustomerId = item.CustomerId;
                order.TotalAmount = item.TotalAmount;
                order.CustomerId = item.CustomerId;
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public void DeleteOrder (int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Order order = db.Order.Find(id);
                db.Order.Remove(order);
                db.SaveChanges();
            }
        }

        public List<CustomerViewModel> GetCustomer()
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
                    customer.NamaLengkap = item.FirstName + " " + item.LastName;

                    Pelanggan.Add(customer);
                }
                return (Pelanggan);
            }
        }
        public OrderViewModel GetCurrentId()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                OrderViewModel OrderVM = new OrderViewModel();
                List<Order> OrderDB = db.Order.ToList();
                int Id = (db.Order.ToList().Count != 0) ?
                    (from o in OrderDB orderby o.Id descending select o.Id).First() + 1 : 1;
                string bulan = (DateTime.Today.Month) < 10 ? ("0" + DateTime.Today.Month).ToString() : (DateTime.Today.Month).ToString();
                string tahun = (DateTime.Today.Year % 2000).ToString();
                if (Id < 10)
                {
                    OrderVM.OrderNumber = "ORD" + tahun + bulan + "00" + Id.ToString();
                }
                else
                {
                    OrderVM.OrderNumber = "ORD" + tahun + bulan + "0" + Id.ToString();
                }
                OrderVM.OrderDate = (DateTime.Now.Date);
                return OrderVM;
            }
        }
        public List<OrderViewModel> Search(string OrderNumber, string Date, string CustomerId)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                List<OrderViewModel> orderan = new List<OrderViewModel>();
                DateTime Date2 = new DateTime();
                if (!string.IsNullOrEmpty(Date))
                {
                    Date2 = DateTime.Parse(Date).Date;
                }
                foreach (var item in db.Order.ToList())
                {
                    if (
                        (item.OrderNumber.ToLower().Contains(OrderNumber.ToLower()) || string.IsNullOrWhiteSpace(OrderNumber) || string.IsNullOrEmpty(OrderNumber)) 
                        &&
                        (string.IsNullOrEmpty(Date) || string.IsNullOrWhiteSpace(Date) || item.OrderDate == Date2) 
                        &&
                        (string.IsNullOrWhiteSpace(CustomerId) || string.IsNullOrEmpty(CustomerId) || item.CustomerId == int.Parse(CustomerId))
                        )
                    {
                        OrderViewModel order = new OrderViewModel();
                        order.Id = item.Id;
                        order.OrderDate = item.OrderDate;
                        order.OrderNumber = item.OrderNumber;
                        order.CustomerId = item.CustomerId;
                        order.TotalAmount = item.TotalAmount;
                        order.CustomerName = item.Customer.FirstName + " " + item.Customer.LastName;

                        orderan.Add(order);
                    }
                }
                return (orderan);
            }

        }
    }
}

