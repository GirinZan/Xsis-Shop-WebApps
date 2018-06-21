using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace XSIS.Shop.WebApps.ViewModel
{
    public class SupplierViewModel
    {
        public int Id { get; set; }
        [StringLength(40, ErrorMessage = "Too many characters"),
            Required(ErrorMessage = "Field has to be filled"),
            Display(Name = "Nama Perusahaan")]
        public string CompanyName { get; set; }
        [StringLength(50, ErrorMessage = "Too many characters"),
            Display(Name = "Nama Kontak")]
        public string ContactName { get; set; }
        [StringLength(40, ErrorMessage = "Too many characters"),
            Display(Name = "Judul Kontak")]
        public string ContactTitle { get; set; }
        [StringLength(40, ErrorMessage = "Too many characters"),
            Display(Name = "Kota")]
        public string City { get; set; }
        [StringLength(40, ErrorMessage = "Too many characters"),
            Display(Name = "Negara")]
        public string Country { get; set; }
        [StringLength(30, ErrorMessage = "Too many characters"),
            Display(Name = "Nomor Telepon")]
        public string Phone { get; set; }
        [StringLength(30, ErrorMessage = "Too many characters"),
            Display(Name = "Faksimile")]
        public string Fax { get; set; }
    }
}