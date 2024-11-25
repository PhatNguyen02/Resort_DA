using System.Web.Mvc;

namespace ResortManagement.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
    "Admin_default",  // Tên route
    "Admin/{controller}/{action}/{id}",
    new { controller = "Home", action = "Index", id = UrlParameter.Optional }
);
        }
    }
}
//using System.Web.Mvc;

//namespace ResortManagement.Areas.Admin
//{
//    public class AdminAreaRegistration : AreaRegistration
//    {
//        public override string AreaName
//        {
//            get
//            {
//                return "Admin";
//            }
//        }

//        public override void RegisterArea(AreaRegistrationContext context)
//        {
//            // Đảm bảo rằng route cho Admin chỉ được đăng ký ở đây
//            context.MapRoute(
//                "Admin_default",  // Tên route cho Admin
//                "Admin/{controller}/{action}/{id}",
//                new { action = "Index", id = UrlParameter.Optional }, // Giá trị mặc định
//                namespaces: new[] { "ResortManagement.Areas.Admin.Controllers" }  // Namespace cho Admin Area
//            );
//        }
//    }
//}

