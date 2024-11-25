using ResortManagement.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service = ResortManagement.Models.Services;

namespace ResortManagement.Areas.Admin.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Admin/Service
        public ActionResult Index(string search)
        {
            DB_ResortfEntities context = new DB_ResortfEntities();
            List<Service> services;


            if (string.IsNullOrEmpty(search))
            {
                services = context.Services.ToList();
            }
            else
            {
                services = context.Services
                .Where(p => p.ServiceName.Contains(search))
                    .ToList();
            }

            ViewBag.Search = search;
            return View(services);
        }

        [HttpGet]
        public ActionResult AddService()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddService(Service service, HttpPostedFileBase ImageServices)
        {
            DB_ResortfEntities _context = new DB_ResortfEntities();

            if (ImageServices != null && ImageServices.ContentLength > 0)
            {
                string fileName = Path.GetFileName(ImageServices.FileName);
                string path = Path.Combine(Server.MapPath("~/assets_detail/img/service/"), fileName);

                ImageServices.SaveAs(path);

                service.ImageServices = fileName;
            }

            if (ModelState.IsValid)
            {
                _context.Services.Add(service);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(service);
        }

        public ActionResult EditService(int id)
        {
            DB_ResortfEntities _context = new DB_ResortfEntities();

            var service = _context.Services.Find(id);

            if (service == null)
            {
                return HttpNotFound();
            }

            return View(service);
        }

        [HttpPost]
        public ActionResult EditService(Service service, HttpPostedFileBase ImageServices)
        {
            if (ModelState.IsValid)
            {
                using (DB_ResortfEntities _context = new DB_ResortfEntities())
                {
                    var servicedb = _context.Services.Find(service.ServiceID);

                    if (servicedb == null)
                    {
                        return HttpNotFound();
                    }

                    servicedb.ServiceName = service.ServiceName;
                    servicedb.ServiceCategory = service.ServiceCategory;
                    servicedb.Price = service.Price;
                    servicedb.Description = service.Description;
                    servicedb.StatusServices = service.StatusServices;

                    if (ImageServices != null && ImageServices.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(ImageServices.FileName);
                        string path = Path.Combine(Server.MapPath("~/assets_detail/img/service/"), fileName);

                        ImageServices.SaveAs(path);

                        if (!string.IsNullOrEmpty(servicedb.ImageServices))
                        {
                            string oldPath = Path.Combine(Server.MapPath("~/assets_detail/img/service/"), servicedb.ImageServices);
                            if (System.IO.File.Exists(oldPath))
                            {
                                System.IO.File.Delete(oldPath);
                            }
                        }

                        servicedb.ImageServices = fileName;
                    }

                    _context.Entry(servicedb).State = System.Data.Entity.EntityState.Modified;
                    _context.SaveChanges();

                    return RedirectToAction("Index", "Service");
                }
            }
            return View(service);
        }

        public ActionResult DeleteService(int id)
        {
            DB_ResortfEntities _context = new DB_ResortfEntities();

            var service = _context.Services.Find(id);

            if (service == null)
            {
                return HttpNotFound();
            }

            return View(service);
        }

        [HttpPost, ActionName("DeleteService")]
        public ActionResult ComfirmDeleteService(int id)
        {
            DB_ResortfEntities _context = new DB_ResortfEntities();
            var service = _context.Services.Find(id);


            if (service == null)
            {
                return HttpNotFound();
            }


            _context.Services.Remove(service);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}