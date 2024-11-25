using ResortManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResortManagement.Areas.Admin.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Admin/Profile
        public ActionResult Info(string UserName)
        {
            DB_ResortfEntities context = new DB_ResortfEntities();
            var user = context.Users.SingleOrDefault(u => u.Username.Equals(UserName));
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
    }
}