using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClasesAlicanTeam.EN;

namespace LibrosSalesianos.Models
{
    public class ShoppingCart
    {
        #region Properties

        public List<CartItem> Items { get; private set; }

        #endregion

        #region Singleton Implementation

        // Readonly properties can only be set in initialization or in a constructor
        public static readonly ShoppingCart Instance;
        // The static constructor is called as soon as the class is loaded into memory
        static ShoppingCart()
        {
            // If the cart is not in the session, create one and put it there
            // Otherwise, get it from the session
            if (HttpContext.Current.Session["ASPNETShoppingCart"] == null)
            {
                Instance = new ShoppingCart();
                Instance.Items = new List<CartItem>();
                HttpContext.Current.Session["ASPNETShoppingCart"] = Instance;
            }
            else
            {
                Instance = (ShoppingCart)HttpContext.Current.Session["ASPNETShoppingCart"];
            }
        }

        // A protected constructor ensures that an object can't be created from outside
        protected ShoppingCart() { }

        #endregion

        #region Item Modification Methods

        public CartItem GetByISBN(string isbn)
        {
            foreach (CartItem item in Items)
            {
                if (item.BookIsbn == isbn)
                {
                    return item;
                }
            }
            return null;
        }

        /**
	     * AddItem() - Adds an item to the shopping 
	     */
        public void AddItem(ENNewBook book)
        {
            // Create a new item to add to the cart
            ENNewBook b = new ENNewBook(book.Id);
            CartItem newItem = new CartItem(b);

            // If this item already exists in our list of items, increase the quantity
            // Otherwise, add the new item to the list
                foreach (CartItem item in Items)
                {
                    if (item.Book.Id == book.Id)
                    {
                        item.Quantity++;
                        return;
                    }
                }
 
                newItem.Quantity = 1;
                Items.Add(newItem);
        }

        /**
         * SetItemQuantity() - Changes the quantity of an item in the cart
         */
        public void SetItemQuantity(string isbn, int quantity)
        {
            var item = GetByISBN(isbn);
            if (item != null)
            {
                // If we are setting the quantity to 0, remove the item entirely
                if (quantity == 0)
                {
                    Items.Remove(item);
                    return;
                }

                item.Quantity = quantity;
            }
        }

        /**
         * RemoveItem() - Removes an item from the shopping cart
         */
        public void RemoveItem(ENNewBook book)
        {
            CartItem removedItem = new CartItem(book);
            foreach (var item in Items)
            {
                if (item.Book.Id == book.Id)
                {
                    Items.Remove(item);
                    break;
                }
            }
        }
        #endregion

        #region Reporting Methods
        /**
	     * GetSubTotal() - returns the total price of all of the items
	     *                 before tax, shipping, etc.
	     */
        public float GetSubTotal()
        {
            float subTotal = 0;
            foreach (CartItem item in Items)
                subTotal += item.TotalPrice;

            return subTotal;
        }
        #endregion
    }
}