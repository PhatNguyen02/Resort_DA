using ResortManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResortManagement.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            DB_ResortfEntities dBContext = new DB_ResortfEntities();
            List<Booking> book = dBContext.Bookings.ToList();
            return View(book);  // Truyền model cho View
            
        }
    }
}