using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shopping.MVCWebUI.Models
{
    public class Login
    {
        [DisplayName("Kullanıcı Adı")]
        [Required]
        public string UserName { get; set; }
        [DisplayName("Şifre")]
        [Required]
        public string Password { get; set; }
        [DisplayName("Beni Hatırla")]
        public bool RememberMe { get; set; }
    }
}