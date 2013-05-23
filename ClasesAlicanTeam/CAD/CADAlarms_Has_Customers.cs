using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ClasesAlicanTeam.CAD
{
    public class CADAlarms_Has_Customers : ICAD
    {
        public CADAlarms_Has_Customers()
            : base()
        {
            this.tablename = "Alarms_Has_Customers";
        }

        /// <summary>
        /// Devuelve una tabla con todos los identificadores de las alarmas a las que está suscrito un cliente.
        /// </summary>
        /// <param name="idCustomer">Identificador del cliente a filtrar.</param>
        /// <returns>DataTable con los ID's de las alarmas.</returns>
        public DataTable GetAlarmsFrom(int idCustomer)
        {
            try
            {
                string query = "SELECT idAlarms  FROM Alarms_Has_Customers WHERE idCustomers = " + idCustomer;
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
