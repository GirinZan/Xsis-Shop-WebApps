using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace XSIS.Shop.ViewModel
{
    public class CombineOrder
    {
        [StringLength(10, ErrorMessage = "Too many characters"),
            Display(Name = "Order Number")]
        public string OrderNumber { get; set; }
        [Required,
            Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }
        [Required(ErrorMessage = "Field has to be filled"),
            Display(Name = "Customer Name")]
        public int CustomerId { get; set; }
        [Display(Name = "Total Amount")]
        public Nullable<decimal> TotalAmount { get; set; }
        public List<OrderItemViewModel> orderItem { get; set; }
        [StringLength(40, ErrorMessage = "Too many characters"),
            Required(ErrorMessage = "Field has to be filled")]
        public string CustomerName { get; set; }

        public int ItemId { get; set; }
        public int OrderId { get; set; }
        [Display(Name = "Product item")]
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        [Display(Name = "Quantity"),
         RegularExpression("[0-9]+", ErrorMessage = "Only positive numbers")]
        public int Quantity { get; set; }
    }
}
