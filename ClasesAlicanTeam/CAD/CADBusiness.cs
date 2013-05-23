using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClasesAlicanTeam.EN;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;



namespace ClasesAlicanTeam.CAD
{
    public class CADBusiness : ICAD
    {

        public CADBusiness()
            :base()
        {
            this.tablename = "Business";
        } 
    }
}
