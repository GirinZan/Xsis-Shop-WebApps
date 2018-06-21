using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace XSIS.Shop.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [StringLength(50, ErrorMessage = "Too many characters"),
            Required(ErrorMessage = "Field has to be filled"),
            Display(Name = "Nama Produk")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Field has to be filled"),
            Display(Name = "Id Supplier")]
        public int SupplierId { get; set; }
        [Display(Name = "Harga Satuan")]
        public Nullable<decimal> UnitPrice { get; set; }
        [Display(Name = "Paket")]
        public string Package { get; set; }
        [Display(Name = "Sudah Berhenti")]
        public bool IsDiscontinued { get; set; }

        public string SupplierName { get; set; }
    }
}
