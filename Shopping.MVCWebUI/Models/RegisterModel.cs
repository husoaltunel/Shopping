using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shopping.MVCWebUI.Models
{
    public class RegisterModel
    {
        [DisplayName("Ad")]
        [Required]
        public string Name { get; set; }
        [DisplayName("Soyad")]
        [Required]
        public string Surname { get; set; }
        [DisplayName("Kullanıcı Adı")]
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Email adresinizi doğru giriniz")]
        public string Email { get; set; }
        [DisplayName("Şifre")]
        [Required]
        public string Password { get; set; }
        [DisplayName("Şifre Tekrar")]
        [Required]
        [Compare("Password",ErrorMessage = "Şifreler uyuşmuyor")]
        public string RePassword { get; set; }
    }
}