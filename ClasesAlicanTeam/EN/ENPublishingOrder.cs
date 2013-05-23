using ClasesAlicanTeam.CAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesAlicanTeam.EN
{
    public class ENPublishingOrder
    {
        private int idHouse_Orders;
        private ENDistributor cif;
        private DateTime dataOrder;
        List<ENLinePublishingOrder> linespublishingorder;
        CADPublishingOrder cad;

        public ENPublishingOrder()
        {
            cad = new CADPublishingOrder();
        }

        public ENPublishingOrder(int idHouse_Orders, ENDistributor cif, DateTime DataOrder)
        {
            this.idHouse_Orders = idHouse_Orders;
            this.cif = cif;
            this.dataOrder = DataOrder;
            linespublishingorder = new List<ENLinePublishingOrder>();
            cad = new CADPublishingOrder();
        }

        public int IdHouse_Orders
        {
            get { return idHouse_Orders; }
            set { idHouse_Orders = value; }
        }

        public ENDistributor CIF
        {
            get { return cif; }
            set { cif = value; }
        }

        public DateTime DataOrder
        {
            get { return dataOrder; }
            set { dataOrder = value; }
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

