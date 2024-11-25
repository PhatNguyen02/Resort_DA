//using PayPal.Api;
//using ResortManagement.Models;
//using ResortManagement.ViewModel;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using Newtonsoft.Json;
//namespace ResortManagement.Controllers
//{
//    public class InvoiceController : Controller
//    {

//        public InvoiceController() { }
//        // GET: Invoice
//        public ActionResult Payment()
//        {
//            return View();
//        }

//        public ActionResult PaymentSuccess()
//        {
//            return View();
//        }
//        public ActionResult PaymentFail()
//        {
//            return View();
//        }
//        //public ActionResult PaymentWithPaypal(string Cancel = null, List<Service> services=null, int bookingId = 0)
//        //{
//        //    //getting the apiContext  
//        //    APIContext apiContext = PaypalConfiguration.GetAPIContext();
//        //    try
//        //    {
//        //        //A resource representing a Payer that funds a payment Payment Method as paypal  
//        //        //Payer Id will be returned when payment proceeds or click to pay  
//        //        string payerId = Request.Params["PayerID"];
//        //        if (string.IsNullOrEmpty(payerId))
//        //        {
//        //            //this section will be executed first because PayerID doesn't exist  
//        //            //it is returned by the create function call of the payment class  
//        //            // Creating a payment  
//        //            // baseURL is the url on which paypal sendsback the data.  
//        //            string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Invoice/PaymentWithPayPal?";
//        //            //here we are generating guid for storing the paymentID received in session  
//        //            //which will be used in the payment execution  
//        //            var guid = Convert.ToString((new Random()).Next(100000));
//        //            //CreatePayment function gives us the payment approval url  
//        //            //on which payer is redirected for paypal account payment  
//        //            var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, services ?? new List<Service>(), bookingId);

//        //            //get links returned from paypal in response to Create function call  
//        //            var links = createdPayment.links.GetEnumerator();
//        //            string paypalRedirectUrl = null;
//        //            while (links.MoveNext())
//        //            {
//        //                Links lnk = links.Current;
//        //                if (lnk.rel.ToLower().Trim().Equals("approval_url"))
//        //                {
//        //                    //saving the payapalredirect URL to which user will be redirected for payment  
//        //                    paypalRedirectUrl = lnk.href;
//        //                }
//        //            }
//        //            // saving the paymentID in the key guid  
//        //            Session.Add(guid, createdPayment.id);
//        //            return Redirect(paypalRedirectUrl);
//        //        }
//        //        else
//        //        {
//        //            // This function exectues after receving all parameters for the payment  
//        //            var guid = Request.Params["guid"];
//        //            var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
//        //            //If executed payment failed then we will show payment failure message to user  
//        //            if (executedPayment.state.ToLower() != "approved")
//        //            {
//        //                return View("PaymentFail");
//        //            }
//        //        }
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        return View("PaymentFail");
//        //    }
//        //    //on successful payment, show success page to user.  
//        //    return View("PaymentSuccess");
//        //}
//        //[HttpPost]
//        //public ActionResult PaymentWithPaypal(string Cancel = null, int bookingId = 0)
//        //{
//        //    //if (services == null)
//        //    //{
//        //    //    return Json(new { message = "No services provided" }, JsonRequestBehavior.AllowGet);
//        //    //}

//        //    // Getting the apiContext
//        //    APIContext apiContext = PaypalConfiguration.GetAPIContext();
//        //    try
//        //    {
//        //        // Payer Id will be returned when payment proceeds or click to pay
//        //        string payerId = Request.Params["PayerID"];
//        //        if (string.IsNullOrEmpty(payerId))
//        //        {
//        //            // This section will be executed first because PayerID doesn't exist  
//        //            // Creating a payment  
//        //            string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Invoice/PaymentWithPayPal?";
//        //            // Generate guid for storing the paymentID received in session  
//        //            var guid = Convert.ToString((new Random()).Next(100000));
//        //            // Call CreatePayment with the required parameters
//        //            var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, bookingId);

