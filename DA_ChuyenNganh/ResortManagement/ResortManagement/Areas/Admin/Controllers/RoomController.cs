using ResortManagement.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResortManagement.Areas.Admin.Controllers
{
    public class RoomController : Controller
    {
        // GET: Admin/Room
        public ActionResult Index()
        {
            DB_ResortfEntities context = new DB_ResortfEntities();
            List<Room> rooms = context.Rooms.ToList();
            return View(rooms);
        }

        public ActionResult AddRoom()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddRoom(Room room, HttpPostedFileBase ImageRooms)
        {
            DB_ResortfEntities _context = new DB_ResortfEntities();

            if (ImageRooms != null && ImageRooms.ContentLength > 0)
            {
                string fileName = Path.GetFileName(ImageRooms.FileName);
                string path = Path.Combine(Server.MapPath("~/assets_detail/img/room/"), fileName);

                ImageRooms.SaveAs(path);

                room.ImageRooms = fileName;
            }

            if (ModelState.IsValid)
            {
                _context.Rooms.Add(room);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(room);
        }

        public ActionResult EditRoom(int id)
        {
            DB_ResortfEntities _context = new DB_ResortfEntities();

            var room = _context.Rooms.Find(id);

            if (room == null)
            {
                return HttpNotFound();
            }

            return View(room);
        }

        [HttpPost]
        public ActionResult EditRoom(Room room, HttpPostedFileBase ImageRooms)
        {
            if (ModelState.IsValid)
            {
                using (DB_ResortfEntities _context = new DB_ResortfEntities())
                {
                    var roomdb = _context.Rooms.Find(room.RoomID);

                    if (roomdb == null)
                    {
                        return HttpNotFound();
                    }

                    roomdb.RoomType = room.RoomType;
                    roomdb.Price = room.Price;
                    roomdb.Description = room.Description;
                    roomdb.Status = room.Status;

                    if (ImageRooms != null && ImageRooms.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(ImageRooms.FileName);
                        string path = Path.Combine(Server.MapPath("~/assets_detail/img/service/"), fileName);

                        ImageRooms.SaveAs(path);

                        if (!string.IsNullOrEmpty(roomdb.ImageRooms))
                        {
                            string oldPath = Path.Combine(Server.MapPath("~/assets_detail/img/service/"), roomdb.ImageRooms);
                            if (System.IO.File.Exists(oldPath))
                            {
                                System.IO.File.Delete(oldPath);
                            }
                        }

                        roomdb.ImageRooms = fileName;
                    }

                    _context.Entry(roomdb).State = System.Data.Entity.EntityState.Modified;
                    _context.SaveChanges();

                    return RedirectToAction("Index", "Room");
                }
            }
            return View(room);
        }

        public ActionResult DeleteRoom(int id)
        {
            DB_ResortfEntities _context = new DB_ResortfEntities();

            var room = _context.Rooms.Find(id);

            if (room == null)
            {
                return HttpNotFound();
            }

            return View(room);
        }

        [HttpPost, ActionName("DeleteRoom")]
        public ActionResult ComfirmDeleteRoom(int id)
        {
            DB_ResortfEntities _context = new DB_ResortfEntities();
            var room = _context.Rooms.Find(id);


            if (room == null)
            {
                return HttpNotFound();
            }


            _context.Rooms.Remove(room);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}