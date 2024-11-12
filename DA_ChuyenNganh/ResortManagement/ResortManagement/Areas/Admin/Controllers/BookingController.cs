using ResortManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResortManagement.Areas.Admin.Controllers
{
    public class BookingController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            DB_ResortfEntities context = new DB_ResortfEntities();
            List<Booking> book = context.Bookings.ToList();
            return View(book);  
        }

        public ActionResult EditBooking(int id)
        {
            DB_ResortfEntities context = new DB_ResortfEntities();
            Booking book = context.Bookings.Find(id);

            if(book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        
    }
}