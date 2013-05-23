using ClasesAlicanTeam.EN;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ClasesAlicanTeam.CAD
{
    public class CADCustomerOrder : ICAD
    {

        public  CADCustomerOrder()
            :base()
        {
            this.tablename = "Customers_Orders";
        }
    }
}
