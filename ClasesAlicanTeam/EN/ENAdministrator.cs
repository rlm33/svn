using ClasesAlicanTeam.CAD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ClasesAlicanTeam.EN
{
    public class ENAdministrator : AEN
    {
        private int idUser;

        #region//Getters & Setters

        /// <summary>
        /// Devuelve y establece el idAdministrator del administrador.
        /// </summary>
        public int IdUser
        {
            get
            {
                return this.idUser;
            }
            set
            {
                this.idUser = value;
            }
        }

        #endregion

        #region//Private Methods

        protected override DataRow ToDataRow
        {
            get
            {
                DataRow ret = cad.GetVoidRow;
                ret["ID"] = this.id;
                ret["idUser"] = this.idUser;
                return ret;
            }
        }

        protected override void FromRow(DataRow Row)
        {
            /*
            ENUser a = base.Read((int)Row["idUser"]);
            this.id = a.Id;
            this.Name = a.Name;
            this.Surname = a.Surname;
            this.Telephone = a.Telephone;
            this.Adress = a.Adress;
            this.idUser = (int)Row["idUser"];
             * */
            throw new NotImplementedException();
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor por defecto que inicializa el objeto con sus campos vacíos.
        /// </summary>
        public ENAdministrator()
            : base()
        {
            cad = new CADAdministrator();
            idUser = 0;
        }

        /// <summary>
        /// Constructor sobrecargado que inicializa el objeto con los datos pasado por parámetro.
        /// </summary>
        /// <param name="idAdministrator">Identificador de la clase ENAdministrator padre del administrador.</param>
        public ENAdministrator(int idUser)
            : base()
        {
            cad = new CADAdministrator();
            this.idUser = idUser;
        }

        /// <summary>
        /// Busca el administrador en la base de datos y lo devuelve
        /// </summary>
        /// <param name="id">Identificador del administrador a buscar.</param>
        /// <returns>ENAdministrator del administrador encontrado en la base de datos.</returns>
        public ENAdministrator Read(int id)
        {
            ENAdministrator ret = new ENAdministrator();
            List<object> param = new List<object>();
            param.Add((object)id);
            ret.FromRow(cad.Select(param));
            return ret;
        }

        /// <summary>
        /// Devuelve todos los administrador que existen en la base de datos.
        /// </summary>
        /// <returns>Lista de ENAdministrador con todos los administradores de la base de datos.</returns>
        public List<ENAdministrator> ReadAll()
        {
            List<ENAdministrator> ret = new List<ENAdministrator>();
            DataTable tabla = cad.SelectAll();
            foreach (DataRow rows in tabla.Rows)
            {
                ENAdministrator nuevo = new ENAdministrator();
                nuevo.FromRow(rows);
                ret.Add(nuevo);
            }
            return ret;
        }

        #endregion
    }
}
