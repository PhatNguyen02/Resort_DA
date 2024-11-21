using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResortManagement.Models
{
    public class ServiceItem
    {
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
    }

}