using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using ClasesAlicanTeam.EN;
using System.Data;
using System.Linq;
using System.Text;

namespace ClasesAlicanTeam.CAD
{
    public class CADYearsHasBooks : ICAD
    {


        public CADYearsHasBooks()
            :base()
        {
            this.tablename = "Books_Has_Years";
        }
    }
}
