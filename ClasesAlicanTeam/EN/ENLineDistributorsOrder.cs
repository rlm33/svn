using ClasesAlicanTeam.CAD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ClasesAlicanTeam.EN
{
    public class ENLineDistributorsOrder:AEN
    {
        private int idLines_DOrders;
        private int iddistributororder;
        private ENNewBook newBook;
        private int quantity;
        private CADLineDistributorsOrder cad;
        private float total;

        public ENLineDistributorsOrder()
        {
            Id = 0;
            idLines_DOrders = 0;
            iddistributororder = 0;
            newBook = null;
            quantity = 0;
            cad = new CADLineDistributorsOrder();
            total = 0;
        }

        public ENLineDistributorsOrder(int idLines_DOrders, int iddistributororder, ENNewBook newBook, int quantity)
        {
            this.id = 0;
            this.idLines_DOrders = idLines_DOrders;
            this.iddistributororder = iddistributororder;
            this.newBook = newBook;
            this.quantity = quantity;
            cad = new CADLineDistributorsOrder();
            total = quantity * newBook.Price;
        }


        public int IdLines_DOrders
        {
            get { return idLines_DOrders; }
            set { idLines_DOrders = value; }
        }

        public int IdDistributorOrder
        {
            get { return iddistributororder; }
            set { iddistributororder = value; }
        }

        public ENNewBook NewBook
        {
            get { return newBook; }
            set { newBook = value; }
        }

        public int NewBookId
        {
            get
            {
                if (this.NewBook != null)
                {
                    return this.NewBook.Id;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                this.NewBook = (new ENNewBook()).Read(value);
            }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public float Total
        {
            get { return total = quantity * newBook.Price; }
        }

        protected override DataRow ToDataRow
        {
            get
            {
                DataRow ret = cad.GetVoidRow;
                ret["ID"] = this.id;
                ret["idLines_DOrders"] = this.idLines_DOrders;
                ret["idD_Orders"] = this.iddistributororder;
                ret["idNews"] = this.newBook.Id;
                ret["Quantity"] = this.quantity;
                return ret;
            }
        }

        protected override void FromRow(DataRow Row)
        {
            this.Id = (int)Row["ID"];
            this.idLines_DOrders = (int)Row["idLines_DOrders"];
            this.iddistributororder = (int)Row["idD_Orders"];
            this.NewBookId = (int)Row["idNews"];
            this.quantity = (int)Row["Quantity"];

        }

        public override int Save()
        {
            try
            {
                if (this.id == 0)
                {
                    return id = cad.Insert(ToDataRow);
                }
                else
                {
                    cad.Update(ToDataRow);
                    return this.Id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void Delete()
        {

            try
            {
                cad.Delete(ToDataRow);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ENLineDistributorsOrder Read(int id)
        {
            try
            {
                ENLineDistributorsOrder ret = new ENLineDistributorsOrder();

                List<object> param = new List<object>();
                param.Add((object)id);

                ret.FromRow(cad.Select(param));

                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ENLineDistributorsOrder> ReadAll()
        {
            List<ENLineDistributorsOrder> ret = new List<ENLineDistributorsOrder>();
            DataTable table = cad.SelectAll();

            try
            {

                foreach (DataRow row in table.Rows)
                {
                    ENLineDistributorsOrder course = new ENLineDistributorsOrder();
                    course.FromRow(row);
                    ret.Add(course);

                }
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ENLineDistributorsOrder> Filter(string where)
        {
            List<ENLineDistributorsOrder> ret = new List<ENLineDistributorsOrder>();
            DataTable table = cad.SelectWhere(where);

            try
            {

                foreach (DataRow row in table.Rows)
                {
                    ENLineDistributorsOrder course = new ENLineDistributorsOrder();
                    course.FromRow(row);
                    ret.Add(course);

                }
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}