//        //            return Json(new { message = "Thanh toán thành công!" });
//        //        }
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        return Json(new { message = "Lỗi xảy ra. Vui lòng thử lại!" });
//        //    }

//        //    return Json(new { message = "Payer ID không hợp lệ." });
//        //}

//        //private PayPal.Api.Payment payment;
//        //private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
//        //{
//        //    var paymentExecution = new PaymentExecution()
//        //    {
//        //        payer_id = payerId
//        //    };
//        //    this.payment = new Payment()
//        //    {
//        //        id = paymentId
//        //    };
//        //    return this.payment.Execute(apiContext, paymentExecution);
//        //}

//        //[HttpPost]
//        //public ActionResult PaymentWithPaypal(string Cancel = null)
//        //{
//        //    APIContext apiContext = PaypalConfiguration.GetAPIContext();
//        //    try
//        //    {
//        //        // Đọc dữ liệu từ body request
//        //        var jsonString = new System.IO.StreamReader(Request.InputStream).ReadToEnd();
//        //        var jsonData = JsonConvert.DeserializeObject<PaymentRequest>(jsonString);

//        //        if (jsonData == null || jsonData.Services == null || !jsonData.Services.Any())
//        //        {
//        //            return Json(new { message = "Không có dịch vụ nào được chọn." });
//        //        }

//        //        // Tạo thanh toán qua PayPal
//        //        string payerId = Request.Params["PayerID"];
//        //        if (string.IsNullOrEmpty(payerId))
//        //        {
//        //            string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Invoice/PaymentWithPayPal?";
//        //            var guid = Convert.ToString((new Random()).Next(100000));
//        //            var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, jsonData.Services);

//        //            return Json(new { message = "Thanh toán thành công!" });
//        //        }
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        return Json(new { message = "Lỗi xảy ra: " + ex.Message });
//        //    }

//        //    return Json(new { message = "Payer ID không hợp lệ." });
//        //}

//        //private Payment CreatePayment(APIContext apiContext, string redirectUrl, int bookingId, List<Service> services)
//        //{
//        //    var itemList = new ItemList()
//        //    {
//        //        items = new List<Item>()
//        //    };

//        //    using (var db = new DB_ResortfEntities())
//        //    {
//        //        var booking = db.Bookings.Include("Room").FirstOrDefault(b => b.BookingID == bookingId);

//        //        if (booking == null || booking.Room == null)
//        //        {
//        //            throw new Exception("Không tìm thấy thông tin đặt phòng hoặc phòng.");
//        //        }

//        //        // Thêm giá phòng vào danh sách Item
//        //        var room = booking.Room;
//        //        itemList.items.Add(new Item()
//        //        {
//        //            name = room.RoomType,
//        //            currency = "USD",
//        //            price = room.Price.HasValue ? room.Price.Value.ToString("F2") : "0.00",
//        //            quantity = "1",
//        //            sku = room.RoomID.ToString()
//        //        });

//        //        // Thêm danh sách dịch vụ vào ItemList
//        //        if (services != null)
//        //        {
//        //            itemList.items.AddRange(services.Select(service => new Item()
//        //            {
//        //                name = service.ServiceName,
//        //                currency = "USD",
//        //                price = service.Price.ToString("F2"),
//        //                quantity = service.Quantity.ToString(),
//        //                sku = service.ServiceID.ToString()
//        //            }));
//        //        }

//        //        // Cấu hình người thanh toán
//        //        var payer = new Payer()
//        //        {
//        //            payment_method = "paypal"
//        //        };

//        //        // Cấu hình RedirectUrls
//        //        var redirUrls = new RedirectUrls()
//        //        {
//        //            cancel_url = redirectUrl + "&Cancel=true",
//        //            return_url = redirectUrl
//        //        };

//        //        // Danh sách giao dịch
//        //        var paypalOrderId = DateTime.Now.Ticks;
//        //        var transactionList = new List<Transaction>
//        //{
//        //    new Transaction()
//        //    {
//        //        description = $"Invoice #{paypalOrderId}",
//        //        invoice_number = paypalOrderId.ToString(),
//        //        amount = new Amount()
//        //        {
//        //            currency = "USD",
//        //            total = itemList.items.Sum(i => decimal.Parse(i.price) * int.Parse(i.quantity)).ToString("F2")
//        //        },
//        //        item_list = itemList
//        //    }
//        //};

