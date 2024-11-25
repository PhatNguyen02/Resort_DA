using ResortManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;

namespace ResortManagement.Areas.Admin.Controllers
{
    public class InvoiceController : Controller
    {
        public ActionResult PrintInvoice(int id)
        {
            DB_ResortfEntities context = new DB_ResortfEntities();

            var booking = context.Bookings.Find(id);

            if (booking == null)
            {
                return HttpNotFound();
            }

            var invoice = new Invoices
            {
                BookingID = booking.BookingID,
                InvoiceDate = DateTime.Now,
                TotalAmount = booking.TotalPrice,
                IsPaid = false,
                PaymentStatus = "Pending",
                PaymentMethod = "Cash"
            };

            context.Invoices.Add(invoice);
            context.SaveChanges();

            return View(invoice);
        }
        public ActionResult Index(string search)
        {
            DB_ResortfEntities context = new DB_ResortfEntities();

            List<Invoices> invoices;

            if (string.IsNullOrEmpty(search))
            {
                invoices = context.Invoices.ToList();
            }
            else
            {
                invoices = context.Invoices
                    .Where(i => i.InvoiceID.ToString().Contains(search) || i.BookingID.ToString().Contains(search))
                    .ToList();
            }

            ViewBag.Search = search;

            return View(invoices);
        }

        //Xem chi tiết hóa đơn
        //public ActionResult Details(int id)
        //{
        //    DB_ResortfEntities context = new DB_ResortfEntities();

        //    var invoice =  context.Invoice.FirstOrDefault(i => i.InvoiceID == id);

        //    if (invoice == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(invoice);
        //}
        public ActionResult Details(int id)
        {
            DB_ResortfEntities context = new DB_ResortfEntities();

            // Lấy thông tin hóa đơn
            var invoice = context.Invoices.FirstOrDefault(i => i.InvoiceID == id);
            if (invoice == null)
            {
                return HttpNotFound();
            }

            // Lấy thông tin đặt phòng liên quan đến hóa đơn
            var booking = context.Bookings.FirstOrDefault(b => b.BookingID == invoice.BookingID);
            if (booking == null)
            {
                return HttpNotFound();
            }

            // Lấy thông tin phòng
            var room = context.Rooms.FirstOrDefault(r => r.RoomID == booking.RoomID);

            // Lấy danh sách dịch vụ đã sử dụng
            var usedServices = context.UsedServices
            .Where(us => us.BookingID == booking.BookingID)
            .Join(
                context.Services,
                us => us.ServiceID,
                s => s.ServiceID,
                (us, s) => new UsedServiceViewModel // Chuyển từ kiểu ẩn danh sang ViewModel
                {
                    ServiceName = s.ServiceName,
                    Price = s.Price ?? 0,
                    Quantity = us.Quantity ?? 0,
                    TotalPrice = us.TotalPrice ?? 0
                }
            )
            .ToList();

            // Tạo một model view để truyền dữ liệu
            var invoiceDetailViewModel = new InvoiceDetailViewModel
            {
                Invoice = invoice,
                Booking = booking,
                Room = room,
                UsedServices = usedServices
            };

            return View(invoiceDetailViewModel);
        }

    }
}