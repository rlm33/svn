using ClasesAlicanTeam.EN;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;


namespace ClasesAlicanTeam.CAD
{
    public class CADDistributorsOrder : ICAD
    {
        public CADDistributorsOrder()
            :base()
        {
            this.tablename = "Distributors_Orders";
        }
    }
}

