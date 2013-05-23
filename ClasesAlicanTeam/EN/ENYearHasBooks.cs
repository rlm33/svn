using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClasesAlicanTeam.CAD;

namespace ClasesAlicanTeam.EN
{
    public class ENYearHasBooks
    {
        private ENYear year;
        private ENBook book;
        private CADYearsHasBooks cadYearHasBook;

        public ENYearHasBooks()
        {
            cadYearHasBook = new CADYearsHasBooks();
            year = new ENYear();
            book = new ENBook();
        }

        public ENYearHasBooks(ENBook book, ENYear year)
        {
            this.book = book;
            this.year = year;
        }

        /*
        public  bool insert()
        {
            return cadYearHasBook.insert(this);
        }

        public  bool update(ENYearHasBooks oldFila, ENYearHasBooks newFila)
        {
            return cadYearHasBook.update(oldFila, newFila);
        }

        public  bool delete()
        {
            return cadYearHasBook.delete(this);
        }

        public List<ENYear> readBook (ENBook oldBook){
            return cadYearHasBook.readBook(oldBook);
        }

        public List<ENBook> readYear(ENYear oldYear){
            return cadYearHasBook.readYear(oldYear);
        }
        */

        public ENBook Book
        {
            get { return book; }
            set { book = value; }
        }
        public ENYear Year
        {
            get { return year; }
            set { year = value; }
        }




    }
}
