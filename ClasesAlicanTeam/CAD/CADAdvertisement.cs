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
    public class CADAdvertisement : ICAD
    {

        public CADAdvertisement()
            : base()
        {
            this.tablename = "Advertisements";
        }

        /// <summary>
        /// Carga los anuncios de un cliente
        /// </summary>
        /// <param name="id">Id del cliente.</param>
        /// <returns>DataTable de los anuncios que tiene el cliente.</returns>
        public DataTable FromCustomer(int id)
        {
            try
            {
                string query = "SELECT * FROM " + this.tablename + " WHERE idCustomers = " + id;
                connect();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet dSet = new DataSet();
                adapter.Fill(dSet, this.tablename);
                DataTable dTable = dSet.Tables[this.tablename];
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