//        //        // Tạo Payment
//        //        this.payment = new Payment()
//        //        {
//        //            intent = "sale",
//        //            payer = payer,
//        //            transactions = transactionList,
//        //            redirect_urls = redirUrls
//        //        };

//        //        // Gửi yêu cầu tới API PayPal
//        //        return this.payment.Create(apiContext);
//        //    }
//        //}

//        private Payment CreatePayment(APIContext apiContext, string redirectUrl, int bookingId)
//        {
//            var itemList = new ItemList()
//            {
//                items = new List<Item>()
//            };

//            using (var db = new DB_ResortfEntities())
//            {
//                // Lấy thông tin booking và phòng
//                var booking = db.Bookings
//                                .Include("Room")
//                                .FirstOrDefault(b => b.BookingID == bookingId);

//                if (booking == null || booking.Room == null)
//                {
//                    throw new Exception("Không tìm thấy thông tin đặt phòng hoặc phòng.");
//                }

//                // Thêm giá phòng vào danh sách Item
//                var room = booking.Room;
//                itemList.items.Add(new Item()
//                {
//                    name = room.RoomType,
//                    currency = "USD",
//                    price = room.Price.HasValue ? room.Price.Value.ToString("F2") : "0.00",
//                    quantity = "1",
//                    sku = room.RoomID.ToString()
//                });

//                // Thêm các dịch vụ vào danh sách Item
//                //if (services != null)
//                //{
//                //    itemList.items.AddRange(services.Select(service => new Item()
//                //    {
//                //        name = service.ServiceName,
//                //        currency = "USD",
//                //        price = service.Price.HasValue ? service.Price.Value.ToString("F2") : "0.00",
//                //        quantity = "1",
//                //        sku = service.ServiceID.ToString()
//                //    }));
//                //}

//                //// Tính toán tổng giá trị
//                //var roomPrice = room.Price;
//                ////var servicesPrice = services?.Sum(s => s.Price) ?? 0;
//                ////var subtotal = roomPrice + servicesPrice;
//                //var tax = subtotal * 0.1m; // Thuế giả định là 10%
//                //var shipping = 5m; // Phí vận chuyển cố định
//                //var total = subtotal + tax + shipping;

//                // Cấu hình người thanh toán
//                var payer = new Payer()
//                {
//                    payment_method = "paypal"
//                };

//                // Cấu hình RedirectUrls
//                var redirUrls = new RedirectUrls()
//                {
//                    cancel_url = redirectUrl + "&Cancel=true",
//                    return_url = redirectUrl
//                };

//                // Chi tiết thanh toán
//                //var details = new Details()
//                //{
//                //    tax = tax.HasValue ? tax.Value.ToString("F2") : "0.00",
//                //    shipping = shipping.ToString("F2"),
//                //    subtotal = subtotal.HasValue ? subtotal.Value.ToString("F2") : "0.00"
//                //};

//                //// Tổng hợp số tiền
//                //var amount = new Amount()
//                //{
//                //    currency = "USD",
//                //    total = total.HasValue ? total.Value.ToString("F2") : "0.00",
//                //    details = details
//                //};

//                // Danh sách giao dịch
//                var paypalOrderId = DateTime.Now.Ticks;
//                var transactionList = new List<Transaction>
//        {
//            new Transaction()
//            {
//                description = $"Invoice #{paypalOrderId}",
//                invoice_number = paypalOrderId.ToString(),
//                //amount = amount,
//                item_list = itemList
//            }
//        };

//                // Cấu hình Payment
//                this.payment = new Payment()
//                {
//                    intent = "sale",
//                    payer = payer,
//                    transactions = transactionList,
//                    redirect_urls = redirUrls
//                };

//                // Tạo Payment qua APIContext
//                return this.payment.Create(apiContext);
//            }
//        }

