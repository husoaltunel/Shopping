using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shopping.MVCWebUI.Models
{
    public class MailDetails
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string AddressTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Neighborhood { get; set; }
        public string ZipCode { get; set; }

    }
}