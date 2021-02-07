using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shopping.MVCWebUI.Entity
{
    public class Order
    {
        public int Id { get; set; }
        [DisplayName("Sipariş Numarası")]
        public string OrderNumber { get; set; }
        [DisplayName("Toplam Fiyat")]
        public double TotalPrice { get; set; }
        [DisplayName("Sipariş Tarihi")]
        public DateTime OrderDate { get; set; }
        [DisplayName("Sipariş Durumu")]
        public EnumOrderState OrderState { get; set; }

        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }
        [DisplayName("Adres Başlığı")]
        public string AddressTitle { get; set; }
        [DisplayName("Adres")]
        public string Address { get; set; }
        [DisplayName("Şehir")]
        public string City { get; set; }
        [DisplayName("İlçe")]
        public string District { get; set; }
        [DisplayName("Semt")]
        public string Neighborhood { get; set; }
        [DisplayName("Posta Kodu")]
        public string ZipCode { get; set; }

        public virtual List<OrderLine> OrderLines{ get; set; }
    }
    public class OrderLine
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}