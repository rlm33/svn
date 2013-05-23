using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ClasesAlicanTeam.CAD;

namespace ClasesAlicanTeam.EN
{
    public abstract class AEN
    {
        protected ICAD cad;

        protected int id;

        /// <summary>
        /// Convierte el objeto actual en una DataRow de la base de datos con sus campos rellenados.
        /// </summary>
        protected abstract DataRow ToDataRow
        {
            get;
        }

        /// <summary>
        /// Rellena los datos del objeto a partir de una datarow de su base de datos.
        /// </summary>
        /// <param name="Row">Registro de la base de datos de su tabla</param>
        protected abstract void FromRow(DataRow Row);

        public int Id
        {
            get { return id; }

            set { id = value; }
        }

        /// <summary>
        /// Guarda el objeto actual en la base de datos, insertandolo si es nuevo y modificandolo si ya está en la base de datos.
        /// <returns>Si != 0 me sirve para guardar en las herencias tanto en el padre como en los hijos</returns>
        /// </summary>
        public virtual int Save()
        {
            if (id == 0)
            {
                return this.id = cad.Insert(ToDataRow);
            }
            else
            {
                cad.Update(ToDataRow);
                return this.id;
            }
        }

        /// <summary>
        /// Elimina de la base de datos el objeto acutal.
        /// </summary>
        public virtual void Delete()
        {
            if (id != 0)
            {
                cad.Delete(ToDataRow);
            }
        }

    }
}
