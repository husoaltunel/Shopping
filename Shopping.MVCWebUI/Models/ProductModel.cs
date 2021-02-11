using Shopping.MVCWebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shopping.MVCWebUI.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public int SubCategoryId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double DiscountedPrice { get; set; }
        public bool CampaignStatus { get; set; }
        public int Stock { get; set; }
        public int QuantitySold { get; set; }
        public string Image { get; set; }
        
        public SubCategory SubCategory { get; set; }
        public CampaignModel Campaign { get; set; }
    }
}