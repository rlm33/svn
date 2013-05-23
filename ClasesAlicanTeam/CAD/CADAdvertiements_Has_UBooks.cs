using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ClasesAlicanTeam.CAD
{
    public class CADAdvertisements_Has_UBooks : ICAD
    {
        public CADAdvertisements_Has_UBooks()
            : base()
        {
            this.tablename = "Advertisements_Has_UBooks";
        }

        /// <summary>
        /// Devuelve todos los libros que se anuncian en un solo anuncio.
        /// </summary>
        /// <param name="idAdvertisement">Identificador del anuncio a buscar.</param>
        /// <returns>DataTable con los aunucios.</returns>
        public DataTable GetBooksFrom(int idAdvertisement)
        {
            try
            {
                string query = "SELECT idUsed_Books FROM " + this.tablename + " WHERE idAdvertisements = " + idAdvertisement;
                connect();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet dSet = new DataSet();
                adapter.Fill(dSet, "Books");
                DataTable dTable = dSet.Tables["Books"];
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
