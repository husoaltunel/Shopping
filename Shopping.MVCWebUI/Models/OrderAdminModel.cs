using Shopping.MVCWebUI.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Shopping.MVCWebUI.Models
{
    public class OrderAdminModel
    {
        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }
        [DisplayName("Sipariş Id")]
        public int OrderId { get; set; }
        [DisplayName("Sipariş Numarası")]
        public string OrderNumber { get; set; }
        [DisplayName("Toplam Fiyat")]
        public double TotalPrice { get; set; }
        [DisplayName("Sipariş Durumu")]
        public EnumOrderState OrderState { get; set; }
        [DisplayName("Sipariş Tarihi")]
        public DateTime OrderDate { get; set; }
        [DisplayName("Ürün Sayısı")]
        public int Count { get; set; }
    }
}