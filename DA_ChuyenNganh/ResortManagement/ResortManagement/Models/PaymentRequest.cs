using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResortManagement.Models
{
    public class PaymentRequest
    {
        public int BookingId { get; set; }
        public List<ServiceItem> Services { get; set; }
        public decimal TotalAmount { get; set; }
    }


    //public class ServiceDto
    //{
    //    public int ServiceID { get; set; }
    //    public string ServiceName { get; set; }
    //    public decimal Price { get; set; }
    //    public int Quantity { get; set; }
    //}
}
