using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ClasesAlicanTeam.CAD;


namespace ClasesAlicanTeam.EN
{
    public class ENNewBook : ENBook
    {
        private int idNewBooks;
        private int quantity;
        private float price;

        #region//Getters & Setters

        /// <summary>
        /// Devuelve y establece el idNewBook del libro.
        /// </summary>
        public int IdNewBooks
        {
            get
            {
                return this.idNewBooks;
            }
            set
            {
                this.idNewBooks = value;
            }
        }

        /// <summary>
        /// Devuelve y establece la cantidad que hay del libro.
        /// </summary>
        public int Quantity
        {
            get
            {
                return this.quantity;
            }
            set
            {
                this.quantity = value;
            }
        }

        /// <summary>
        /// Devuelve y establece el precio del libro.
        /// </summary>
        public float Price
        {
            get
            {
                return this.price;
            }
            set
            {
                this.price = value;
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
                ret["idNewBooks"] = this.idNewBooks;
                ret["Quantity"] = this.quantity;
                ret["Price"] = this.price;
                return ret;
            }
        }

        protected override void FromRow(DataRow Row)
        {
            ENBook t = base.Read((int)Row["idNewBooks"]);
            this.IdBook = t.IdBook;
            this.Subject = t.Subject;
            this.Bussiness = t.Bussiness;
            this.Name = t.Name;
            this.Description = t.Description;
            this.Picture = t.Picture;
            this.id = (int)Row["ID"];
            this.idNewBooks = (int)Row["idNewBooks"];
            this.quantity = (int)Row["Quantity"];
            this.price = (float)(double)Row["Price"];
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor por defecto que inicializa el objeto con sus campos vacíos.
        /// </summary>
        public ENNewBook()
            : base()
        {
            cad = new CADNewBook();
            idNewBooks = 0;
            quantity = 0;
            price = 0;
        }

        /// <summary>
        /// Constructor sobrecargado que inicializa el objeto con los datos pasado por parámetro.
        /// </summary>
        /// <param name="idNewBook">Identificador de la clase ENBook padre del libro.</param>
        public ENNewBook(int idNewBook,  int quantity, float price)
            : base()
        {
            cad = new CADNewBook();
            this.idNewBooks = idNewBook;
            this.quantity = quantity;
            this.price = price;
        }

        public ENNewBook(int id) : base()
        {
            var book = base.Read(id);
            var newbook = this.Read(id);

            this.BusinessId = book.BusinessId;
            this.Bussiness = book.Bussiness;
            this.Description = book.Description;
            this.Id = book.Id;
            this.IdBook = book.IdBook;
            this.IdNewBooks = newbook.IdNewBooks;
            this.Name = book.Name;
            this.Picture = book.Picture;
            this.Price = newbook.Price;
            this.Quantity = newbook.Quantity;
            this.Subject = book.Subject;
            this.SubjectId = book.SubjectId;
        }

        /// <summary>
        /// Busca el libro nuevo en la base de datos y lo devuelve
        /// </summary>
        /// <param name="id">Identificador del libro a buscar.</param>
        /// <returns>ENNewBook del libro encontrado en la base de datos.</returns>
        public  ENNewBook Read(int id)
        {
            ENNewBook ret = new ENNewBook();
            cad = new CADNewBook();
            List<object> param = new List<object>();
            param.Add((object)id);
            ret.FromRow(cad.Select(param));
            return ret;
        }

        /// <summary>
        /// Devuelve todos los libros nuevos que existen en la base de datos.
        /// </summary>
        /// <returns>Lista de ENNewBook con todos los libros nuevos de la base de datos.</returns>
        public  List<ENNewBook> ReadAll()
        {
            List<ENNewBook> ret = new List<ENNewBook>();
            DataTable tabla = cad.SelectAll();
            foreach (DataRow rows in tabla.Rows)
            {
                ENNewBook nuevo = new ENNewBook();
                nuevo.FromRow(rows);
                ret.Add(nuevo);
            }
            return ret;
        }

        public List<ENNewBook> Filter(String where)
        {
            List<ENNewBook> ret = new List<ENNewBook>();
            DataTable table = cad.SelectWhere(where);

            try
            {

                foreach (DataRow row in table.Rows)
                {
                    ENNewBook course = new ENNewBook();
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

        public override int Save()
        {
            if (id == 0)
            {
                this.idNewBooks = new CADBook().Insert(base.ToDataRow);
                cad = new CADNewBook();
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
