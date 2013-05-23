using ClasesAlicanTeam.CAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClasesAlicanTeam.EN
{
    public class ENCustomerOrder : AEN
    {
        private int cOrder;
        private int idCustomer;
        private DateTime dataOrder;
        private ENLineCustomerOrder line;
        private char status;
        private float total;
        private List<ENLineCustomerOrder> lines;

        #region Attributes
        /// <summary>
        /// Devuelve y establece el COrder del Pedido.
        /// </summary>
        public int IdCustomerOrder
        {
            get { return cOrder; }
            set { cOrder = value; }
        }

        /// <summary>
        /// Devuelve y establece el ENCustormer del Pedido.
        /// </summary>
        public ENCustomer Customer
        {
            get;
            set;
        }

        public int CustomerId
        {
            get
            {
                if (this.Customer == null)
                {
                    return 0;
                }
                else
                {
                    return this.Customer.Id;
                }
            }
            set
            {
                this.Customer = (new ENCustomer()).Read(value);
            }
        }

        /// <summary>
        /// Devuelve y establece la fecha del Pedido.
        /// </summary>
        public DateTime DataOrder
        {
            get { return dataOrder; }
            set { dataOrder = value; }
        }

        public char Status
        {
            get { return status; }
            set
            {

                if (value == 'P' || value == 'C') status = value;
                else status = 'P';
            }
        }

        /// <summary>
        /// Establece las lineas del Pedido.
        /// </summary>
        /// 
        /*
        public List<ENLineCustomerOrder> Linecustomerorder
        {
            get { return line.Filter("COrder = " + id); }
        }
        */

        public List<ENLineCustomerOrder> Lines
        {
            get { return line.Filter("COrder = " + id); }

            set { lines = value; }
        }
        
        public float Total
        {
            get
            {

                float total = 0;

                foreach (ENLineCustomerOrder l in Lines)
                {

                    total += l.Total;
                }

                return total;

            }

        }

        /// <summary>
        /// Devuelve el total del precio del pedido.
        /// </summary>
        /*public float Total
        {
            get 
            {
                total = 0;
                List<ENLineCustomerOrder> lineas = Linecustomerorder;
                    foreach(ENLineCustomerOrder lin in lineas)
                    {
                        total += lin.Total;
                    }
                return total; 
            }
        }*/
        #endregion

        /// <summary>
        /// Constructor por defecto que inicializa el objeto con sus campos vacíos. 
        /// </summary>
        public ENCustomerOrder()
        {
            id = 0;
            total = 0;
            status = 'P';
            line = new ENLineCustomerOrder();
            cad = new CADCustomerOrder();
            total = 0;
            lines = new List<ENLineCustomerOrder>();
        }

        /// <summary>
        /// Constructor sobrecargado que inicializa el objeto con los datos que se la pasan por parámetro y deja las lineas de pedido vacías.
        /// </summary>
        /// <param name="cOrder">Identificador del pedido.</param>
        /// <param name="customer">ENCustomer que realiza el pedido.</param>
        /// <param name="DataOrder">Fecha en la que se realiza el pedido.</param>
        public ENCustomerOrder(int cOrder, int customer, DateTime DataOrder, char status)
        {
            id = 0;
            this.cOrder = cOrder;
            this.idCustomer = customer;
            this.dataOrder = DataOrder;
            this.total = 0;
            this.status = status;
            line = new ENLineCustomerOrder();
            cad = new CADCustomerOrder();
            total = 0;
            lines = new List<ENLineCustomerOrder>();
        }

        protected override DataRow ToDataRow
        {
            get 
            {
                DataRow ret = cad.GetVoidRow;
                ret["id"] = this.id;
	            ret["idCustomers"] = CustomerId;
	            ret["DataOrder"] = dataOrder;
                ret["Status"] = status;
                return ret;
            }
        }

        protected override void FromRow(DataRow Row)
        {
            this.id = (int)Row["id"];
            CustomerId = (int)Row["idCustomers"];
            dataOrder = (DateTime)Row["DataOrder"];
            status = ((string)Row["Status"])[0];
        }

        public override void Delete()
        {
            try
            {
                foreach (ENLineCustomerOrder l in Lines)
                {
                    l.Delete();
                }
                cad.Delete(ToDataRow);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override int Save()
        {
            try
            {
                if (this.id == 0)
                {
                    id = cad.Insert(ToDataRow);
                    foreach (ENLineCustomerOrder l in lines)
                    {
                        l.IdcustomerOrder = id;
                        l.Save();
                    }
                    return id;
                }
                else
                {
                    cad.Update(ToDataRow);

                    Lines = Lines;
                    foreach (ENLineCustomerOrder l in lines)
                    {
                        l.IdcustomerOrder = id;
                        l.Save();
                    }
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ENCustomerOrder Read(int id)
        {
            try
            {
                ENCustomerOrder ret = new ENCustomerOrder();

                List<object> param = new List<object>();
                param.Add((object)id);

                ret.FromRow(cad.Select(param));

                Lines = ret.Lines;

                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ENCustomerOrder> ReadAll()
        {
            List<ENCustomerOrder> ret = new List<ENCustomerOrder>();
            DataTable table = cad.SelectAll();

            try
            {

                foreach (DataRow row in table.Rows)
                {
                    ENCustomerOrder co = new ENCustomerOrder();
                    co.FromRow(row);
                    //co.Lines = co.Lines;
                    ret.Add(co);

                }
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ENCustomerOrder> Filter(String where)
        {
            List<ENCustomerOrder> ret = new List<ENCustomerOrder>();
            DataTable table = cad.SelectWhere(where);

            try
            {
                foreach (DataRow row in table.Rows)
                {
                    ENCustomerOrder co = new ENCustomerOrder();
                    co.FromRow(row);
                    co.Lines = co.Lines;
                    ret.Add(co);
                }
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddLine(ENNewBook book, int quantity)
        {
            ENLineCustomerOrder line = new ENLineCustomerOrder(0, this.id, book, quantity);
            lines.Add(line);
        }
    }
}
