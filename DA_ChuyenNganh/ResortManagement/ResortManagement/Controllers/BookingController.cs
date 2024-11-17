using ResortManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ResortManagement.Services;

namespace ResortManagement.Controllers
{
    public class BookingController : Controller
    {
        private readonly BookingService _bookingService = new BookingService();

        // GET: Booking
        [HttpPost]
        //public ActionResult BookingRoom(int idRoom, int idUser, string checkIn, string checkOut , decimal price)
        //{
        //    DateTime checkInDate, checkOutDate;
        //    string status = "Pending";
        //    // Chuyển đổi dữ liệu từ chuỗi sang DateTime
        //    if (!DateTime.TryParse(checkIn, out checkInDate) || !DateTime.TryParse(checkOut, out checkOutDate))
        //    {
        //        ViewBag.Message = "Invalid date format.";
        //        return RedirectToAction("MainPage");
        //    }

        //    // Thiết lập thời gian cố định cho check-in và check-out
        //    checkInDate = checkInDate.Date.AddHours(14);  // Check-in lúc 14h
        //    checkOutDate = checkOutDate.Date.AddHours(12); // Check-out lúc 12h

        //    // Kiểm tra: Ngày check-out phải sau check-in
        //    if (checkOutDate <= checkInDate)
        //    {
        //        ViewBag.Message = "Check-out date must be later than check-in date.";
        //        return RedirectToAction("MainPage");
        //    }

        //    // Kiểm tra xem phòng có trùng lịch không
        //    if (!_bookingService.IsRoomAvailable(idRoom, checkInDate, checkOutDate))
        //    {
        //        ViewBag.Message = "Room is already booked during this period.";
        //        return RedirectToAction("MainPage");
        //    }

        //    using (var db = new DB_ResortfEntities())
        //    {
        //        var room = db.Rooms.FirstOrDefault(r => r.RoomID == idRoom);
        //        var user = db.Users.Find(idUser);

        //        if (room == null || user == null)
        //        {
        //            ViewBag.Message = "Invalid Room or User ID.";
        //            return RedirectToAction("MainPage");
        //        }

        //        // Tạo booking mới
        //        var booking = new Booking
        //        {
        //            RoomID = room.RoomID,
        //            UserID = user.UserID,
        //            BookingDate = DateTime.Now,
        //            CheckInDate = checkInDate,
        //            CheckOutDate = checkOutDate,
        //            TotalPrice = price, // Giá có thể tính toán dựa vào thời gian thuê
        //            PaymentType = "Prepaid",
        //            Status = status
        //        };

        //        try
        //        {
        //            db.Bookings.Add(booking);
        //            db.SaveChanges();
        //            ViewBag.Message = "Booking successful!";
        //        }
        //        catch (Exception ex)
        //        {
        //            ViewBag.Message = "An error occurred while booking. Please try again.";
        //        }

        //        return RedirectToAction("MainPage","Home");
        //    }
        //}

        public ActionResult BookingRoom(int idRoom, int idUser, string checkIn, string checkOut, decimal idPrice)
        {
            DateTime checkInDate, checkOutDate;
            string status = "Pending";
            // Chuyển đổi dữ liệu từ chuỗi sang DateTime
            if (!DateTime.TryParse(checkIn, out checkInDate) || !DateTime.TryParse(checkOut, out checkOutDate))
            {
                ViewBag.Message = "Invalid date format.";
                return RedirectToAction("MainPage","Home");
            }

            // Thiết lập thời gian cố định cho check-in và check-out
            checkInDate = checkInDate.Date.AddHours(14);  // Check-in lúc 14h
            checkOutDate = checkOutDate.Date.AddHours(12); // Check-out lúc 12h

            // Kiểm tra: Ngày check-out phải sau check-in
            if (checkOutDate <= checkInDate)
            {
                ViewBag.Message = "Check-out date must be later than check-in date.";
                return RedirectToAction("MainPage", "Home");
            }

            // Kiểm tra xem phòng có trùng lịch không
            if (!_bookingService.IsRoomAvailable(idRoom, checkInDate, checkOutDate))
            {
                ViewBag.Message = "Room is already booked during this period.";
                return RedirectToAction("MainPage", "Home");
            }

            using (var db = new DB_ResortfEntities())
            {
                var room = db.Rooms.FirstOrDefault(r => r.RoomID == idRoom);
                var user = db.Users.Find(idUser);

                if (room == null || user == null)
                {
                    ViewBag.Message = "Invalid Room or User ID.";
                    return RedirectToAction("MainPage", "Home");
                }

                // Tạo booking mới
                var booking = new Booking
                {
                    RoomID = room.RoomID,
                    UserID = user.UserID,
                    BookingDate = DateTime.Now,
                    CheckInDate = checkInDate,
                    CheckOutDate = checkOutDate,
                    TotalPrice = idPrice, // Giá có thể tính toán dựa vào thời gian thuê
                    PaymentType = "Prepaid",
                    Status = status
                };

                try
                {
                    db.Bookings.Add(booking);
                    db.SaveChanges();
                    ViewBag.Message = "Booking successful!";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "An error occurred while booking. Please try again.";
                }

                return RedirectToAction("MainPage","Home");
            }
        }


