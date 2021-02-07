using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Shopping.MVCWebUI.Entity
{
    public class Campaign
    {
        public int Id { get; set; }
        [DisplayName("Kampanya Adı")]
        public string Name { get; set; }
        [DisplayName("Bitiş Tarihi")]
        public DateTime DueDate { get; set; }
        [DisplayName("İndirim Yüzdesi % ")]
        public int DiscountPercent { get; set; }

        public virtual List<Product> Products { get; set; }


    }
}