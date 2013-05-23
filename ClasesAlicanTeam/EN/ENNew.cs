using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClasesAlicanTeam.CAD;


namespace ClasesAlicanTeam.EN
{
    public class ENNew : ENBook
    {

        private CADNew cadNew;
        private float precio;



        public ENNew() : base()
        {
            
            cadNew = new CADNew();
        }

       public ENNew(String idBook, ENSubject subject, ENCourse course, 
                    String cif, ENYear years, String name, int quantity, String description) : 
              base(idBook,  subject,  course, 
                   cif,  years,  name,  quantity,  description)
        {
            cadNew = new CADNew();
        }


        public  bool insert()
        {
            try
            {
                return cadBook.insert(this) && cadNew.insert(this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool update()
        {
            try
            {
                return cadBook.update(this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool delete()
        {
            try
            {
                return cadNew.delete(this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ENBook read(String id)
        {
            ENBook book;
            ENNew nuevo;
            book = cadBook.read(id);
            nuevo = cadNew.read(id);
            book.Quantity = nuevo.quantity;
            return book;
        }

        public List<ENBook> readAll()
        {
            return cadNew.readAll();
        }

        public float Precio
        {
            get { return precio; }
            set { precio = value; }
        }
    }
}
