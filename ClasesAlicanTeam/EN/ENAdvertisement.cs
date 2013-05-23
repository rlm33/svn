using ClasesAlicanTeam.CAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClasesAlicanTeam.EN
{
    public class ENAdvertisement : AEN
    {
        private int customerToLoad;
        private ENCustomer customer;
        private string description;
        private string picture;
        private List<ENUsedBook> books;
        private bool booksLoaded;

        #region //Geters & Setters

        /// <summary>
        /// Obtiene y establece el ENCustomer del anuncio.
        /// </summary>
        public ENCustomer Customer
        {
            get 
            {
                if (customerToLoad != -1 || customer == null)
                {
                    customer = customer.Read(customerToLoad);
                    customerToLoad = -1;
                }
                
                return this.customer;
            }
            set 
            { 
                customer = value;
                customerToLoad = customer.Id;
            }
        }

        /// <summary>
        /// Obtiene y establece la descripcion del anuncio.
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// Obtiene y establece la ruta a la imagen del anuncio.
        /// </summary>
        public string Picture
        {
            get { return picture; }
            set { picture = value; }
        }

        /// <summary>
        /// Obtiene la lista de libros usados que se anuncian.
        /// </summary>
        public List<ENUsedBook> Books
        {
            get
            {
                this.LoadBooks();
                return books;
            }
        }

        #endregion

        #region //Private Methods

        /// <summary>
        /// Convierte el objeto actual en una DataRow de la base de datos con sus campos rellenados.
        /// </summary>
        protected override DataRow ToDataRow
        {
            get
            {
                DataRow ret = cad.GetVoidRow;
                ret["ID"] = this.id;

                if (customer == null)
                {
                    ret["idCustomers"] = this.customerToLoad;
                }
                else
                {
                    ret["idCustomers"] = this.customer.Id;
                }

                ret["Description"] = description;
                ret["Picture"] = picture;
                return ret;
            }
        }

        protected override void FromRow(DataRow row)
        {
            this.id = (int)row["ID"];
            this.customerToLoad = (int)row["idCustomers"];
            this.description = (string)row["Description"];
            this.picture = (string)row["Picture"];
            
        }

        private void LoadBooks()
        {
            if(!booksLoaded)
            {
                DataTable dTable = new CADAdvertisements_Has_UBooks().GetBooksFrom(this.id);
                foreach (DataRow rows in dTable.Rows)
                {
                    books.Add(new ENUsedBook().Read((int)rows["idUsed_Books"]));
                }
                booksLoaded = true;
            }
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor por defecto que inicializa el objeto con sus campos vacíos.
        /// </summary>
        public ENAdvertisement()
        {
            cad = new CADAdvertisement();
            customer = new ENCustomer();
            this.id = 0;
            this.description = "";
            this.picture = "";
            customerToLoad = 0;
            books = new List<ENUsedBook>();
            
        }

        /// <summary>
        /// Constructor sobrecargado que inicializa el objeto asignando los campos que se le pasan por parámetro.
        /// </summary>
        /// <param name="customer">ENCustomer que ha creado el anuncio.</param>
        /// <param name="description">Descripcion del anuncio.</param>
        /// <param name="picture">Ruta a la imagen del anuncio.</param>
        public ENAdvertisement(ENCustomer customer, String description, String picture)
        {
            this.customer = customer;
            this.description = description;
            this.picture = picture;
            this.id = 0;
            customerToLoad = -1;
            cad = new CADAdvertisement();
            books = new List<ENUsedBook>();
        }

        /// <summary>
        /// Busca el anuncio en la base de datos y lo devuelve.
        /// </summary>
        /// <param name="idAdvertisement">Identificador del anuncio a buscar en la base de datos.</param>
        /// <returns>ENAdvertisement del anuncio encontrado en la base de datos.</returns>
        public ENAdvertisement Read(int idAdvertisement)
        {
            ENAdvertisement ret = new ENAdvertisement();
            List<object> param = new List<object>();
            param.Add((object)idAdvertisement);
            ret.FromRow(cad.Select(param));
            return ret;
        }

        /// <summary>
        /// Devuelve todos los anuncios que existen en la base de datos.
        /// </summary>
        /// <returns>IList de ENAdvertisement con todos los anuncios de la base de datos.</returns>
         public List<ENAdvertisement> ReadAll()
        {
            List<ENAdvertisement> ret = new List<ENAdvertisement>();
            DataTable tabla = cad.SelectAll();
            foreach (DataRow rows in tabla.Rows)
            {
                ENAdvertisement nuevo = new ENAdvertisement();
                nuevo.FromRow(rows);
                ret.Add(nuevo);
            }
            return ret;
        }

         public List<ENAdvertisement> ReadFromCustomer(int id)
         {
             List<ENAdvertisement> ret = new List<ENAdvertisement>();
             DataTable tabla = new CADAdvertisement().FromCustomer(id);
             foreach (DataRow rows in tabla.Rows)
             {
                 ENAdvertisement nuevo = new ENAdvertisement();
                 nuevo.FromRow(rows);
                 ret.Add(nuevo);
             }
             return ret;
         }

         public List<ENAdvertisement> Filter(String where)
         {
             List<ENAdvertisement> ret = new List<ENAdvertisement>();
             DataTable table = cad.SelectWhere(where);

             try
             {

                 foreach (DataRow row in table.Rows)
                 {
                     ENAdvertisement course = new ENAdvertisement();
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