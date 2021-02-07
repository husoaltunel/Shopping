using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shopping.MVCWebUI.Entity
{
    public class Category
    {
        public int Id { get; set; }
        [DisplayName("Kategori Adı")]
        [StringLength(20,ErrorMessage = "Kategori adları en fazla 20 karakter içermelidir",MinimumLength = 2)]
        public string Name { get; set; }
        [DisplayName("Açıklama")]
        public string Description { get; set; }
        public virtual List<SubCategory> SubCategories { get; set; }
    }
}