using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClasesAlicanTeam.CAD;
using System.Data;

namespace ClasesAlicanTeam.EN
{
    public class ENPublisher : ENBusiness
    {
        private int idBusiness;

        #region//Getters & Setters

        /// <summary>
        /// 
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
                ret["ID"] = this.idBusiness;
                ret["idBusiness"] = this.idBusiness;
                return ret;
            }
        }

        protected override void FromRow(DataRow Row)
        {
            ENBusiness b = base.Read((int)Row["idBusiness"]);
            this.idBusiness = b.Id;
            this.cif = b.Cif;
            this.Name = b.Name;
            this.Address = b.Address;
            this.Telephone = b.Telephone;
            this.email = b.Email;
            this.idBusiness = (int)Row["idBusiness"];
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor por defecto que inicializa el objeto con sus campos vacíos.
        /// </summary>
        public ENPublisher()
            : base()
        {
            cad = new CADPublisher();
            idBusiness = 0;
            
        }

        /// <summary>
        /// Constructor sobrecargado que inicializa el objeto con los datos pasado por parámetro.
        /// </summary>
        /// <param name="idNewBook">Identificador de la clase ENBusiness padre del Distributor.</param>
        public ENPublisher(int idBusiness)
            : base()
        {
            cad = new CADPublisher();
            this.idBusiness = idBusiness;
        }

        /// <summary>
        /// Busca la editorial nueva en la base de datos y lo devuelve
        /// </summary>
        /// <param name="id">Identificador del editorial a buscar.</param>
        /// <returns>ENPublisher de la editorial encontrado en la base de datos.</returns>
        public ENPublisher Read(int id)
        {
            ENPublisher ret = new ENPublisher();
            List<object> param = new List<object>();
            param.Add((object)id);
            ret.FromRow(cad.Select(param));
            return ret;
        }

        /// <summary>
        /// Devuelve todos las editoriales nuevas que existen en la base de datos.
        /// </summary>
        /// <returns>Lista de ENPublisher con todos las editoriales nuevas de la base de datos.</returns>
        public List<ENPublisher> ReadAll()
        {
            List<ENPublisher> ret = new List<ENPublisher>();
            DataTable tabla = cad.SelectAll();
            foreach (DataRow rows in tabla.Rows)
            {
                ENPublisher nuevo = new ENPublisher();
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
                cad = new CADPublisher();
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
