using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shopping.MVCWebUI.Entity
{
    public class SubCategory 
    {
        public int Id { get; set; }
        [DisplayName("Alt Kategori Adı")]     
        public string Name { get; set; }
        [DisplayName("Açıklama")]
        public string Description { get; set; }
        
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}