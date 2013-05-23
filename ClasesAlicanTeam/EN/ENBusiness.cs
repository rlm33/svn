using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ClasesAlicanTeam.CAD;

namespace ClasesAlicanTeam.EN
{
    public class ENBusiness : AEN
    {
        protected CADBusiness cadBusiness;
        protected String cif;
        protected String name;
        protected String address;
        protected int telephone;
        protected String email;

        public ENBusiness()
        {
            cadBusiness = new CADBusiness();
            cif = "";
            name = "";
            address = "";
            telephone = 0;
            email = "";
        }

        public ENBusiness(String cif, String name, String address, int telephone, String email)
        {
            this.cif = cif;
            this.name = name;
            this.address = address;
            this.telephone = telephone;
            this.email = email;
            id = 0;
            cadBusiness = new CADBusiness();
        }

        public String Cif
        {
            get { return cif; }
            set { cif = value; }
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public String Address
        {
            get { return address; }
            set { address = value; }
        }

        public int Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        }

        public String Email
        {
            get { return email; }
            set { email = value; }
        }

        protected override DataRow ToDataRow
        {
            get
            {
                cad = new CADBusiness();
                DataRow ret = cad.GetVoidRow;
                ret["ID"] = id;
                ret["CIF"] = cif;
                ret["Name"] = name;
                ret["Address"] = address;
                ret["Telephone"] = telephone;
                ret["Email"] = email;
                return ret;
            }
        }

        protected override void FromRow(DataRow Row)
        {
            
            id = (int)Row["id"];
            cif = (string)Row["cif"];
            name =(string) Row["name"];
            address = (string)Row["address"];
            telephone = (int)Row["telephone"];
            email = (string)Row["email"];
            
        }

        public ENBusiness Read(int id)
        {
                cad = new CADBusiness();
                ENBusiness ret = new ENBusiness();

                List<object> param = new List<object>();
                param.Add((object)id);

                ret.FromRow(cad.Select(param));

                return ret;
        }

        public List<ENBusiness> ReadAll()
        {
            List<ENBusiness> ret = new List<ENBusiness>();
            DataTable table = cad.SelectAll();

            try
            {

                foreach (DataRow row in table.Rows)
                {
                    ENBusiness course = new ENBusiness();
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

        public List<ENBusiness> Filter(String where)
        {
            List<ENBusiness> ret = new List<ENBusiness>();
            DataTable table = cad.SelectWhere(where);

            try
            {

                foreach (DataRow row in table.Rows)
                {
                    ENBusiness course = new ENBusiness();
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
