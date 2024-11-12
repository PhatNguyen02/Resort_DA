using ResortManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResortManagement.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Cart()
        {
            return View();
        }
        //public List<Cart> GetCartItems()
        //{
        //    if (HttpContext.Current.Session["Cart"] == null)
        //    {
        //        HttpContext.Current.Session["Cart"] = new List<Cart>();
        //    }
        //    return (List<Cart>)HttpContext.Current.Session["Cart"];
        //}
        //public ActionResult AddToCart(int RoomID, string strUrl)
        //{
        //    // Validate if user session exists
        //    if (Session["User"] is not User user)
        //    {
        //        Response.StatusCode = 401; // Unauthorized
        //        return null;
        //    }

        //    int userID = user.UserID;

        //    using (DB_ResortfEntities db = new DB_ResortfEntities())
        //    {
        //        // Find the room by ID
        //        var room = db.Rooms.SingleOrDefault(x => x.RoomID == RoomID);
        //        if (room == null)
        //        {
        //            Response.StatusCode = 404; // Not Found
        //            return null;
        //        }

        //        List<CartItem> lstCart = ListItemCart();

        //        // Check if the room is already in the cart
        //        CartItem cartItem = lstCart.SingleOrDefault(x => x.RoomID == RoomID);
        //        if (cartItem != null)
        //        {
        //            // Check if adding one more exceeds stock
        //            if (cartItem.Quantity + 1 > room.Quantity)
        //            {
        //                Response.StatusCode = 400; // Bad Request
        //                return null;
        //            }

        //            cartItem.Quantity++;
        //            cartItem.ItemPriceTotal = cartItem.Quantity * cartItem.Price;
        //        }
        //        else
        //        {
        //            // Add new room to the cart
        //            CartItem newItem = new CartItem(RoomID, userID, room.Price);
        //            if (room.Quantity < newItem.Quantity)
        //            {
        //                Response.StatusCode = 400; // Bad Request
        //                return null;
        //            }

        //            lstCart.Add(newItem);
        //        }
        //    }

        //    return Redirect(strUrl); // Redirect back to the provided URL
        //}

        // Helper method to get the cart items
        private List<CartItem> ListItemCart()
        {
            var cart = Session["Cart"] as List<CartItem>;
            if (cart == null)
            {
                cart = new List<CartItem>();
                Session["Cart"] = cart;
            }
            return cart;
        }

    }
}