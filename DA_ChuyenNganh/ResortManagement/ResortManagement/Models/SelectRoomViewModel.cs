using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResortManagement.Models
{
    public class SelectRoomViewModel
    {
        public List<Rooms> AvailableRooms {  get; set; }
        public int UserID { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int BookingID { get; set; }
    }
}