using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResortManagement.Models
{
    public class CustomerViewModel
    {
        public bool IsExistingCustomer { get; set; } // indicates if the customer is existing or new
        public string ExistUserPhoneNumber { get; set; } // for existing customer phone number
        public string FullName { get; set; }
        public string NewUserPhoneNumber { get; set; } // for new customer phone number
        public string Email { get; set; }
        public string Address { get; set; }
    }
}