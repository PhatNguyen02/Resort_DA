using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResortManagement.Services
{
    public class ZaloPayService
    {
        public string CreatePayment(long bookingId, decimal totalAmount)
        {
            // Logic tạo request thanh toán đến ZaloPay
            // Tạo URL redirect đến cổng thanh toán ZaloPay
            string redirectUrl = $"https://sandbox.zalopay.vn/?bookingId={bookingId}&amount={totalAmount}";
            return redirectUrl;
        }

        public PaymentResult VerifyPayment(string transactionId, string token)
        {
            // Logic xác thực giao dịch với ZaloPay
            // Gửi request đến API của ZaloPay để kiểm tra giao dịch
            return new PaymentResult
            {
                IsSuccess = true,
                BookingId = 12345 // BookingId được trả về từ ZaloPay
            };
        }
    }

    public class PaymentResult
    {
        public bool IsSuccess { get; set; }
        public long BookingId { get; set; }
    }
}
