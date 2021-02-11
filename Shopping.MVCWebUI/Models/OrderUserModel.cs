using Shopping.MVCWebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shopping.MVCWebUI.Models
{
    public class OrderUserModel
    {
        public int OrderId { get; set; }

        public string OrderNumber { get; set; }
        public double TotalPrice { get; set; }
        public EnumOrderState OrderState { get; set; }
        public DateTime OrderDate { get; set; }
    }
}