using Shopping.MVCWebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shopping.MVCWebUI.Models
{
    public class SubCategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EnumIsActiveState IsActive { get; set; }
        public Category Category { get; set; }
        public List<Product> Products { get; set; }
    }
}