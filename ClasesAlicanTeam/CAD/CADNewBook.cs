using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClasesAlicanTeam.EN;
using ClasesAlicanTeam.CAD;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ClasesAlicanTeam.CAD
{
    public class CADNewBook : CADBook
    {
        public CADNewBook() 
            :base()
        {
            this.tablename = "News_Books";
        }
    }
}
