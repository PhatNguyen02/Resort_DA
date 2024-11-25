using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ResortManagement.Models;
using ResortManagement.Services;
using ResortManagement.ViewModel;
namespace ResortManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookingService _bookingService = new BookingService();
          DB_ResortfEntities dBContext = new DB_ResortfEntities();
        // GET: Home
        public ActionResult MainPage(int page=1 )
        {
            DB_ResortfEntities dBContext = new DB_ResortfEntities();
            List<Rooms> roomList = dBContext.Rooms.ToList();
            
            //Paging
            int NoOfRecordPerPage = 3;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(roomList.Count) / Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordToSkip = (page- 1) * NoOfRecordPerPage;
            ViewBag.Page = page;
            ViewBag.NoOfPage = NoOfPages;
            roomList = roomList.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();
            return View(roomList);
        }

        public ActionResult Detail(int id)
        {
            DB_ResortfEntities dBContext = new DB_ResortfEntities();
            var rooms = dBContext.Rooms.Where(row => row.RoomID == id).FirstOrDefault();

            return View(rooms); // Trả về view hiển thị chi tiết sản phẩm

        }

        //public ActionResult Service
        //public ActionResult Search(SearchVM searchDateTime, int page = 1)
        //{
        //    // Kiểm tra ngày check-in và check-out
        //    if (!_bookingService.CheckBooking(searchDateTime.CheckIn, searchDateTime.CheckOut))
        //    {
        //        ViewBag.Message = "Check-in or check-out date is invalid.";
        //        return RedirectToAction("MainPage");
        //    }

        //    // Lấy danh sách các phòng khả dụng trong khoảng thời gian đã cho
        //    var availableRooms = dBContext.Rooms
        //        .Where(r => r.Status == "Available" &&
        //                    _bookingService.CheckRoomAvailability(r.RoomID, searchDateTime.CheckIn, searchDateTime.CheckOut))
        //        .ToList();

        //    // Phân trang cho kết quả tìm kiếm
        //    int NoOfRecordPerPage = 3;
        //    int NoOfPages = (int)Math.Ceiling((double)availableRooms.Count / NoOfRecordPerPage);
        //    int NoOfRecordToSkip = (page - 1) * NoOfRecordPerPage;

        //    ViewBag.Page = page;
        //    ViewBag.NoOfPage = NoOfPages;
        //    ViewBag.CheckIn = searchDateTime.CheckIn;
        //    ViewBag.CheckOut = searchDateTime.CheckOut;

        //    availableRooms = availableRooms.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();
        //    return View("MainPage", availableRooms);
        //}
        public ActionResult Search(SearchVM searchDateTime, int page = 1)
        {
            // Kiểm tra ngày check-in và check-out
            //if (!_bookingService.CheckBooking(searchDateTime.CheckIn, searchDateTime.CheckOut))
            //{
            //    ViewBag.Message = "Invalid check-in or check-out date.";
            //    return RedirectToAction("MainPage");
            //}

            // Lọc các phòng khả dụng trực tiếp trong truy vấn LINQ
            var availableRooms = dBContext.Rooms
                .Where(r => r.Status == "Available" &&
                            !dBContext.Bookings.Any(b =>
                                b.RoomID == r.RoomID &&
                                b.CheckInDate < searchDateTime.CheckOut &&
                                b.CheckOutDate > searchDateTime.CheckIn))
                .ToList();

            // Phân trang kết quả tìm kiếm
            int NoOfRecordPerPage = 3;
            int NoOfPages = (int)Math.Ceiling((double)availableRooms.Count / NoOfRecordPerPage);
            int NoOfRecordToSkip = (page - 1) * NoOfRecordPerPage;

            ViewBag.Page = page;
            ViewBag.NoOfPage = NoOfPages;
            ViewBag.CheckIn = searchDateTime.CheckIn;
            ViewBag.CheckOut = searchDateTime.CheckOut;

            availableRooms = availableRooms.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();

            return View("MainPage", availableRooms);
        }




        //public ActionResult Search(SearchVM searchDateTime)
        //{
        //    DB_ResortfEntities dBContext = new DB_ResortfEntities();
        //    if (!_bookingService.checkBooking(searchDateTime.CheckIn,searchDateTime.CheckOut))
        //    {
        //        ViewBag.Message = "Check-in or check-out date is FALSE.";
        //        return RedirectToAction("MainPage");
        //    }
        //    else
        //    {
        //        var availableRooms = dBContext.Rooms
        //       .Where(r => r.Status == "Available")
        //       .ToList();
        //        return View("MainPage", availableRooms);
        //    }
        //}
    }
}