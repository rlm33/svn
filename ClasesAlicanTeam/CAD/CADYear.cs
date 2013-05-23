using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ClasesAlicanTeam.EN;

namespace ClasesAlicanTeam.CAD
{
    public class CADYear : ICAD
    {
        public CADYear()
            :base()
        {
            this.tablename = "Years";
        }
    }
}
