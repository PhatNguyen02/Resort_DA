using ResortManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ResortManagement.Services
{
    public class BookingService
    {
        private readonly DB_ResortfEntities _dbContext = new DB_ResortfEntities();
        public bool checkBooking(DateTime checkin, DateTime checkout)
        {
            if (checkout <= checkin)
            {
                return false;
            }
            return true;
        }

        public int totalday(DateTime checkInDate, DateTime checkOutDate)
        {
            double totalDays = (checkOutDate - checkInDate).TotalDays;

            // Làm tròn lên nếu có phần thập phân (ngày phần thập phân có nghĩa là chưa đến hết một ngày)
            int rentalUnits = (int)Math.Ceiling(totalDays);
            return rentalUnits;
        }

        public Booking GetBookingById(long bookingId)
        {
            // Ví dụ: Truy xuất từ cơ sở dữ liệu
            return _dbContext.Bookings
                .Include(b => b.UsedServices) // Bao gồm dịch vụ (nếu có)
                .FirstOrDefault(b => b.BookingID == bookingId);
        }

        public Room GetRoomByIdBooking(int roomId)
        {
            // Ví dụ: Truy xuất từ cơ sở dữ liệu
            return _dbContext.Rooms
                .FirstOrDefault(b => b.RoomID == roomId);
        }

        public decimal calculateTotal(DateTime checkInDate, DateTime checkOutDate, int  idRoom)
        {
            decimal pricePerDay = _dbContext.Rooms
            .Where(x => x.RoomID == idRoom)
            .Select(x => x.Price)
            .SingleOrDefault() ?? 0m;  // Nếu giá là null, gán 0m (mặc định)


            if (checkBooking(checkInDate,checkOutDate))
            {
                double totalHours = (checkOutDate - checkInDate).TotalHours;

                // Tính số lần thuê (làm tròn lên)
                int rentalUnits = (int)Math.Ceiling(totalHours / 24);

                // Tính tổng tiền
                decimal totalPrice = rentalUnits * pricePerDay;

                return totalPrice;
            }
            else
            {
                return 0;
            }
            
        }

        public bool CheckRoomAvailability(int roomId, DateTime checkIn, DateTime checkOut)
        {
            // Kiểm tra xem phòng có đặt chồng chéo với khoảng thời gian đã chọn hay không
            return !_dbContext.Bookings.Any(b => b.RoomID == roomId &&
                (b.CheckInDate < checkOut && b.CheckOutDate > checkIn));
        }

        public bool CheckBooking(DateTime checkIn, DateTime checkOut)
        {
            // Kiểm tra ngày check-in và check-out có hợp lệ hay không (ví dụ: không phải quá khứ)
            return checkIn < checkOut && checkIn >= DateTime.Now;
        }

        public bool IsRoomAvailable(int roomId, DateTime newCheckIn, DateTime newCheckOut)
        {
            using (var db = new DB_ResortfEntities())
            {
                // Kiểm tra xem có booking nào đã tồn tại với thời gian chồng chéo không
                var overlappingBooking = db.Bookings
                    .Any(b => b.RoomID == roomId &&
                              ((newCheckIn < b.CheckOutDate && newCheckOut > b.CheckInDate) ||
                               (newCheckIn == b.CheckInDate && newCheckOut == b.CheckOutDate)));

                // Trả về true nếu không có trùng lịch
                return !overlappingBooking;
            }
        }



    }
}