//        private Payment CreatePayment(APIContext apiContext, string redirectUrl, List<Service> services, int bookingId)
//        {
//            // Tạo danh sách Item
//            var itemList = new ItemList()
//            {
//                items = new List<Item>()
//            };

//            // Thêm giá phòng từ bookingId vào danh sách Item
//            var db = new DB_ResortfEntities();
//            //var room = db.Bookings. // Lấy thông tin phòng
//            var booking = db.Bookings
//                           .Include("User")
//                           .Include("Room")
//                           .FirstOrDefault(b => b.BookingID == bookingId);

//            var room = db.Rooms
//                               .Include("User")
//                               .Include("Room")
//                               .FirstOrDefault(b => b.RoomID == booking.RoomID);
//            if (room != null)
//            {
//                itemList.items.Add(new Item()
//                {
//                    name = room.RoomType,
//                    currency = "USD",
//                    price = room.Price.ToString("F2"),
//                    quantity = "1",
//                    sku = room.RoomID.ToString()
//                });
//            }

//            // Thêm các dịch vụ vào danh sách Item
//            itemList.items.AddRange(services.Select(service => new Item()
//            {
//                name = service.ServiceName,
//                currency = "USD",
//                price = service.Price.ToString("F2"),
//                quantity = "1",
//                sku = service.ServiceID.ToString()
//            }));

//            // Tính toán tổng giá trị
//            var roomPrice = room?.Price ?? 0; // Giá phòng (nếu có)
//            var subtotal = services.Sum(s => s.Price) + roomPrice;
//            var tax = subtotal * 0.1m; // Giả sử thuế là 10%
//            var shipping = 5m; // Phí vận chuyển cố định
//            var total = subtotal + tax + shipping;

//            // Cấu hình người thanh toán
//            var payer = new Payer()
//            {
//                payment_method = "paypal"
//            };

//            // Cấu hình RedirectUrls
//            var redirUrls = new RedirectUrls()
//            {
//                cancel_url = redirectUrl + "&Cancel=true",
//                return_url = redirectUrl
//            };

//            // Thêm chi tiết thanh toán
//            var details = new Details()
//            {
//                tax = tax.ToString("F2"),
//                shipping = shipping.ToString("F2"),
//                subtotal = subtotal.ToString("F2")
//            };

//            // Tổng hợp số tiền
//            var amount = new Amount()
//            {
//                currency = "USD",
//                total = total.ToString("F2"),
//                details = details
//            };

//            // Danh sách giao dịch
//            var transactionList = new List<Transaction>();
//            var paypalOrderId = DateTime.Now.Ticks; // Tạo số hóa đơn duy nhất
//            transactionList.Add(new Transaction()
//            {
//                description = $"Invoice #{paypalOrderId}",
//                invoice_number = paypalOrderId.ToString(),
//                amount = amount,
//                item_list = itemList
//            });

//            // Cấu hình Payment
//            this.payment = new Payment()
//            {
//                intent = "sale",
//                payer = payer,
//                transactions = transactionList,
//                redirect_urls = redirUrls
//            };

//            // Tạo Payment qua APIContext
//            return this.payment.Create(apiContext);
//        }

//        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
//        {
//            //create itemlist and add item objects to it  

//            var itemList = new ItemList()
//            {
//                items = new List<Item>()
//            };
//            //Adding Item Details like name, currency, price etc  
//            itemList.items.Add(new Item()
//            {
//                name = "Item Name comes here",
//                currency = "USD",
//                price = "1",
//                quantity = "1",
//                sku = "sku"
//            });
//            var payer = new Payer()
//            {
//                payment_method = "paypal"
//            };
//            // Configure Redirect Urls here with RedirectUrls object  
//            var redirUrls = new RedirectUrls()
//            {
//                cancel_url = redirectUrl + "&Cancel=true",
//                return_url = redirectUrl
//            };
//            // Adding Tax, shipping and Subtotal details  
//            var details = new Details()
//            {
//                tax = "1",
//                shipping = "1",
//                subtotal = "1"
//            };

//            // Tổng phải khớp: subtotal + tax + shipping = total
//            var amount = new Amount()
//            {
//                currency = "USD",
//                total = "3",  // 1 + 1 + 1 = 3
//                details = details
//            };

