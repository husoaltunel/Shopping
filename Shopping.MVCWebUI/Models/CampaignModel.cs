
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Shopping.MVCWebUI.Models
{
    public class CampaignModel
    {
        public int Id { get; set; }
        [DisplayName("Bitiş Tarihi")]
        public DateTime? DueDate { get; set; }
        [DisplayName("İndirim Yüzdesi %")]
        public int? DiscountPercent { get; set; }

    }
}