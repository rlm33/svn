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
    public class CADCourse : ICAD
    {

        public CADCourse()
            :base()
        {
            this.tablename = "Courses";
        }
    }
}
