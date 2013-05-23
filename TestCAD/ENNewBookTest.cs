using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClasesAlicanTeam.EN;

namespace TestCAD
{
    [TestClass]
    public class ENNewBookTest
    {
        [TestMethod]
        public void NewBookVerifyingPrice()
        {
            var newBook = new ENNewBook();
            newBook = newBook.Read(1);
            var price = newBook.Price;
            //used to fail casting
        }

        [TestMethod]
        public void NewBookWithISBN()
        {
            var newBook1 = (new ENNewBook()).Read(1);
            Assert.AreNotEqual("", newBook1.IdBook);
        }
    }
}
