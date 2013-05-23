using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesAlicanTeam.CAD
{
    public class CADAdministrator : ICAD
    {
        public CADAdministrator()
            : base()
        {
            this.tablename = "Administrators";
        }
    }
}
