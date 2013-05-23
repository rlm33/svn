using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClasesAlicanTeam.CAD;
using System.Data;


namespace ClasesAlicanTeam.EN
{
    public class ENDistributor : ENBusiness
    { 
        private int idBusiness;

        #region//Getters & Setters

        /// <summary>
        /// Devuelve y establece el idDistributor del distribuidor.
        /// </summary>
        public int IdBusiness
        {
            get
            {
                return this.idBusiness;
            }
            set
            {
                this.idBusiness = value;
            }
        }

        #endregion

        #region//Private Methods

        protected override DataRow ToDataRow
        {
            get
            {
                DataRow ret = cad.GetVoidRow;
                ret["ID"] = this.Id;
                ret["idBusiness"] = this.idBusiness;
                return ret;
            }
        }

        protected override void FromRow(DataRow Row)
        {
            this.Id = (int)Row["ID"];
            ENBusiness b = base.Read((int)Row["idBusiness"]);
            this.idBusiness = b.Id;
            this.Cif = b.Cif;
            this.Name = b.Name;
            this.Address = b.Address;
            this.Telephone = b.Telephone;
            this.Email = b.Email;
            this.IdBusiness = (int)Row["idBusiness"];
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor por defecto que inicializa el objeto con sus campos vacíos.
        /// </summary>
        public ENDistributor()
            : base()
        {
            cad = new CADDistributor();
            idBusiness = 0;
        }

        /// <summary>
        /// Constructor sobrecargado que inicializa el objeto con los datos pasado por parámetro.
        /// </summary>
        /// <param name="idBusiness">Identificador de la clase ENBusiness padre del distribuidor.</param>
        public ENDistributor(int idBusiness)
            : base()
        {
            cad = new CADDistributor();
            this.IdBusiness = idBusiness;
        }

        /// <summary>
        /// Busca el distribuidor en la base de datos y lo devuelve
        /// </summary>
        /// <param name="id">Identificador del distribuidor a buscar.</param>
        /// <returns>ENDistributor del distribuidor encontrado en la base de datos.</returns>
        public ENDistributor Read(int id)
        {
            ENDistributor ret = new ENDistributor();
            List<object> param = new List<object>();
            param.Add((object)id);
            ret.FromRow(cad.Select(param));
            return ret;
        }

        /// <summary>
        /// Devuelve todos los distribuidores nuevos que existen en la base de datos.
        /// </summary>
        /// <returns>Lista de ENDistributor con todos los distribuidores de la base de datos.</returns>
        public List<ENDistributor> ReadAll()
        {
            List<ENDistributor> ret = new List<ENDistributor>();
            DataTable tabla = cad.SelectAll();
            foreach (DataRow rows in tabla.Rows)
            {
                ENDistributor nuevo = new ENDistributor();
                nuevo.FromRow(rows);
                ret.Add(nuevo);
            }
            return ret;
        }

        public override int Save()
        {
            if (id == 0)
            {
                this.idBusiness = new CADBusiness().Insert(base.ToDataRow);
                cad = new CADDistributor();
                return this.id = cad.Insert(ToDataRow);
            }
            else
            {
                cad.Update(ToDataRow);
                return 0;
            }
        }

        #endregion
    }
}
