using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web;
namespace ResortManagement.ViewModel
{
    public class ZaloPayConfiguration
    {
        public string AppId { get; set; }
        public string AppSecret { get; set; }
        public string ReturnUrl { get; set; }
        public string NotifyUrl { get; set; }
        public string CreateOrderUrl { get; set; }
        public string QueryOrderUrl { get; set; }

        public ZaloPayConfiguration()
        {
            AppId = "2553";  // AppId của bạn
            AppSecret = "PcY4iZIKFCIdgZvA6ueMcMHHUbRLYjPL"; // AppSecret của bạn

            // Cấu hình các URL động
            ReturnUrl = $"{HttpContext.Current.Request.Url.Scheme}://{HttpContext.Current.Request.Url.Authority}/Invoice/PaymentSuccess";
            NotifyUrl = $"{HttpContext.Current.Request.Url.Scheme}://{HttpContext.Current.Request.Url.Authority}/Invoice/PaymentNotify";

            CreateOrderUrl = "https://sb-openapi.zalopay.vn/v2/create";
            QueryOrderUrl = "https://sb-openapi.zalopay.vn/v2/query";
        }
    }

}

