using ResortManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using X.PagedList.Extensions;


namespace ResortManagement.Areas.Admin.Controllers
{
    public class BookingController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index(string search)
        {
            DB_ResortfEntities context = new DB_ResortfEntities();

            List<Bookings> bookings;

            if (string.IsNullOrEmpty(search))
            {
                bookings = context.Bookings.ToList();
            }
            else
            {

                bookings = context.Bookings
                    .Where(b => b.Users.FullName.Contains(search)) 
                    .ToList();
                ViewBag.Search = search;
            }

            return View(bookings);
        }

        //Tìm khách hàng
        public ActionResult CustomerSelection()
        {
            return View(new CustomerViewModel());
        }

        [HttpPost]
        public ActionResult IdentifyOrCreateCustomer(CustomerViewModel model)
        {
            DB_ResortfEntities context = new DB_ResortfEntities();
            if (model.IsExistingCustomer)
            {
                var user = context.Users.FirstOrDefault(u => u.PhoneNumber == model.ExistUserPhoneNumber);
                if (user != null)
                {
                    return RedirectToAction("RoomSelection", new { userId = user.UserID });
                }
                ModelState.AddModelError("", "The customer does not exist.");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var newUser = new Users
                    {
                        FullName = model.FullName,
                        Email = model.Email,
                        PhoneNumber = model.NewUserPhoneNumber,
                        Address = model.Address,
                        Username = GenerateUsername(model.FullName),
                        Password = "123",
                        RegistrationDate = DateTime.Now,
                        Role = "Customer"
                    };
                    context.Users.Add(newUser);
                    context.SaveChanges();
                    return RedirectToAction("RoomSelection", new { userId = newUser.UserID });
                }
            }
            return View("CustomerSelection", model);
        }
        //Tạo UserName
        private string GenerateUsername(string fullName)
        {
            var nameParts = fullName.Split(' ');
            var username = string.Join("", nameParts.Select(n => n.Substring(0, 1))).ToLower();
            var currentTime = DateTime.Now.ToString("HHmmss");

            username += currentTime;

            return username;
        }
        //Chon phong
        [HttpGet]
        [Route("Booking/RoomSelection/{userId}")]
        public ActionResult RoomSelection(int userId)
        {
            DB_ResortfEntities context = new DB_ResortfEntities();

            // Lấy các phòng trống (Available)
            var tomorrow = DateTime.Today.AddDays(1);
            var availableRooms = context.Rooms
                .Where(r => r.Status == "Available" &&
                            !r.Bookings.Any(b =>
                                b.CheckInDate == DateTime.Today &&
                                b.CheckOutDate == tomorrow))
                .ToList();


            var model = new SelectRoomViewModel
            {
                AvailableRooms = availableRooms,
                UserID = userId,
                CheckInDate = DateTime.Today,
                CheckOutDate = DateTime.Today.AddDays(1) 
            };

            return View(model);
        }

        //Lấy phòng theo ngày chkin, chout
        public ActionResult GetAvailableRooms(string checkInDate, string checkOutDate)
        {
            DateTime checkIn = DateTime.ParseExact(checkInDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime checkOut = DateTime.ParseExact(checkOutDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            Console.WriteLine("CheckInDate: " + checkIn.ToString("yyyy-MM-dd"));
            Console.WriteLine("CheckOutDate: " + checkOut.ToString("yyyy-MM-dd"));

            using (DB_ResortfEntities context = new DB_ResortfEntities())
            {
                var availableRooms = context.Rooms
                    .Where(r => r.Status == "Available" &&
                                !r.Bookings.Any(b =>
                                    (b.CheckInDate < checkOut && b.CheckOutDate > checkIn)))
                    .ToList();

                return PartialView("GetAvailableRooms", availableRooms);
            }
        }
        //public ActionResult GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate)
        //{
        //    DB_ResortfEntities context = new DB_ResortfEntities();

        //    // Lọc các phòng còn trống
        //    var availableRooms = context.Room
        //            .Where(r => r.Status == "Available" &&
        //                        !r.Booking.Any(b =>
        //                            (b.CheckInDate < checkOutDate && b.CheckOutDate > checkInDate)))
        //            .ToList();

        //    // Trả về phần tử cần thiết (chỉ danh sách phòng)
        //    return PartialView("GetAvailableRooms", availableRooms);
        //}

        [HttpPost]
        public ActionResult CreateBooking(BookingViewModel model)
        {
            DB_ResortfEntities context = new DB_ResortfEntities();

            // Kiểm tra giá trị RoomID
            if (model.RoomID == 0) // Hoặc kiểm tra null nếu RoomID là nullable
            {
                ModelState.AddModelError("", "RoomID is required.");
            }
            // Xử lý logic cho Check Out
            if (Request["IsCheckoutOnly"] == "true")
            {
                var room = context.Rooms.FirstOrDefault(r => r.RoomID == model.RoomID && r.Status == "Available");
                if (room != null)
                {
                    decimal RoomPrice = CalculateRoomPrice(model.CheckInDate, model.CheckOutDate, room.Price ?? 0);
                    var booking = new Bookings
                    {
                        UserID = model.UserID,
                        RoomID = model.RoomID,
                        BookingDate = DateTime.Today,
                        CheckInDate = model.CheckInDate,
                        CheckOutDate = model.CheckOutDate,
                        TotalPrice = RoomPrice,
                        Status = "Confirmed"
                    };

                    context.Bookings.Add(booking);
                    context.SaveChanges();

                    // Điều hướng sang trang Checkout
                    return RedirectToAction("Checkout", new { bookingId = booking.BookingID });
                }
                else
                {
                    ModelState.AddModelError("", "Room is not available.");
                }
            }

            // Xử lý logic cho Select Service
            if (ModelState.IsValid)
            {
                var room = context.Rooms.FirstOrDefault(r => r.RoomID == model.RoomID && r.Status == "Available");
                if (room != null)
                {
                    decimal RoomPrice = CalculateRoomPrice(model.CheckInDate, model.CheckOutDate, room.Price ?? 0);
                    var booking = new Bookings
                    {
                        UserID = model.UserID,
                        RoomID = model.RoomID,
                        BookingDate = DateTime.Today,
                        CheckInDate = model.CheckInDate,
                        CheckOutDate = model.CheckOutDate,
                        TotalPrice = RoomPrice,
                        Status = "Confirmed"
                    };

                    context.Bookings.Add(booking);
                    context.SaveChanges();

                    return RedirectToAction("ServiceSelection", new { bookingId = booking.BookingID });
                }
                else
                {
                    ModelState.AddModelError("", "Room is not available.");
                }
            }

            // Nếu có lỗi, trả lại View RoomSelection với SelectRoomViewModel
            var selectRoomViewModel = new SelectRoomViewModel
            {
                UserID = model.UserID,
                CheckInDate = model.CheckInDate,
                CheckOutDate = model.CheckOutDate,
                AvailableRooms = context.Rooms.Where(r => r.Status == "Available").ToList()
            };

            return View("RoomSelection", selectRoomViewModel);

        }

        //Tính TotalPrice
        public decimal CalculateRoomPrice(DateTime checkInDate, DateTime checkOutDate, decimal roomPricePerDay)
        {
            // Kiểm tra nếu CheckOutDate trước CheckInDate
            if (checkOutDate <= checkInDate)
            {
                throw new ArgumentException("Check-out date must be after check-in date.");
            }

            // Tính số ngày ở (số đêm lưu trú)
            int numberOfDays = (checkOutDate - checkInDate).Days;

            // Tính tổng tiền
            decimal totalRoomPrice = numberOfDays * roomPricePerDay;

            return totalRoomPrice;
        }

        //Chọn service
        public ActionResult ServiceSelection(int bookingId)
        {
            var context = new DB_ResortfEntities();

            // Lấy danh sách dịch vụ từ cơ sở dữ liệu
            var services = context.Services.Select(s => new ServiceVM
            {
                ServiceID = s.ServiceID,
                Name = s.ServiceName,
                Price = s.Price ?? 0
            }).ToList();

            // Sử dụng ViewBag để truyền danh sách dịch vụ đến view
            ViewBag.Services = services;

            // Chuẩn bị ViewModel
            var model = new ServiceSelectionViewModel
            {
                BookingID = bookingId,
                SelectedServiceIDs = new List<int>(), // Khởi tạo danh sách rỗng
                ServiceQuantities = new Dictionary<int, int>() // Khởi tạo từ điển rỗng
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult AddServices(ServiceSelectionViewModel model)
        {
            using (var context = new DB_ResortfEntities())
            {
                // Kiểm tra BookingID có tồn tại trong bảng Booking không
                var isValidBooking = context.Bookings.Any(b => b.BookingID == model.BookingID);
                if (!isValidBooking)
                {
                    ModelState.AddModelError("", "Booking không tồn tại.");
                    return View("ServiceSelection", model);
                }

                // Thêm từng dịch vụ vào bảng UsedService
                foreach (var serviceId in model.SelectedServiceIDs)
                {
                    var service = context.Services.FirstOrDefault(s => s.ServiceID == serviceId);
                    if (service != null)
                    {
                        if (model.ServiceQuantities == null)
                        {
                            model.ServiceQuantities = new Dictionary<int, int>();
                        }

                        var usedService = new UsedServices
                        {
                            BookingID = model.BookingID, // Đảm bảo BookingID hợp lệ
                            ServiceID = serviceId,
                            Quantity = model.ServiceQuantities.ContainsKey(serviceId) ? model.ServiceQuantities[serviceId] : 1,
                            TotalPrice = service.Price * (model.ServiceQuantities.ContainsKey(serviceId) ? model.ServiceQuantities[serviceId] : 1)
                        };
                        context.UsedServices.Add(usedService);
                    }
                }

                context.SaveChanges();
            }

            return RedirectToAction("Checkout", "Booking", new { bookingId = model.BookingID });
        }



        //Invoice
        public ActionResult Checkout(int bookingId)
        {
            using (var context = new DB_ResortfEntities())
            {
                // Lấy thông tin Booking
                var booking = context.Bookings.FirstOrDefault(b => b.BookingID == bookingId);

                if (booking == null)
                {
                    return HttpNotFound("Booking not found");
                }

                // Lấy thông tin Room
                var room = context.Rooms.FirstOrDefault(r => r.RoomID == booking.RoomID);

                if (room == null)
                {
                    return HttpNotFound("Room not found");
                }

                // Lấy thông tin dịch vụ đã chọn
                var services = context.UsedServices
                    .Where(us => us.BookingID == bookingId)
                    .Select(us => new ServiceVM
                    {
                        Name = us.Services.ServiceName,
                        Price = us.TotalPrice ?? 0
                    }).ToList();

                // Tính tổng giá
                decimal roomTotal = booking.TotalPrice ?? 0;
                decimal serviceTotal = services.Any() ? services.Sum(s => s.Price) : 0; // Giá trị dịch vụ hoặc 0
                decimal grandTotal = roomTotal + serviceTotal;

                // Tạo ViewModel
                var model = new InvoiceVM
                {
                    BookingID = bookingId,
                    Room = room,
                    RoomTotal = roomTotal,
                    Services = services, // Có thể rỗng nếu không chọn dịch vụ
                    GrandTotal = grandTotal
                };

                // Gửi thông báo nếu không có dịch vụ được chọn
                if (!services.Any())
                {
                    ViewBag.NoServicesMessage = "You have not selected any additional services.";
                }

                return View(model);
            }

        }

        [HttpPost]
        public ActionResult ConfirmPayment(InvoiceVM model)
        {
            DB_ResortfEntities context = new DB_ResortfEntities();
            var invoice = new Invoices
            {
                BookingID = model.BookingID,
                InvoiceDate = DateTime.Now,
                TotalAmount = model.GrandTotal,
                IsPaid = true,
                PaymentStatus = "Paid",
                PaymentMethod = model.PaymentMethod
            };
            context.Invoices.Add(invoice);
            context.SaveChanges();
            return RedirectToAction("BookingSuccess");
        }

        public ActionResult BookingSuccess()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EditBooking(Bookings booking)
        {
            if (ModelState.IsValid)
            {
                using (DB_ResortfEntities _context = new DB_ResortfEntities())
                {
                    var book = _context.Bookings.Find(booking.BookingID);
                    if (book == null)
                    {
                        return HttpNotFound();
                    }

                    book.UserID = booking.UserID;
                    book.RoomID = booking.RoomID;
                    book.PromotionID = booking.PromotionID;
                    book.BookingDate = booking.BookingDate;
                    book.CheckOutDate = booking.CheckOutDate;
                    book.TotalPrice = booking.TotalPrice;
                    book.Status = booking.Status;

                    _context.Entry(book).State = System.Data.Entity.EntityState.Modified;
                    _context.SaveChanges();

                    return RedirectToAction("Index", "Promotion");
                }
            }
            return View(booking);
        }

        public ActionResult DeleteBooking(int id)
        {
            DB_ResortfEntities _context = new DB_ResortfEntities();
            var book = _context.Bookings.Find(id);

            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        [HttpPost, ActionName("DeleteBooking")]
        public ActionResult ConfirmDeleteBooking(int id)
        {
            DB_ResortfEntities _context = new DB_ResortfEntities();
            var book = _context.Bookings.Find(id);


            if (book == null)
            {
                return HttpNotFound();
            }


            _context.Bookings.Remove(book);
            _context.SaveChanges();


            return RedirectToAction("Index");
        }

        
    }

    
}