using ClasesAlicanTeam.CAD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ClasesAlicanTeam.EN
{
    public class ENSubject : AEN
    {
        protected ENCourse course;
        protected CADSubject cad;
        protected String name;
        protected int idCourse;


        public ENSubject()
        {
            course = new ENCourse();
            cad = new CADSubject();
            id = 0;
        }

        public ENSubject(String name, ENCourse course)
        {
            cad = new CADSubject();
            this.name = name;
            this.course = course;
            id = 0;
        }



        public int IdCourse
        {
            get{ return idCourse;            }
            set{ idCourse = value;}
            
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public ENCourse Course
        {
            get {
                /*if(course != null)
                {
                    return course;
                }
                else
                {*/
                    course = (new ENCourse().Read(idCourse));
                    return course;
                //}
            }
            set { course = value; }
        }

        protected override DataRow ToDataRow
        {
            get
            {
                cad = new CADSubject();
                DataRow ret = cad.GetVoidRow;
                ret["ID"] = this.id;
                ret["Name"] = name;
                ret["idCourse"] = idCourse;

                return ret;
            }
        }

        protected override void FromRow(DataRow Row)
        {
            this.id = (int)Row["ID"];
            Name = (string)Row["Name"];
            idCourse = (int)Row["idCourse"];


        }

        public override int Save()
        {
            try
            {
                if (this.id == 0)
                {
                    DataRow data = ToDataRow;
                    return id = cad.Insert(data);
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

        public virtual List<ENSubject> ReadAll()
        {
            List<ENSubject> ret = new List<ENSubject>();
            DataTable table = cad.SelectAll();

            try
            {

                foreach (DataRow row in table.Rows)
                {
                    ENSubject course = new ENSubject();
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

        public ENSubject Read(int idSubject)
        {
            cad = new CADSubject();
                ENSubject ret = new ENSubject();

                List<object> param = new List<object>();
                param.Add((object)idSubject);

                ret.FromRow(cad.Select(param));

                return ret;
        }

        public List<ENSubject> Filter(string where)
        {
            List<ENSubject> ret = new List<ENSubject>();
            DataTable table = cad.SelectWhere(where);

            try
            {

                foreach (DataRow row in table.Rows)
                {
                    ENSubject course = new ENSubject();
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
