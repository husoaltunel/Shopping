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

        [Required(ErrorMessage = "Lütfen adres başlığı giriniz")]
        public string AddressTitle { get; set; }

        [Required(ErrorMessage = "Lütfen bir adres giriniz")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Şehir ismi giriniz")]
        public string City { get; set; }

        [Required(ErrorMessage = "Semt ismi giriniz")]
        public string District { get; set; }

        [Required(ErrorMessage = "Mahalle ismi giriniz")]
        public string Neighborhood { get; set; }
        public string ZipCode { get; set; }
    }
}