using ResortManagement.Models;
using ResortManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResortManagement.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            ViewBag.UserName = Session["User"] as string ?? "Guest";
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginVM lvm)
        {
            using (var DB = new DB_ResortfEntities())
            {
                // Kiểm tra user với Username từ ViewModel
                var checkUser = DB.Users.SingleOrDefault(x => x.Username == lvm.Username);

                //if (checkUser == null || !VerifyPassword(lvm.Password, checkUser.Password)) // Giả sử có phương thức VerifyPassword
                //{
                //    ViewBag.Message = "Username or Password is incorrect";
                //    return View();
                //}

                // Lưu thông tin vào session
                Session["User"] = checkUser.Username;
                Session["UserId"] = checkUser.UserID;
                ViewBag.UserName = Session["User"] as string ?? "Guest";

                // Điều hướng tùy thuộc vào vai trò của người dùng
                return RedirectToAction(checkUser.Role == "Admin" ? "Index" : "MainPage", "Home", new { area = checkUser.Role == "Admin" ? "Admin" : "" });
            }
        }

        public ActionResult Logout()
        {
            // Xóa thông tin người dùng khỏi Session
            Session.Clear(); // Xóa toàn bộ session

            // Hoặc xóa cụ thể từng thông tin nếu cần
            // Session["User"] = null;
            // Session["UserId"] = null;

            // Điều hướng người dùng đến trang đăng nhập hoặc trang chủ
            return RedirectToAction("Login", "Account");
        }

        //public ActionResult Login(LoginVM lvm)
        //{
        //    using (var DB = new DB_ResortfEntities())
        //    {
        //        // Kiểm tra user với Username và Password từ ViewModel
        //        var checkUser = DB.Users
        //                          .SingleOrDefault(x => x.Username == lvm.Username && x.Password == lvm.Password);

        //        if (checkUser == null)
        //        {
        //            // Thông báo lỗi nếu không tìm thấy user
        //            ViewBag.Message = "Username or Password is incorrect";
        //            return View();
        //        }

        //        // Lấy vai trò của người dùng
        //        var roleUser = checkUser.Role;

        //        // Điều hướng tùy thuộc vào vai trò của người dùng
        //        if (roleUser == "Admin")
        //        {
        //            Session["User"] = checkUser.Username;
        //            Session["UserId"] = checkUser.UserID;
        //            ViewBag.UserName = Session["User"] as string ?? "Guest";
        //            return RedirectToAction("Index", "Home", new { area = "Admin" });
        //        }
        //        else
        //        {
        //            Session["User"] = checkUser.Username; // Đảm bảo lưu thông tin user vào session
        //            ViewBag.UserName = Session["User"] as string ?? "Guest";
        //            return RedirectToAction("MainPage", "Home");
        //        }
        //    }
        //}


        public ActionResult Register()
        {
            return View();
        }
    }
}