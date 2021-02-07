using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Shopping.MVCWebUI.Entity
{
    public class Product
    {
        public int Id { get; set; }
        [DisplayName("Ürün Adı")]
        public string Name { get; set; }
        [DisplayName("Ürün Açıklaması")]
        public string Description { get; set; }
        [DisplayName("Ürün fiyatı")]
        public double Price { get; set; }
        [DisplayName("İndirimli Fiyat")]
        public double DiscountedPrice { get; set; }
        [DisplayName("Kampanya Durumu")]
        public bool CampaignStatus { get; set; }
        [DisplayName("Stok Adedi")]
        public int Stock { get; set; }
        [DisplayName("Satılan Miktar")]
        public int QuantitySold { get; set; }
        [DisplayName("Ürün resmi")]
        public string Images { get; set; }
        [DisplayName("Varsayılan resim")]
        public string DefaultImage { get; set; }
        [DisplayName("Anasayfa durumu")]
        public bool IsHome { get; set; }

        public int SubCategoryId { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        [DisplayName("Kampanya Id")]
        public Nullable<int> CampaignId { get; set; }
        public virtual Campaign Campaign { get; set; }

    }
}