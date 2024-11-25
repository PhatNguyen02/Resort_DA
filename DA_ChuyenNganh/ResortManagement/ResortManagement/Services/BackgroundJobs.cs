using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ResortManagement.Controllers;
namespace ResortManagement.Services
{
    public class BackgroundJobs
    {
        public void SomeBackgroundJob()
        {
            // Logic xử lý công việc nền
            // Ví dụ: Kiểm tra và hủy booking đã hết hạn
            new BookingController().AutoCancelExpiredBookings();
        }

    }
}