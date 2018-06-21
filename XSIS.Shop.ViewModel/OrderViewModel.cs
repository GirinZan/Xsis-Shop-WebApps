using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSIS.Shop.ViewModel
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        [StringLength(10, ErrorMessage = "Too many characters")]
        [Display(Name = "Order Number")]
        public string OrderNumber { get; set; }

        [Required]
        [Display(Name = "Order Date")]
        [DisplayFormat(ApplyFormatInEditMode =true, DataFormatString ="{0:MM/dd/yyyy}")]
        public DateTime OrderDate { get; set; }

        [Required (ErrorMessage ="Field has to be filled")]
        [Display(Name = "Customer Name")]
        public int CustomerId { get; set; }

        [Display(Name = "Total Amount")]
        public Nullable<decimal> TotalAmount { get; set; }

        [Display(Name = "Detail Item")]
        public List<OrderItemViewModel> orderItem { get; set; }

        public string CustomerName { get; set; }
        //public string ProductName { get; set; }


    }
}
