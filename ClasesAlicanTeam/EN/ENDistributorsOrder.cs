using ClasesAlicanTeam.CAD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ClasesAlicanTeam.EN
{
    public class ENDistributorsOrder : AEN
    {
        private int distributor;
        private DateTime dataOrder;
        private char status;
        private ENLineDistributorsOrder line;
        private List<ENLineDistributorsOrder> lines;
        
        private float total;

        public ENDistributorsOrder()
        {
            id = 0;
            cad = new CADDistributorsOrder();
            line = new ENLineDistributorsOrder();
            status = 'P';
            total = 0;
            lines = new List<ENLineDistributorsOrder>();
        }

        public ENDistributorsOrder(int dis, DateTime DataOrder, char status)
        {
            this.id = 0;
            this.distributor = dis;
            this.dataOrder = DataOrder;
            
            cad = new CADDistributorsOrder();
            total = 0;
            total = 0;
            if (status == 'P' || status == 'C')
            {

                this.status = status;
            }
            else
            {
                this.status = 'P';
            }
            lines = new List<ENLineDistributorsOrder>();
            
        }

        public int Distributor
        {
            get { return distributor; }
            set { distributor = value; }
        }

        public DateTime DataOrder
        {
            get { return dataOrder; }
            set { dataOrder = value; }
        }

        public float Total
        {
            get
            {
                
                float total = 0;

                foreach (ENLineDistributorsOrder l in lines)
                {

                    total += l.Total;
                }

                return total;

            }

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

        public List<ENLineDistributorsOrder> Lines
        {
            get { return line.Filter("idD_Orders = " + id); }

            set { lines = value; }
            
        }

        protected override DataRow ToDataRow
        {
            get
            {
                DataRow ret = cad.GetVoidRow;
                ret["ID"] = Id;
                ret["idDistributor"] = distributor;
                ret["DataOrder"] = dataOrder;
                ret["Status"] = status;

                return ret;
            }
        }

        protected override void FromRow(DataRow Row)
        {
            Id = (int)Row["ID"];
            distributor = (int)Row["idDistributor"];
            dataOrder = (DateTime)Row["DataOrder"];
            status = ((string)Row["status"])[0];
        }


        public override int Save()
        {
            try
            {
                if (this.id == 0)
                {
                    id = cad.Insert(ToDataRow);
                    foreach (ENLineDistributorsOrder l in lines)
                    {
                        l.IdDistributorOrder = id;
                        l.Save();
                    }
                    return id;
                }
                else
                {
                    cad.Update(ToDataRow);
                    Lines = Lines;
                    foreach (ENLineDistributorsOrder l in lines)
                    {
                        l.IdDistributorOrder = id;
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

        public override void Delete()
        {

            try
            {
                

                foreach (ENLineDistributorsOrder l in Lines)
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

        public ENDistributorsOrder Read(int id)
        {
            try
            {
                ENDistributorsOrder ret = new ENDistributorsOrder();

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

        public List<ENDistributorsOrder> ReadAll()
        {
            List<ENDistributorsOrder> ret = new List<ENDistributorsOrder>();
            DataTable table = cad.SelectAll();

            try
            {

                foreach (DataRow row in table.Rows)
                {
                    ENDistributorsOrder course = new ENDistributorsOrder();
                    course.FromRow(row);
                    course.Lines = course.Lines;
                    ret.Add(course);

                }
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ENDistributorsOrder> Filter(string where)
        {
            List<ENDistributorsOrder> ret = new List<ENDistributorsOrder>();
            DataTable table = cad.SelectWhere(where);

            try
            {

                foreach (DataRow row in table.Rows)
                {
                    ENDistributorsOrder course = new ENDistributorsOrder();
                    course.FromRow(row);
                    course.Lines = course.Lines;
                    ret.Add(course);

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
            ENLineDistributorsOrder line = new ENLineDistributorsOrder(0, this.id, book, quantity);
            lines.Add(line);

        }
    }
}

