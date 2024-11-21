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

namespace ResortManagement.Controllers
{
    public class InvoiceController : Controller
    {
         public ActionResult PaymentSuccess()
        {
            // Trang hiển thị thông báo thành công
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View();
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
                APIContext apiContext = PaypalConfiguration.GetAPIContext();

                // Chuyển đổi sang USD và làm tròn
                decimal exchangeRate = 0.000043m; // Tỷ giá VND sang USD
                decimal totalUSD = Math.Round(request.TotalAmount * exchangeRate, 2);

                var itemList = new ItemList()
                {
                    items = request.Services.Select(s => new Item()
                    {
                        name = s.ServiceName,
                        currency = "USD",
                        price = Math.Round(s.Price * exchangeRate, 2).ToString("0.00"),
                        quantity = s.Quantity.ToString(),
                        sku = s.ServiceID.ToString()
                    }).ToList()
                };

                var details = new Details()
                {
                    subtotal = totalUSD.ToString("0.00"),
                    tax = "0.00",
                    shipping = "0.00"
                };

                var amount = new Amount()
                {
                    currency = "USD",
                    total = totalUSD.ToString("0.00"),
                    details = details
                };

                var transactionList = new List<Transaction> {
            new Transaction() {
                description = $"Booking #{request.BookingId}",
                invoice_number = DateTime.Now.Ticks.ToString(),
                amount = amount,
                item_list = itemList
            }
        };

                var payment = new Payment()
                {
                    intent = "sale",
                    payer = new Payer() { payment_method = "paypal" },
                    transactions = transactionList,
                    redirect_urls = new RedirectUrls()
                    {
                        cancel_url = Request.Url.Scheme + "://" + Request.Url.Authority + "/Invoice/PaymentCancel",
                        return_url = Request.Url.Scheme + "://" + Request.Url.Authority + "/Invoice/PaymentSuccess"
                    }
                };

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
                // Log chi tiết lỗi PayPal
                return Json(new
                {
                    success = false,
                    message = "Lỗi PayPal: " + ex.InnerException?.Message ?? ex.Message
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Lỗi: " + ex.Message
                });
            }
        }

        // Lớp để ánh xạ dữ liệu từ AJAX
        
       
    }
}