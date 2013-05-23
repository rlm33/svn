using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;

namespace ClasesAlicanTeam.CAD
{
    public abstract class ICAD
    {
        protected string tablename;
        protected List<string> idFormat;
        private bool rowReturned;
        private DataRow voidRow;
        private String sqlConnectionString;
        protected SqlConnection connection = null;

        #region //Private Methods
        private void init()
        {
            sqlConnectionString = ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString;
            rowReturned = false;
        }

        protected void connect()
        {

            try
            {
                connection = new SqlConnection(sqlConnectionString);
                connection.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void disconnect()
        {
            while (connection.State == ConnectionState.Executing)
            {
            }
            connection.Close();
        }

        private int last()
        {
            string sql = "SELECT MAX(ID) FROM " + this.tablename;
            SqlCommand command = new SqlCommand(sql, connection);
            return (int)command.ExecuteScalar();
        }

        #endregion

        #region //Public Methods

        /// <summary>
        /// Constructor que inicializa la objeto.
        /// </summary>
        public ICAD()
        {
            init();
            this.idFormat = new List<string>();
            this.idFormat.Add("ID");
        }

        /// <summary>
        /// Obtiene y establece el formato de la columna que identifica cada registro en la base de datos.
        /// </summary>
        public List<string> IdFormat
        {
            get
            {
                return this.idFormat;
            }
        }

        /// <summary>
        /// Obtiene el nombre de la tabla en la base de datos.
        /// </summary>
        public string TableName
        {
            get { return tablename; }
        }

        /// <summary>
        /// Devuelve una tabla con todos los registros de la misma.
        /// </summary>
        /// <param name="startRecord">Registro por el que se empezará a llenar la tabla.</param>
        /// <param name="maxRecord">Numero máximo de registros que se devolverá.</param>
        /// <returns>DataTable con los datos de la base de datos.</returns>
        public DataTable SelectAll(int startRecord = 0, int maxRecord = -1)
        {
            try
            {
                connect();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM " + tablename, connection);
                DataTable datatable = new DataTable();
                DataSet dataset = new DataSet();
                if (maxRecord < 0)
                {
                    adapter.Fill(dataset, tablename);
                    datatable = dataset.Tables[tablename];
                }
                else
                {
                    adapter.Fill(startRecord, maxRecord, datatable);
                }
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                disconnect();
            }
        }

        /// <summary>
        /// Obtiene una fila vacía lista para rellenar.
        /// </summary>
        /// <returns>DataRow vacía.</returns>
        public virtual DataRow GetVoidRow
        {
            get
            {
                DataRow ret;
                if (!rowReturned || voidRow == null)
                {
                    DataTable datatable = SelectAll(1, 1);
                    ret = datatable.NewRow();
                    voidRow = ret;
                    rowReturned = true;
                }
                else
                {
                    ret = voidRow;
                }
                return ret;
            }
        }

        /// <summary>
        /// Devuelve un DataRow con el registro indicado en el id. El formato de este id dependerá del IdFormat. En caso de que no lo encuentre devuelve null.
        /// </summary>
        /// <param name="id">Lista con los identificadores de la tabla, solo uno en casod de campo simple.</param>
        /// <returns>Devuelve una fila de la base de datos.</returns>
        public virtual DataRow Select(List<object> id)
        {
            try
            {
                if (id.Count != this.IdFormat.Count)
                {
                    throw new Exception("Invalid number of id");
                }
                string query = "SELECT * FROM " + this.TableName + " WHERE ";
                for (int i = 0; i < id.Count; i++)
                {
                    query += this.IdFormat[i] + " = " + id[i].ToString() + " ";
                }
                connect();
                
                
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                
                DataSet dSet = new DataSet();
                adapter.Fill(dSet, this.TableName);
                DataTable dTable = dSet.Tables[this.TableName];
                rowReturned = true;
                voidRow = dTable.NewRow();
                if (dTable.Rows.Count == 1)
                {
                    return dTable.Rows[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                disconnect();
            }
        }

        /// <summary>
        /// Devuelve un DataTable con una ejecucion de la base de datos con la clausula where pasada por parámetro.
        /// </summary>
        /// <param name="whereStatement">Clausula where, deberá estar bien formada.</param>
        /// <param name="startRecord">Registro por el que se empezará a llenar la tabla.</param>
        /// <param name="maxRecord">Numero máximo de registros que se devolverá.</param>
        /// <returns>DaTatable con los datos de la base de datos.</returns>
        public virtual DataTable SelectWhere(string whereStatement, int startRecord = 0, int maxRecords = -1)
        {
            try
            {
                connect();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM " + this.TableName + " WHERE " + whereStatement, connection);
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                rowReturned = true;
                voidRow = dt.NewRow();
                if (maxRecords < 0)
                {
                    adapter.Fill(ds, this.TableName);
                    dt = ds.Tables[this.TableName];
                }
                else
                {
                    adapter.Fill(startRecord, maxRecords, dt);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                disconnect();
            }
        }

        /// <summary>
        /// INserta la fila que se le pasa por parámetro en la base de datos.
        /// </summary>
        /// <param name="newRow">Fila a insertar en la base de datos.</param>
        /// <returns>Devuelve un entero con el ID del nuevo registro.</returns>
        public virtual int Insert(DataRow newRow)
        {
            try
            {
                connect();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM " + this.TableName, connection);

                DataSet dSet = new DataSet();
                adapter.Fill(dSet, this.TableName);
                DataTable dTable = dSet.Tables[this.TableName];
                rowReturned = true;

                voidRow = dTable.NewRow();
                DataRow addRow = dTable.NewRow();
                addRow.ItemArray = newRow.ItemArray;
                dTable.Rows.Add(addRow);

                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Update(dSet, tablename);

                return this.last();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                disconnect();
            }
        }

        /// <summary>
        /// Modifica en la base de datos la fila pasada por parámetro. La fila que se modifica viene indicada por el Identificador de la fila pasada por parámetro.
        /// </summary>
        /// <param name="newRow">Fila a modificar en la base de datos.</param>
        public virtual void Update(DataRow newRow)
        {
            try
            {
                string query = "SELECT * FROM " + this.TableName + " WHERE ";
                for (int i = 0; i < IdFormat.Count; i++)
                {
                    query += IdFormat[i] + " = " + newRow[IdFormat[i]] + " ";
                }
                connect();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                DataSet dSet = new DataSet();
                adapter.Fill(dSet, this.TableName);
                DataTable dTable = dSet.Tables[this.TableName];
                rowReturned = true;

                dTable.Rows[0].BeginEdit();
                dTable.Rows[0].ItemArray = newRow.ItemArray;
                dTable.Rows[0].EndEdit();

                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Update(dSet, tablename);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                disconnect();
            }
        }

        /// <summary>
        /// Elimina en la base de datos la fila pasada por parámetro.
        /// </summary>
        /// <param name="delRow">Fila a eliminar.</param>
        public virtual void Delete(DataRow delRow)
        {
            try
            {
                string query = "SELECT * FROM " + this.TableName + " WHERE ";
                for (int i = 0; i < IdFormat.Count; i++)
                {
                    query += IdFormat[i] + " = " + delRow[IdFormat[i]] + " ";
                }
                connect();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                DataSet dSet = new DataSet();
                adapter.Fill(dSet, this.TableName);
                DataTable dTable = dSet.Tables[this.TableName];
                rowReturned = true;

                dTable.Rows[0].Delete();

                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Update(dSet, tablename);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                disconnect();
            }
        }

        #endregion
    }
}
