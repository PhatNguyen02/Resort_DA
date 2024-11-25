using PayPal.Api;
using ResortManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResortManagement.Areas.Admin.Controllers
{
    public class UserController : Controller
    {

        // GET: Admin/User
        public ActionResult Index(string search)
        {
            DB_ResortfEntities context = new DB_ResortfEntities();
            List<Users> users;


            if (string.IsNullOrEmpty(search))
            {
                users = context.Users.ToList();
            }
            else
        {
                users = context.Users
                .Where(p => p.Username.Contains(search))
                    .ToList();
            }

            ViewBag.Search = search;
            return View(users);
        }


        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(Users user)
        {
            DB_ResortfEntities _context = new DB_ResortfEntities();
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult DeleteUser(int id)
        {
            DB_ResortfEntities _context = new DB_ResortfEntities();
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }


            return View(user);
        }

        [HttpPost, ActionName("DeleteUser")]
        public ActionResult DeleteUserConfirmed(int id)
        {
            DB_ResortfEntities _context = new DB_ResortfEntities();
            var user = _context.Users.Find(id);


            if (user == null)
            {
                return HttpNotFound();
            }


            _context.Users.Remove(user);
            _context.SaveChanges();


            return RedirectToAction("Index");
        }

        public ActionResult EditUser(int id)
        {
            DB_ResortfEntities _context = new DB_ResortfEntities();
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult EditUser(Users user)
        {
            if (ModelState.IsValid)
            {
                using (DB_ResortfEntities _context = new DB_ResortfEntities())
                {
                    var existingUser = _context.Users.Find(user.UserID);
                    if (existingUser == null)
                    {
                        return HttpNotFound();
                    }

                    existingUser.FullName = user.FullName;
                    existingUser.Email = user.Email;
                    existingUser.PhoneNumber = user.PhoneNumber;
                    existingUser.Address = user.Address;
                    existingUser.Username = user.Username;
                    existingUser.Password = user.Password;
                    existingUser.Role = user.Role;

                    _context.Entry(existingUser).State = System.Data.Entity.EntityState.Modified;
                    _context.SaveChanges();

                    return RedirectToAction("Index", "User");
                }
            }
            return View(user);
        }
    }
}