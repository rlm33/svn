using ClasesAlicanTeam.EN;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ClasesAlicanTeam.CAD
{
    public class CADCustomer : CADUser
    {

        public CADCustomer()
            :base()
        {
            this.tablename = "Customers";
        }
    }
}
