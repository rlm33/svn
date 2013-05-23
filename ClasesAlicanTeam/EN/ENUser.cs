using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ClasesAlicanTeam.CAD;

namespace ClasesAlicanTeam.EN
{
    public class ENUser : AEN
    {
        private string account;
        private string password;

        #region//Getters y Setters

        /// <summary>
        /// Devuelve y establece el nombre de cuenta del usuario.
        /// </summary>
        public string Account
        {
            get { return account; }
            set { account = value; }
        }

        /// <summary>
        /// Devuelve y establece la contraseña del usuario.
        /// </summary>
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        #endregion

        #region//Private Methods

        protected override DataRow ToDataRow
        {
            get 
            {
                DataRow ret = cad.GetVoidRow;
                ret["ID"] = this.id;
                ret["Account"] = this.account;
                ret["Password"] = this.password;
                return ret;
            }
        }

        protected override void FromRow(DataRow Row)
        {
            this.id = (int)Row["ID"];
            this.account = (string)Row["Account"];
            this.password = (string)Row["Password"];
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor por defecto que inicializa el objeto con sus campos vacios.
        /// </summary>
        public ENUser()
        {
            cad = new CADUser();
            account = "";
            password = "";
            id = 0;
        }

        /// <summary>
        /// Constructor sobrecargado que inicializa el objeto con los datos pasados por parámetro.
        /// </summary>
        /// <param name="account">Nombre de la cuenta del usuario.</param>
        /// <param name="password">Contraseña del usuario.</param>
        public ENUser(String account, String password)
        {
            this.account = account;
            this.password = password;
            this.id = 0;
            cad = new CADUser();
        }

        /// <summary>
        /// Busca el usuario en la base de datos y lo devuelve.
        /// </summary>
        /// <param name="id">Identificador del usuario a buscar.</param>
        /// <returns>ENUser del usuario encontrado en la base de datos.</returns>
        public ENUser Read(int id)
        {
            ENUser ret = new ENUser();

            List<object> param = new List<object>();
            param.Add((object)id);
            cad = new CADUser();
            ret.FromRow(cad.Select(param));
            return ret;
        }

        public List<ENUser> ReadAll()
        {
            List<ENUser> ret = new List<ENUser>();
            cad = new CADUser();
            DataTable tabla = cad.SelectAll();
            foreach (DataRow rows in tabla.Rows)
            {
                ENUser nuevo = new ENUser();
                nuevo.FromRow(rows);
                ret.Add(nuevo);
            }
            return ret;
        }

        /// <summary>
        /// Consulta en la base de datos si el nombre de cuenta y contraseña existen.
        /// </summary>
        /// <param name="account">Nombre de la cuenta.</param>
        /// <param name="password">Contraseña de la cuenta.</param>
        /// <returns>True en caso de exisitr en la base de datos, false en caso contrario.</returns>
        public Boolean LogIn(string account, string password)
        {
            cad = new CADUser();
            DataTable log = cad.SelectWhere("Account = \""+account+"\" AND Password = \""+password+"\"");
            if (log.Rows.Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<ENUser> Filter(String where)
        {
            List<ENUser> ret = new List<ENUser>();

            cad = new CADUser();

            DataTable table = cad.SelectWhere(where);

            try
            {

                foreach (DataRow row in table.Rows)
                {
                    ENUser course = new ENUser();
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
