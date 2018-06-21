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
    public class OrderItemRepository
    {
        public OrderViewModel temporaryList(OrderItemViewModel itemVM, List<OrderItemViewModel> ListItem)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Product produk = db.Product.Find(itemVM.ProductId);
                OrderViewModel order = new OrderViewModel();
                order.orderItem = ListItem;
                foreach (var item in order.orderItem)
                {
                    if (item.ProductId == itemVM.ProductId)
                    {
                        item.Quantity += itemVM.Quantity;
                        item.TotalAmount = item.Quantity * item.UnitPrice;
                        return order;
                    }
                }
                decimal harga = produk.UnitPrice.HasValue ? produk.UnitPrice.Value : 0;
                int Id = (db.Order.ToList().Count != 0) ?
                    (from o in db.Order orderby o.Id descending select o.Id).First() + 1 : 1;
                itemVM.ProductName = produk.ProductName;
                itemVM.OrderId = Id;
                itemVM.UnitPrice = harga;
                itemVM.TotalAmount = itemVM.Quantity * itemVM.UnitPrice;
 
                int idItem = (order.orderItem.Count != 0) ?
                    (from o in order.orderItem orderby o.Id descending select o.Id).First() + 1 : 1;
                itemVM.Id = idItem;
                order.orderItem.Add(itemVM);
                return order;
            }
        }
        public List<Product> GetProduct()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                List<Product> result = db.Product.ToList();
                return result;
            }
        }
        public List<OrderItemViewModel> RemoveItem(int id, List<OrderItemViewModel> list)
        {
            OrderItemViewModel VM = (from x in list
                                     where x.Id == id
                                     select x).FirstOrDefault();
            list.Remove(VM);
            return list;
        }
    }
}
