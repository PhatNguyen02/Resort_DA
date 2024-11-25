using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Hangfire;
using ResortManagement.Controllers;
using ResortManagement.Services;
using Owin;
namespace ResortManagement
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            string connectionString = ConfigurationManager.ConnectionStrings["HangfireConnection"].ConnectionString;

            // Cấu hình Hangfire để sử dụng SQL Server làm lưu trữ công việc
            GlobalConfiguration.Configuration.UseSqlServerStorage(connectionString);

            // Cấu hình các công việc định kỳ trong Hangfire (nếu cần)
            RecurringJob.AddOrUpdate(
                "auto-cancel-bookings",
                () => new BackgroundJobs().SomeBackgroundJob(),
                Cron.Minutely); // Ví dụ: Mỗi phút

            //// Các cấu hình khác của ứng dụng
            //AreaRegistration.RegisterAllAreas();
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
//using Hangfire;
//using Hangfire.SqlServer;
//using ResortManagement.Services;
//using ResortManagement;
//using System.Configuration;
//using System.Web.Mvc;
//using System.Web.Routing;

//public class MvcApplication : System.Web.HttpApplication
//{
//    protected void Application_Start()
//    {
//        // Đăng ký tất cả các area và route
//        AreaRegistration.RegisterAllAreas();
//        RouteConfig.RegisterRoutes(RouteTable.Routes);

//        // Cấu hình Hangfire không sử dụng OWIN
//        string connectionString = ConfigurationManager.ConnectionStrings["HangfireConnection"].ConnectionString;
//        GlobalConfiguration.Configuration.UseSqlServerStorage(connectionString);

//        // Định kỳ công việc (ví dụ: tự động hủy các đơn đặt phòng)
//        RecurringJob.AddOrUpdate(
//            "auto-cancel-bookings",
//            () => new BackgroundJobs().SomeBackgroundJob(),
//            Cron.Minutely); // Ví dụ: mỗi phút
//    }
//}
