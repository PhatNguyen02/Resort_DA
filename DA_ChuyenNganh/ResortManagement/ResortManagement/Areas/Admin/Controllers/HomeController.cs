using ResortManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace ResortManagement.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            DB_ResortfEntities context = new DB_ResortfEntities();
            //Tổng doanh thu từ các booking đã thanh toán
            var totalRevenue = context.Invoices
                                      .Where(i => i.IsPaid == true)
                                      .Sum(i => i.TotalAmount);

            // Tổng số booking
            var totalBookings = context.Bookings.Count();

            // Tổng số phòng
            var totalRooms = context.Rooms.Count();

            // Tổng số người dùng
            var totalUsers = context.Users.Count();

            // Tổng số dịch vụ
            var totalServices = context.Services.Count();

            // Tổng số khuyến mãi
            var totalPromotions = context.Promotions.Count();

            // Tổng số phòng đang có booking (đã check-in)
            var occupiedRooms = context.Bookings
                                        .Where(b => b.CheckInDate <= DateTime.Now && b.CheckOutDate >= DateTime.Now)
                                        .Select(b => b.RoomID)
                                        .Distinct()
                                        .Count();

            // Tổng số phòng trống
            var vacantRooms = totalRooms - occupiedRooms;

            DateTime today = DateTime.Now.Date;  // Lấy ngày hiện tại mà không có giờ, phút, giây

            // Tính Revenue ngày hiện tại
            var revenueToday = context.Invoices
                          .Where(i => i.IsPaid == true && DbFunctions.TruncateTime(i.InvoiceDate) == today)
                          .Sum(i => (decimal?)i.TotalAmount) ?? 0;

            // Tính số lượng booking ngày hiện tại
            var bookingsToday = context.Bookings
                                        .Where(b => DbFunctions.TruncateTime(b.BookingDate) == today)
                                        .Count();

            // Thông tin để truyền sang View
            if(totalRevenue > 0)
            {
                ViewBag.TotalRevenue = totalRevenue.HasValue ? totalRevenue.Value.ToString("#,##0" + " $") : "0";

            }
            else
            {
                ViewBag.TotalRevenue = "0 $";
            }    
            ViewBag.TotalBookings = totalBookings;
            ViewBag.TotalRooms = totalRooms;
            ViewBag.TotalUsers = totalUsers;
            ViewBag.TotalServices = totalServices;
            ViewBag.TotalPromotions = totalPromotions;
            ViewBag.OccupiedRooms = occupiedRooms;
            ViewBag.VacantRooms = vacantRooms;
            if(revenueToday > 0)
            {
                ViewBag.RevenueToday = revenueToday.ToString("#,##" + " $");
            }
            else
            {
                ViewBag.RevenueToday = "0 $";
            }    
            
            ViewBag.BookingToday = bookingsToday;

            return View();
        }

    }
}