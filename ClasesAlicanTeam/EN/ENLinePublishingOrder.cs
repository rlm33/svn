using ClasesAlicanTeam.CAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesAlicanTeam.EN
{
    public class ENLinePublishingOrder
    {
        private int idLines_POrders;
        private ENPublishingOrder publishingOrder;
        private ENNew newBook;
        private int quantity;
        private CADLinePublishingOrder cad;

        public ENLinePublishingOrder()
        {
            cad = new CADLinePublishingOrder();
        }

        public ENLinePublishingOrder(int idLines_POrders, ENPublishingOrder publishingOrder, ENNew newBook, int quantity)
        {
            this.idLines_POrders = idLines_POrders;
            this.publishingOrder = publishingOrder;
            this.newBook = newBook;
            this.quantity = quantity;
            cad = new CADLinePublishingOrder();
        }

        public int IdLines_POrders
        {
            get { return idLines_POrders; }
            set { idLines_POrders = value; }
        }

        public ENPublishingOrder PublishingOrder
        {
            get { return publishingOrder; }
            set { publishingOrder = value; }
        }

        public ENNew NewBook
        {
            get { return newBook; }
            set { newBook = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public Boolean insert()
        {
            try
            {
                return cad.insert(this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean update()
        {
            return false;
        }

        public Boolean delete()
        {

            try
            {
                return cad.delete(this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