        public ActionResult SelectService(int bookingId=13)
        {
         
        
            using (var db = new DB_ResortfEntities())
            {
               
               var booking = db.Bookings
                               .Include("User")
                               .Include("Room")
                               .FirstOrDefault(b => b.BookingID == bookingId);

                if (booking == null)
                {
                    ViewBag.Message = "Không tìm thấy thông tin đặt phòng.";
                    return RedirectToAction("MainPage");
                }

                // Lấy danh sách dịch vụ
                var services = db.Services.Where(s => s.StatusServices == "Available").ToList();
                List<Service> service = db.Services.ToList();
                // Truyền dữ liệu sang View
                //int totalday = _bookingService.totalday(booking.CheckInDate, booking.CheckOutDate);
                // Tính tổng số ngày
                int totalDay = _bookingService.totalday(booking.CheckInDate.Value, booking.CheckOutDate.Value);
       
                ViewBag.Totalday = totalDay;
                ViewBag.BookingId = bookingId;
                ViewBag.BookingPrice = booking.TotalPrice;
                //ViewBag.Services = services;

                return View(service);
            }
        }

        public void AutoCancelExpiredBookings()
        {
            using (var db = new DB_ResortfEntities())
            {
                var expiredBookings = db.Bookings
                                        .Where(b => b.Status == "Pending" && b.BookingDate < DateTime.Now.AddHours(-24))
                                        .ToList();

                foreach (var booking in expiredBookings)
                {
                    db.Bookings.Remove(booking);
                }

                db.SaveChanges();
            }
        }




        //public ActionResult BookingRoom(int idRoom, int idUser, string checkIn, string checkOut)
        //{
        //    // Chuyển đổi dữ liệu từ form thành DateTime
        //    DateTime checkInDate;
        //    DateTime checkOutDate;

        //    // Kiểm tra và chuyển đổi định dạng ngày
        //    if (!DateTime.TryParse(checkIn, out checkInDate) || !DateTime.TryParse(checkOut, out checkOutDate))
        //    {
        //        ViewBag.Message = "Invalid date format.";
        //        return RedirectToAction("MainPage");
        //    }

        //    // Thiết lập thời gian check-in và check-out
        //    checkInDate = checkInDate.Date.AddHours(14); // Check-in lúc 14h
        //    checkOutDate = checkOutDate.Date.AddHours(12); // Check-out lúc 12h

        //    using (var db = new DB_ResortfEntities())
        //    {
        //        if (!_bookingService.CheckBooking(checkInDate, checkOutDate))
        //        {
        //            ViewBag.Message = "Check-in or check-out date is invalid.";
        //            return RedirectToAction("MainPage");
        //        }
        //        else
        //        {
        //            var room = db.Rooms.FirstOrDefault(r => r.RoomID == idRoom);
        //            var user = db.Users.Find(idUser);

        //            if (room == null || user == null)
        //            {
        //                ViewBag.Message = "Invalid Room or User ID";
        //                return RedirectToAction("MainPage");
        //            }

        //            // Tạo đối tượng booking
        //            var booking = new Booking
        //            {
        //                RoomID = room.RoomID,
        //                UserID = user.UserID,
        //                BookingDate = DateTime.Now,
        //                CheckInDate = checkInDate,
        //                CheckOutDate = checkOutDate,
        //                TotalPrice = 520000, // Có thể tính toán từ các yếu tố khác
        //                PaymentType = "Prepaid",
        //                Status = "Confirmed"
        //            };

        //            try
        //            {
        //                db.Bookings.Add(booking);
        //                db.SaveChanges();
        //                ViewBag.Message = "Booking successful!";
        //            }
        //            catch (Exception ex)
        //            {
        //                // Ghi log lỗi ở đây nếu cần
        //                ViewBag.Message = "An error occurred while booking. Please try again.";
        //            }
        //        }

        //        return RedirectToAction("MainPage");
        //    }
        //}
        //public ActionResult BookingRoom(int idRoom, int idUser, Booking booking)
        //{
        //    DateTime checkIn = booking.CheckInDate ?? DateTime.Now;
        //    DateTime checkOut = checkIn.AddDays(7);

        //    using (var db = new DB_ResortfEntities())
        //    {
        //        if(!_bookingService.checkBooking(checkIn, checkOut))
        //        {
        //            ViewBag.Message = "Check-in or check-out date is FALSE.";
        //            return RedirectToAction("Login");
        //        }   
        //        else
        //        {
        //            var room = db.Rooms.FirstOrDefault(r => r.RoomID == idRoom);
        //            var user = db.Users.Find(idUser);

        //            if (room == null || user == null)
        //            {
        //                ViewBag.Message = "Invalid Room or User ID";
        //                return RedirectToAction("MainPage");
        //            }

        //            // Gán thông tin cho đối tượng booking
        //            booking.RoomID = room.RoomID;
        //            booking.UserID = user.UserID; // Chỉ cần xác nhận người dùng đã tồn tại
        //            booking.Promotion = null; // Hoặc lấy từ nơi khác nếu có
        //            booking.BookingDate = DateTime.Now;
        //            booking.CheckInDate = booking.CheckInDate; // Lấy từ input
        //            booking.CheckOutDate = booking.CheckOutDate; // Lấy từ input
        //            booking.TotalPrice = 520000; // Có thể tính toán từ các yếu tố khác
        //            booking.PaymentType = "Prepaid";
        //            booking.Status = "Confirmed";

        //            try
        //            {
        //                db.Bookings.Add(booking); // Đảm bảo thêm đối tượng booking
        //                db.SaveChanges();
        //                ViewBag.Message = "Booking successful!";
        //            }
        //            catch (Exception ex)
        //            {
        //                // Ghi log lỗi ở đây nếu cần
        //                ViewBag.Message = "An error occurred while booking. Please try again.";
        //            }
        //        }

        //        return RedirectToAction("MainPage");
        //    }
        //        // Kiểm tra xem phòng và người dùng có tồn tại không

        //}

    }



}