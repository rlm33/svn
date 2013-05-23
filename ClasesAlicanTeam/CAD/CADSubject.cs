using ClasesAlicanTeam.EN;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ClasesAlicanTeam.CAD
{
    public class CADSubject : ICAD
    {
        public CADSubject()
            :base()
        {
            this.tablename = "Subjects";
        }
    }
}
