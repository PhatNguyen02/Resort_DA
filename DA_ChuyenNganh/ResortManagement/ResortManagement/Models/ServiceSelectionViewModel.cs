using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResortManagement.Models
{
    public class ServiceSelectionViewModel
    {
        public int BookingID { get; set; }
        public List<int> SelectedServiceIDs { get; set; } // ID của các dịch vụ được chọn
        public Dictionary<int, int> ServiceQuantities { get; set; }
    }

    public class ServiceVM
    {
        public int ServiceID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}