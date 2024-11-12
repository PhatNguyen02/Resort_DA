using ResortManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResortManagement.Areas.Admin.Controllers
{
    public class PromotionController : Controller
    {
        // GET: Admin/Voucher
        public ActionResult Index()
        {
            DB_ResortfEntities context = new DB_ResortfEntities();
            List<Promotion> promotions = context.Promotions.ToList();
            return View(promotions);
        }

        [HttpGet]
        public ActionResult AddPromotion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPromotion(Promotion promotion)
        {
            DB_ResortfEntities _context = new DB_ResortfEntities();
            if (ModelState.IsValid)
            {
                _context.Promotions.Add(promotion);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(promotion);
        }

        public ActionResult EditPromotion(int id)
        {
            DB_ResortfEntities _context = new DB_ResortfEntities();
            var pro = _context.Promotions.Find(id);

            if(pro == null)
            {
                return HttpNotFound();
            }    

            return View(pro);
        }

        [HttpPost]
        public ActionResult EditPromotion(Promotion promotion)
        {
            if (ModelState.IsValid)
            {
                using (DB_ResortfEntities _context = new DB_ResortfEntities())
                {
                    var pro = _context.Promotions.Find(promotion.PromotionID);
                    if (pro == null)
                    {
                        return HttpNotFound();
                    }

                    pro.PromotionCode = promotion.PromotionCode;
                    pro.Description = promotion.Description;
                    pro.Discount = promotion.Discount;
                    pro.IsFlashDeal = promotion.IsFlashDeal;
                    pro.StartDate = promotion.StartDate;
                    pro.EndDate = promotion.EndDate;

                    _context.Entry(pro).State = System.Data.Entity.EntityState.Modified;
                    _context.SaveChanges();

                    return RedirectToAction("Index", "Promotion");
                }
            }
            return View(promotion);
        }

        public ActionResult DeletePromotion(int id)
        {
            DB_ResortfEntities _context = new DB_ResortfEntities();
            var pro = _context.Promotions.Find(id);

            if(pro == null)
            {
                return HttpNotFound();
            }    

            return View(pro);
        }

        [HttpPost, ActionName("DeletePromotion")]
        public ActionResult ConfirmDeletePromotion(int id)
        {
            DB_ResortfEntities _context = new DB_ResortfEntities();
            var pro = _context.Promotions.Find(id);


            if (pro == null)
            {
                return HttpNotFound();
            }


            _context.Promotions.Remove(pro);
            _context.SaveChanges();


            return RedirectToAction("Index");
        }
    }
}