using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClasesAlicanTeam.EN;

namespace LibrosSalesianos.Models
{
    public class CartItem
    {
         #region Properties

        // A place to store the quantity in the cart
        // This property has an implicit getter and setter.
        public int Quantity { get; set; }

        public string BookIsbn
        {
            get { return Book.IdBook; }
        }

        public ENNewBook Book { get; private set; }

        public string Description
        {
            get { return Book.Description; }
        }

        public string Image { get { return Book.Picture; } }

        public float UnitPrice
        {
            get { return Book.Price; }
        }

        public float TotalPrice
        {
            get { return UnitPrice * Quantity; }
        }

        public int Id { get; set; }

        #endregion

        public CartItem(ENNewBook book)
        {
            this.Book = book;
            this.Id = book.Id;
            this.Quantity = 1;
        }

        /**
         * Equals() - Needed to implement the IEquatable interface
         *    Tests whether or not this item is equal to the parameter
         *    This method is called by the Contains() method in the List class
         *    We used this Contains() method in the ShoppingCart AddItem() method
         */
        public bool Equals(CartItem item)
        {
            return item.BookIsbn == this.BookIsbn;
        }
    }
}