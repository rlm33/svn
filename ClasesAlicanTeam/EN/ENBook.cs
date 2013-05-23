using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ClasesAlicanTeam.CAD;

namespace ClasesAlicanTeam.EN
{
    public class ENBook : AEN
    {
        private string idBook;
        private ENSubject subject;
        private int subjectToLoad;
        private int businessToLoad;
        private ENBusiness business;
        private string name;
        private string description;
        private string picture;

        #region//Getters & Setters

        /// <summary>
        /// Devuelve y establece el idBook del libro.
        /// </summary>
        public string IdBook
        {
            get
            {
                return this.idBook;
            }

            set
            {
                this.idBook = value;
            }
        }

        /// <summary>
        /// Devuelve y establece la asignatura del libro.
        /// </summary>
        public ENSubject Subject
        {
            get
            {
                if (this.subject == null)
                {
                    if (subjectToLoad <= 0)
                    {
                        return null;
                    }
                    subject = (new ENSubject()).Read(subjectToLoad);
                    subjectToLoad = -1;
                }

                return this.subject;
            }
            set
            {
                this.subject = value;
                subjectToLoad = -1;
            }
        }

        /// <summary>
        /// Obtiene y establece el identificador de la asignatura del libro.
        /// </summary>
        public int SubjectId
        {
            get
            {
                if (this.Subject != null)
                {
                    return this.Subject.Id;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (this.Subject == null)
                {
                    this.Subject = new ENSubject();
                }
                this.Subject = this.Subject.Read(value);
            }
        }

        /// <summary>
        /// Devuelve y establece el Bussinesss del libro.
        /// </summary>
        public ENBusiness Bussiness
        {
            get
            {
                if (this.business == null)
                {
                    if (businessToLoad <= 0)
                    {
                        return null;
                    }
                    business = (new ENBusiness()).Read(businessToLoad);
                    businessToLoad = -1;
                }

                return this.business;
            }
            set
            {
                this.business = value;
            }
        }

        /// <summary>
        /// Devuelve y establece el negocio del libro.
        /// </summary>
        public int BusinessId
        {
            get
            {
                if (this.Bussiness != null)
                {
                    return this.Bussiness.Id;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (this.Bussiness == null)
                {
                    this.Bussiness = new ENBusiness();
                }
                this.Bussiness = this.Bussiness.Read(value);
            }
        }

        /// <summary>
        /// Devuelve y establece el nombre del libro.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// Devuelve y establece la descripcion del libro.
        /// </summary>
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        /// <summary>
        /// Devuelve y establece la ruta a la imagen del libro.
        /// </summary>
        public string Picture
        {
            get
            {
                return picture;
            }
            set
            {
                picture = value;
            }
        }

        #endregion

        #region//Private Methods

        protected override DataRow ToDataRow
        {
            get 
            {
                cad = new CADBook();
                DataRow ret = cad.GetVoidRow;
                ret["ID"] = this.id;
                ret["idBooks"] = this.idBook;

                if (this.subject == null)
                {
                    ret["idSubject"] = subjectToLoad;
                }
                else
                {
                    ret["idSubject"] = this.subject.Id;
                }

                if (this.business == null)
                {
                    ret["iDBusiness"] = businessToLoad;
                }
                else
                {
                    ret["idBusiness"] = this.business.Id;
                }
                ret["Name"] = this.name;
                ret["Description"] = this.description;
                ret["Picture"] = this.picture;
                return ret;
            }
        }

        protected override void FromRow(DataRow Row)
        {
            this.id = (int)Row["ID"];
            this.idBook = (string)Row["idBooks"];
            this.businessToLoad = (int)Row["idBusiness"];
            this.subjectToLoad = (int)Row["idSubject"];
            this.name = (string)Row["Name"];
            this.description = (string)Row["Description"];
            this.picture = (string)Row["Picture"];
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor por defecto que inicializa el objeto con sus campos vacios.
        /// </summary>
        public ENBook()
        {
            cad = new CADBook();
            id = 0;
            idBook = "";
            subject = null;
            business = null;
            name = "";
            description = "";
            picture = "";
            subjectToLoad = 0;
            businessToLoad = 0;
        }

        /// <summary>
        /// Constructor sobrecargado que inicializa el objeto con los datos pasados por parámetro.
        /// </summary>
        /// <param name="idBook">idBook del libro.</param>
        /// <param name="subject">Asignatura a la que pertenece el libro.</param>
        /// <param name="business">Business del libro.</param>
        /// <param name="name">Nombre del libro.</param>
        /// <param name="description">Descripcion del libro.</param>
        /// <param name="picture">Ruta a la imagen del libro.</param>
        public ENBook(string idBook, ENSubject subject, ENBusiness business, string name, string description, string picture)
        {
            this.idBook = idBook;
            this.id = 0;
            this.subject = subject;
            this.business = business;
            this.name = name;
            this.description = description;
            this.picture = picture;
            subjectToLoad = -1;
            businessToLoad = -1;
            cad = new CADBook();
        }

        /// <summary>
        /// Busca el libro en la base de datos y lo devuelve.
        /// </summary>
        /// <param name="id">Identificador del libro a buscar.</param>
        /// <returns>ENBook del libro encontrado en la base de datos.</returns>
        public ENBook Read(int id)
        {
            cad = new CADBook();
            ENBook ret = new ENBook();
            List<object> param = new List<object>();
            param.Add((object)id);
            ret.FromRow(cad.Select(param));
            return ret;
        }

        /// <summary>
        /// Devuelve todos los libros que existen en la base de datos.
        /// </summary>
        /// <returns>Lista de ENBooks con todos los libros de la base de datos.</returns>
        public List<ENBook> ReadAll()
        {
            List<ENBook> ret = new List<ENBook>();
            DataTable tabla = cad.SelectAll();
            foreach (DataRow rows in tabla.Rows)
            {
                ENBook nuevo = new ENBook();
                nuevo.FromRow(rows);
                ret.Add(nuevo);
            }
            return ret;
        }

        public List<ENBook> Filter(String where)
        {
            List<ENBook> ret = new List<ENBook>();
            DataTable table = cad.SelectWhere(where);

            try
            {

                foreach (DataRow row in table.Rows)
                {
                    ENBook course = new ENBook();
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
