using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace XSIS.Shop.ViewModel
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        [StringLength(40, ErrorMessage = "Too many characters"),
            Required(ErrorMessage = "Field has to be filled"),
            Display(Name = "Nama Depan")]
        public string FirstName { get; set; }
        [StringLength(40, ErrorMessage = "Too many characters"),
            Required(ErrorMessage = "Field has to be filled"),
            Display(Name = "Nama Belakang")]
        public string LastName { get; set; }
        [StringLength(40, ErrorMessage = "Too many characters"),
            Display(Name = "Kota")]
        public string City { get; set; }
        [StringLength(40, ErrorMessage = "Too many characters"),
            Display(Name = "Negara")]
        public string Country { get; set; }
        [StringLength(20, ErrorMessage = "Too many characters"),
            Display(Name = "Nomor Telepon"),
            RegularExpression("[0-9()+-]+", ErrorMessage = "Only (), +, -, and numerics are allowed")]
        public string Phone { get; set; }
        [StringLength(35, ErrorMessage = "Too many characters"),
            Display(Name = "E-Mail"),
            EmailAddress]
        public string Email { get; set; }
        public string NamaLengkap { get; set; }
    }
}
