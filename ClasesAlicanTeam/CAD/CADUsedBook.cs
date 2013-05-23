using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ClasesAlicanTeam.EN;
using System.Configuration;
using System.Data;

namespace ClasesAlicanTeam.CAD
{
    public class CADUsedBook : ICAD
    {
        public CADUsedBook()
            :base()
        {
            this.tablename = "Used_Books";
        }

        public DataTable FilterByAdvertisementOrderBySubjetc()
        {
            try
            {
                string query = "SELECT Used_Books.ID, Books.Name, Books.idSubject FROM Used_Books INNER JOIN Books ON Used_Books.Books = Books.ID INNER JOIN Advertisements_Has_UBooks ON idUsed_Books = Used_Books.ID ORDER BY idSubject";
                connect();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet dSet = new DataSet();
                adapter.Fill(dSet, "Alarms");
                DataTable dTable = dSet.Tables["Alarms"];
                return dTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.disconnect();
            }
        }
    }
}
