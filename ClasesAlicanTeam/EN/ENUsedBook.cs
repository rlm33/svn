using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ClasesAlicanTeam.CAD;

namespace ClasesAlicanTeam.EN
{
    public class ENUsedBook : ENBook
    {
        private int books;
        private int quantity;

        #region//Getters y Setters

        /// <summary>
        /// Devuelve y establece el Books del libro.
        /// </summary>
        public int Books
        {
            get
            {
                return this.books;
            }
            set
            {
                this.books = value;
            }
        }

        /// <summary>
        /// Devuelve y establece la cantidad de libros.
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

        #endregion

        #region//Private Methods

        protected override DataRow ToDataRow
        {
            get
            {
                cad = new CADUsedBook();
                DataRow ret = cad.GetVoidRow;
                ret["ID"] = this.id;
                ret["Books"] = this.books;
                ret["Name"] = this.Name;
                ret["Quantity"] = this.quantity;
                return ret;
            }
        }

        protected override void FromRow(DataRow Row)
        {
            ENBook t = base.Read((int)Row["Books"]);
            this.Subject = t.Subject;
            this.Bussiness = t.Bussiness;
            this.Name = t.Name;
            this.Description = t.Description;
            this.Picture = t.Picture;
            this.id = (int)Row["ID"];
            this.quantity = (int)Row["Quantity"];
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor por defecto que inicializa el objeto con sus campos vacíos.
        /// </summary>
        public ENUsedBook()
            : base()
        {
            cad = new CADUsedBook();
            Books = 0;
            quantity = 0;
        }

        /// <summary>
        /// Constructor sobrecargado que inicializa el objeto con los datos pasados por parámetro.
        /// </summary>
        /// <param name="book">Identificador de la clase ENBook padre del libro.</param>
        /// <param name="quantity">Cantidad de libros</param>
        public ENUsedBook(int book, int quantity)
            :base()
        {
            cad = new CADUsedBook();
            this.books = book;
            this.quantity = quantity;
        }

        /// <summary>
        /// Busca el libro usado en la base de datos y lo devuelve.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  ENUsedBook Read(int id)
        {
            ENUsedBook ret = new ENUsedBook();
            List<object> param = new List<object>();
            param.Add((object)id);
            ret.FromRow(cad.Select(param));
            return ret;
        }

        /// <summary>
        /// Devuelve todos los libro usados que existen en la base de datos.
        /// </summary>
        /// <returns>Lista de ENUsedBooks con todos los libros usados de la base de datos.</returns>
        public  List<ENUsedBook> ReadAll()
        {
            List<ENUsedBook> ret = new List<ENUsedBook>();
            DataTable tabla = cad.SelectAll();
            foreach(DataRow rows in tabla.Rows)
            {
                ENUsedBook nuevo = new ENUsedBook();
                nuevo.FromRow(rows);
                ret.Add(nuevo);
            }
            return ret;
        }


        public List<ENUsedBook> Filter(String where)
        {
            List<ENUsedBook> ret = new List<ENUsedBook>();
            DataTable table = cad.SelectWhere(where);

            try
            {

                foreach (DataRow row in table.Rows)
                {
                    ENUsedBook course = new ENUsedBook();
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
                this.books = new CADBook().Insert(base.ToDataRow);
                cad = new CADUsedBook();
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
