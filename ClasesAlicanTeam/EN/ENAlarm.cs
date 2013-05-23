using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClasesAlicanTeam.CAD;
using System.Data;

namespace ClasesAlicanTeam.EN
{
    public class ENAlarm : AEN
    {
        private String message;

        #region //Getters & Setters

        /// <summary>
        /// Devuelve y establece el mensaje de la alarma.
        /// </summary>
        public String Message
        {
            get { return message; }
            set { message = value; }
        }
        #endregion

        #region //Private Methods


        protected override DataRow ToDataRow
        {
            get
            {
                DataRow ret = cad.GetVoidRow;
                ret["ID"] = this.id;
                ret["Message"] = message;
                return ret;
            }
        }

        protected override void FromRow(DataRow Row)
        {
            id = (int)Row["ID"];
            message = (string)Row["Message"];
        }

        #endregion

        #region //Public Methods

        /// <summary>
        /// Constructor por defecto que inicializa el objeto con sus campos vacíos.
        /// </summary>
        public ENAlarm()
        {
            cad = new CADAlarm();
            id = 0;
        }

        /// <summary>
        /// Constructor sobrecargado que inicializa el objeto, con el mensaje que se la pasa por parámetro.
        /// </summary>
        /// <param name="message">Mensaje de la alarma.</param>
        public ENAlarm(String message)
        {
            this.message = message;
            cad = new CADAlarm();
            id = 0;
        }

        /// <summary>
        /// Busca la alarma en la base de datos y lo devuelve.
        /// </summary>
        /// <param name="idAlarm">Identificador de la alarma a buscar.</param>
        /// <returns>ENAlarm de la alarma encontrada en la base de datos.</returns>
        public ENAlarm Read(int idAlarm)
        {
            ENAlarm ret = new ENAlarm();
            List<object> param = new List<object>();
            param.Add((object)idAlarm);
            ret.FromRow(cad.Select(param));
            return ret;
        }

        /// <summary>
        /// Devuelve todas las alarmas que existen en la base de datos.
        /// </summary>
        /// <returns>IList de ENAlarm con todas las alarmas de la base de datos.</returns>
        public List<ENAlarm> ReadAll()
        {
            List<ENAlarm> ret = new List<ENAlarm>();
            DataTable tabla = cad.SelectAll();
            foreach (DataRow rows in tabla.Rows)
            {
                ENAlarm nueva = new ENAlarm();
                nueva.FromRow(rows);
                ret.Add(nueva);
            }
            return ret;
        }

        public List<ENAlarm> Filter(String where)
        {
            List<ENAlarm> ret = new List<ENAlarm>();
            DataTable table = cad.SelectWhere(where);

            try
            {

                foreach (DataRow row in table.Rows)
                {
                    ENAlarm course = new ENAlarm();
                    course.FromRow(row);
                    ret.Add(course);

                }
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