//            var transactionList = new List<Transaction>();
//            // Adding description about the transaction  
//            var paypalOrderId = DateTime.Now.Ticks;
//            transactionList.Add(new Transaction()
//            {
//                description = $"Invoice #{paypalOrderId}",
//                invoice_number = paypalOrderId.ToString(), //Generate an Invoice No    
//                amount = amount,
//                item_list = itemList
//            });
//            this.payment = new Payment()
//            {
//                intent = "sale",
//                payer = payer,
//                transactions = transactionList,
//                redirect_urls = redirUrls
//            };
//            // Create a payment using a APIContext  
//            return this.payment.Create(apiContext);
//        }

//    }
//}
using PayPal.Api;
using ResortManagement.Models;
using ResortManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ResortManagement.Services;
using ResortInvoice = ResortManagement.Models.Invoice;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using ZaloPay.Helper.Crypto;
using ZaloPay.Helper;
namespace ResortManagement.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly BookingService _bookingService = new BookingService();
        private readonly ZaloPayConfiguration _zaloPayConfig;

        public InvoiceController()
        {
            // Khởi tạo đối tượng ZaloPayConfiguration từ class này
            _zaloPayConfig = new ZaloPayConfiguration();
        }

        public ActionResult PaymentSuccess(string paymentId, string token, string PayerID)
        {
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                // Lấy APIContext
                APIContext apiContext = PaypalConfiguration.GetAPIContext();

                // Xử lý thanh toán hoàn tất
                var paymentExecution = new PaymentExecution() { payer_id = PayerID };
                var payment = new Payment() { id = paymentId };

                // Hoàn tất thanh toán
                var executedPayment = payment.Execute(apiContext, paymentExecution);

                // Lấy thông tin giao dịch từ executedPayment
                var transaction = executedPayment.transactions.FirstOrDefault();
                if (transaction == null)
                {
                    TempData["Message"] = "Không tìm thấy giao dịch.";
                    return RedirectToAction("Error", "HandleError");
                }

                // Lấy invoice_number (BookingId) từ giao dịch, chuyển về kiểu long vì BookingID là BIGINT
                var bookingId = long.Parse(transaction.invoice_number);

                using (var db = new DB_ResortfEntities())
                {
                    // Tìm booking trong cơ sở dữ liệu, với BookingID là long (BIGINT)
                    var booking = db.Bookings.FirstOrDefault(b => b.BookingID == bookingId);
                    if (booking == null)
                    {
                        TempData["Message"] = "Không tìm thấy Booking.";
                        return RedirectToAction("Error", "HandleError");
                    }

                    // Cập nhật trạng thái thanh toán của booking
                    booking.Status = "Paid";
                    db.SaveChanges();

                    // Tạo hóa đơn (Invoice)
                    var invoice = new ResortInvoice
                    {
                        BookingID = booking.BookingID,
                        InvoiceDate = DateTime.Now,

                        // Kiểm tra TotalPrice là decimal (hoặc float nếu cần)
                        TotalAmount = booking.TotalPrice.HasValue ? (decimal)booking.TotalPrice : 0m,  // Hoặc sử dụng float nếu cần
                        IsPaid = true,
                        PaymentStatus = "Paid",
                        PaymentMethod = "PayPal"
                    };

                    // Lưu Invoice vào cơ sở dữ liệu
                    db.Invoices.Add(invoice);
                    db.SaveChanges();

                    // Chuyển hướng đến trang xác nhận hóa đơn
                    return RedirectToAction("Confirm", "Invoice", new { invoiceId = invoice.InvoiceID });
                }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
            }
            catch (PayPal.PayPalException ex)
            {
                TempData["Message"] = "Lỗi PayPal: " + (ex.InnerException?.Message ?? ex.Message);
                return RedirectToAction("Error", "HandleError");
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Lỗi: " + ex.Message;
                return RedirectToAction("Error", "HandleError");
            }
        }


        public ActionResult PaymentError()
        {
            // Trang hiển thị lỗi
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View();
        }

    // Thêm PaymentCancel action để xử lý khi người dùng hủy thanh toán
    public ActionResult PaymentCancel()
    {
        TempData["ErrorMessage"] = "Thanh toán đã bị hủy!";
        return RedirectToAction("PaymentError");
    }
        [HttpPost]
        public ActionResult PaymentWithPaypal(PaymentRequest request)
        {
            try
            {
                // Lấy APIContext
                APIContext apiContext = PaypalConfiguration.GetAPIContext();

                // Tỷ giá VND sang USD
                decimal exchangeRate = 0.000043m;

                // Lấy thông tin booking từ cơ sở dữ liệu
                var booking = _bookingService.GetBookingById(request.BookingId); // Hàm này bạn cần triển khai
                if (booking == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Booking không tồn tại."
                    });
                }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            //create itemlist and add item objects to it  

                if (booking.RoomID == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "RoomID không tồn tại."
                    });
                }

                var room = _bookingService.GetRoomByIdBooking(booking.RoomID.Value);

                // Lấy thông tin phòng
               
                decimal totalVND = booking.TotalPrice ?? 0m; // Giá trị mặc định là 0 nếu null

                var itemList = new ItemList { items = new List<Item>() };

                // Thêm thông tin phòng vào danh sách
                itemList.items.Add(new Item()
                {
                    name = $"Room {room.RoomType}",
                    currency = "USD",
                    price = Math.Round((decimal)(booking.TotalPrice ?? 0m) * exchangeRate, 2).ToString("0.00"),
                    quantity = "1",
                    sku = booking.Room.ToString()
                });


                // Nếu booking có dịch vụ, thêm vào danh sách
                if (request.Services != null && request.Services.Any())
                {
                    foreach (var service in request.Services)
                    {
                        var servicePriceUSD = Math.Round(service.Price * exchangeRate, 2);
                        totalVND += service.Price * service.Quantity;

                        itemList.items.Add(new Item
                        {
                            name = service.ServiceName,
                            currency = "USD",
                            price = servicePriceUSD.ToString("0.00"),
                            quantity = service.Quantity.ToString(),
                            sku = service.ServiceID.ToString()
                        });
                    }
                }

                // Tổng tiền sau khi chuyển đổi sang USD
                decimal totalUSD = Math.Round(totalVND * exchangeRate, 2);

                // Chi tiết thanh toán
                var details = new Details
                {
                    subtotal = totalUSD.ToString("0.00"),
                    tax = "0.00",
                    shipping = "0.00"
                };

                // Tạo thông tin thanh toán
                var amount = new Amount
                {
                    currency = "USD",
                    total = totalUSD.ToString("0.00"),
                    details = details
                };

                // Tạo giao dịch
                var transactionList = new List<Transaction>
        {
            new Transaction
            {
                description = $"Payment for Booking #{request.BookingId}",
                invoice_number = request.BookingId.ToString(),
                amount = amount,
                item_list = itemList
            }
        };

                // Tạo thanh toán PayPal
                var payment = new Payment
                {
                    intent = "sale",
                    payer = new Payer { payment_method = "paypal" },
                    transactions = transactionList,
                    redirect_urls = new RedirectUrls
                    {
                        cancel_url = $"{Request.Url.Scheme}://{Request.Url.Authority}/Invoice/PaymentCancel",
                        return_url = $"{Request.Url.Scheme}://{Request.Url.Authority}/Invoice/PaymentSuccess"
                    }
                };

                // Tạo thanh toán
                var createdPayment = payment.Create(apiContext);
                var redirectUrl = createdPayment.links
                    .FirstOrDefault(x => x.rel.ToLower() == "approval_url")?.href;

                return Json(new
                {
                    success = true,
                    redirectUrl = redirectUrl
                });
            }
            catch (PayPal.PayPalException ex)
            {
                // Xử lý lỗi PayPal
                return Json(new
                {
                    success = false,
                    message = "Lỗi PayPal: " + (ex.InnerException?.Message ?? ex.Message)
                });
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khác
                return Json(new
                {
                    success = false,
                    message = "Lỗi: " + ex.Message
                });
            }
        }


        ///zalo Pay
        public async Task<Dictionary<string, object>> CreatePaymentOrder(PaymentRequest paymentRequest)
        {
            Random rnd = new Random();
            var embed_data = new { };
            var items = paymentRequest.Services.Select(s => new { s.ServiceName, s.Subtotal }).ToArray();

            var app_trans_id = rnd.Next(1000000);
            var param = new Dictionary<string, string>();

            // Dùng các giá trị từ ZaloPayConfiguration và PaymentRequest
            param.Add("app_id", _zaloPayConfig.AppId);  // app_id từ ZaloPayConfiguration
            param.Add("app_user", paymentRequest.BookingId.ToString());  // Chuyển BookingId thành app_user
            param.Add("app_time", Utils.GetTimeStamp().ToString());
            param.Add("amount", paymentRequest.TotalAmount.ToString());  // Sử dụng TotalAmount từ PaymentRequest
            param.Add("app_trans_id", DateTime.Now.ToString("yyMMdd") + "_" + app_trans_id);
            param.Add("embed_data", JsonConvert.SerializeObject(embed_data));
            param.Add("item", JsonConvert.SerializeObject(items));
            param.Add("description", "Booking #" + paymentRequest.BookingId);  // Mô tả có thể là BookingId hoặc thông tin từ Services
            param.Add("bank_code", "");

            // Tạo chuỗi dữ liệu để tính toán MAC
            var data = _zaloPayConfig.AppId + "|" + param["app_trans_id"] + "|" + param["app_user"] + "|" + param["amount"] + "|"
                         + param["app_time"] + "|" + param["embed_data"] + "|" + param["item"];
            param.Add("mac", HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, _zaloPayConfig.AppSecret, data));  // Dùng appSecret từ ZaloPayConfiguration

            // Gửi yêu cầu đến API ZaloPay để tạo đơn hàng
            var result = await HttpHelper.PostFormAsync(_zaloPayConfig.CreateOrderUrl, param);  // Dùng create_order_url từ ZaloPayConfiguration

            // Kiểm tra kết quả trả về
            if (result.ContainsKey("return_code") && result["return_code"].ToString() == "1")
            {
                // Nếu có order_url, chuyển hướng đến URL thanh toán
                if (result.ContainsKey("order_url"))
                {
                    string orderUrl = result["order_url"].ToString();
                    // Mở URL thanh toán trong trình duyệt mặc định hoặc trả về cho người dùng
                    // System.Diagnostics.Process.Start(orderUrl); // Bạn có thể mở URL này hoặc trả về kết quả cho người dùng
                }
            }
            else
            {
                // Xử lý lỗi hoặc không có order_url
                Console.WriteLine("Error: " + result["return_message"]);
            }

            return result; // Trả về kết quả
        }


        public async Task<Dictionary<string, object>> QueryPaymentStatus(string appTransId)
        {
            string queryOrderUrl = "https://sb-openapi.zalopay.vn/v2/query";

            var param = new Dictionary<string, string>
            {
                { "app_id", _zaloPayConfig.AppId },  // Dùng app_id từ ZaloPayConfiguration
                { "app_trans_id", appTransId }
            };

            // Tạo dữ liệu để tính toán MAC
            var data = $"{_zaloPayConfig.AppId}|{appTransId}|{_zaloPayConfig.AppSecret}";
            param.Add("mac", HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, _zaloPayConfig.AppSecret, data));

            // Gửi yêu cầu để truy vấn trạng thái thanh toán
            var result = await HttpHelper.PostFormAsync(queryOrderUrl, param);
            return result; // Trả về kết quả
        }

        [HttpPost]
        public ActionResult PaymentNotify()
        {
            // Xử lý callback từ ZaloPay
            var response = Request.Form;
            // Lưu log payment
            // Cập nhật trạng thái đơn hàng
            return new HttpStatusCodeResult(200);
        }

        public ActionResult PaymentSuccess()
        {
            // Xử lý khi user được redirect về
            return View();
        }

    }
}