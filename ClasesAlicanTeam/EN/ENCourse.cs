using ClasesAlicanTeam.CAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClasesAlicanTeam.EN
{
    public class ENCourse : AEN
    {
        
        private String name;

        /// <summary>
        /// Constructor por defecto que inicializa el objeto con sus campos vacíos.
        /// </summary>
        public ENCourse()
        {
            cad = new CADCourse();
            id = 0;
        }

        /// <summary>
        /// Constructor sobrecargado que inicializa el objeto con los cursos que se le pasan por parámetro.
        /// </summary>
        /// <param name="courses">Nombre de los cursos.</param>
        public ENCourse(String courses)
        {
            this.name = courses;
            cad = new CADCourse();
            id = 0;
        }

        #region
        /// <summary>
        /// Devuelve y establece el nombre del curso.
        /// </summary>
        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        


        #endregion


        protected override DataRow ToDataRow
        {
            get
            {
                DataRow ret = cad.GetVoidRow;
                ret["ID"] = this.id;
                ret["Course"] = name;
                return ret;
            }
        }
        
        protected override void FromRow(DataRow Row)
        {
            id = (int)Row["ID"];
            name = (string)Row["Course"];
        }


        /// <summary>
        /// Inserta el curso que se le pasa por parámetro en la base de datos.
        /// </summary>
        /// <param name="course">ENCourse que se insertará en la base de datos.</param>
        /// <returns>Retorna el valor true en caso de que se haya insertado en la base de datos, false en caso contrario.</returns>
        public override int Save()
        {
            try
            {
                if (this.id == 0)
                {
                    return id = cad.Insert(ToDataRow);
                }
                else
                {
                    cad.Update(ToDataRow);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Elimina de la base de datos el curso que se le pasa por parámentro.
        /// </summary>
        /// <param name="course">ENCourse que se eliminará en la base de datos.</param>
        /// <returns>Retorna el valor true en caso de que se haya eliminado de la base de datos, false en caso contrario.</returns>
        public override void Delete()
        {

            try
            {
                 cad.Delete(ToDataRow);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Busca el curso en la base de datos y lo devuelve.
        /// </summary>
        /// <param name="courses">String del curso a buscar en la base de datos.</param>
        /// <returns>ENCourse del curso encontrado en la base de datos.</returns>
        public ENCourse Read(int id)
        {
            try
            {
                ENCourse ret = new ENCourse();
                
                List<object> param = new List<object>();
                param.Add((object) id);             
                
                ret.FromRow(cad.Select(param));

                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// Devuelve todos los cursos que existen en la base de datos.
        /// </summary>
        /// <returns>IList de ENCourse con todos los cursos almacenados en la base de datos.</returns>
        public List<ENCourse> ReadAll()
        {
            List<ENCourse> ret = new List<ENCourse>();
            DataTable table = cad.SelectAll();

            try
            {

                foreach (DataRow row in table.Rows)
                {
                    ENCourse course = new ENCourse();
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

        public List<ENCourse> Filter(String where)
        {
            List<ENCourse> ret = new List<ENCourse>();
            DataTable table = cad.SelectWhere(where);

            try
            {

                foreach (DataRow row in table.Rows)
                {
                    ENCourse course = new ENCourse();
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
    }
}
