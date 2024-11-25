using System.Collections.Generic;

namespace ResortManagement.Models
{
    public class InvoiceVM
    {
        public int BookingID { get; set; }
        public Rooms Room { get; set; }
        public List<ServiceVM> Services { get; set; }
        public decimal RoomTotal { get; set; }
        public decimal ServiceTotal { get; set; }
        public decimal GrandTotal { get; set; }
        public string PaymentMethod { get; set; }
    }
}
