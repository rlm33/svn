using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ClasesAlicanTeam.EN;

namespace ClasesAlicanTeam.CAD
{
    public class CADObligatorySubject : CADSubject
    {
                

        public CADObligatorySubject()
            :base()
        {
            this.tablename = "Obligatory_Subjects";
        }
    }
}

