using Shopping.MVCWebUI.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Shopping.MVCWebUI.Models
{
    public class CreateProductModel
    {
        public int Id { get; set; }
        [DisplayName("Ürün Adı")]
        public string Name { get; set; }
        [DisplayName("Ürün Açıklaması")]
        public string Description { get; set; }
        [DisplayName("Ürün fiyatı")]
        public double Price { get; set; }
        [DisplayName("İndirimli fiyatı")]
        public double DiscountedPrice { get; set; }
        [DisplayName("Kampanya Durumu")]
        public bool CampaignStatus { get; set; }
        [DisplayName("Stok Adedi")]
        public int Stock { get; set; }
        [DisplayName("Satılan Miktar")]
        public int QuantitySold { get; set; }
        [DisplayName("Ürün resmi")]
        public HttpPostedFileBase[] Images { get; set; }
        [DisplayName("Anasayfa durumu")]
        public bool IsHome { get; set; }
        public int SubCategoryId { get; set; }
        [DisplayName("Kampanya Id")]
        public System.Nullable<int> CampaignId { get; set; }
    }
}