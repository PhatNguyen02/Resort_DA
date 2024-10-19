using System;

namespace ResortManagement.Models
{
    public class CartItem
    {
        public int RoomID { get; set; }
        public int UserID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal ItemPriceTotal { get; set; }

        // Constructor to initialize properties
        public CartItem(int roomID, int userID, decimal price)
        {
            RoomID = roomID;
            UserID = userID;
            Quantity = 1; // Default quantity is 1
            Price = price;
            ItemPriceTotal = price; // Initial total price
        }
    }
}
