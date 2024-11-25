using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResortManagement.Models
{
    public class InvoiceDetailViewModel
    {
        
        
        public Invoices Invoice { get; set; }
        public Bookings Booking { get; set; }
        public Rooms Room { get; set; }
        public List<UsedServiceViewModel> UsedServices { get; set; }
    }

    public class UsedServiceViewModel
    {
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }

}