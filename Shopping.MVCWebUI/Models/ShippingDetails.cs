using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shopping.MVCWebUI.Models
{
    public class ShippingDetails
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        [Required(ErrorMessage = "Şehir ismi giriniz")]
        public string City { get; set; }
        [Required(ErrorMessage = "İlçe ismi giriniz")]
        public string District { get; set; }
        public string Neighborhood { get; set; }
        [Required(ErrorMessage = "Lütfen bir adres giriniz")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Email giriniz")]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Ödeme yöntemi giriniz")]
        public string PaymentMethod { get; set; }
        public string ZipCode { get; set; }
    }
}