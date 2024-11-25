using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResortManagement.Models
{
    public class AddBookingViewModel
    {
        public bool IsExistingCustomer { get; set; }
        public string ExistingCustomerPhoneNumber { get; set; } 
        public string NewCustomerPhoneNumber { get; set; }

        // For new customers
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        // Booking Details
        public int RoomID { get; set; }
        public DateTime CheckInDate { get; set; } = DateTime.Now;
        public DateTime CheckOutDate { get; set; } = DateTime.Now.AddDays(1);
        public int? PromotionID { get; set; }
        public string PaymentMethod { get; set; }

        public List<Rooms> Rooms { get; set; } = new List<Rooms>();
        public List<Services> Services { get; set; }

        // Services
        public List<SelectedServiceViewModel> SelectedServices { get; set; } = new List<SelectedServiceViewModel> { };
    }

    public class SelectedServiceViewModel
    {
        public int ServiceID { get; set; }
        public int Quantity { get; set; }
    }

}