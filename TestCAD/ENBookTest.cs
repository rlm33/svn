using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClasesAlicanTeam.EN;
using System.Collections.Generic;

namespace TestCAD
{
    [TestClass]
    public class ENBookTest
    {
        [TestMethod]
        public void BookConnecting()
        {
            var actual = new List<ENBook>();
            var book = new ENBook();
            Assert.AreEqual(0, book.Id);
        }

        [TestMethod]
        public void BookReadingAll()
        {
            var actual = new List<ENBook>();
            var book = new ENBook();
            actual = book.ReadAll();
        }

        [TestMethod]
        public void BookInsert()
        {
            var book = new ENBook();
            book.IdBook = "testInsertID";
            book.Name = "testInsertName";
            var testSubject = new ENSubject();
            testSubject = testSubject.Read(1);
            book.Subject = testSubject;
            book.Bussiness = (new ENBusiness()).Read(1);
            book.Save();
            var bookList = book.ReadAll();
            var actual = bookList[bookList.Count - 1];
            actual.Delete();
            Assert.AreEqual("testInsertID", actual.IdBook);
        }

        [TestMethod]
        public void BookUpdate()
        {
            var book = new ENBook();
            book = book.Read(1);
            var oldName = book.Name;
            book.Name = "testUpdateName";
            book.Save();
            var actual = book.Read(1);
            var actualName = actual.Name;
            actual.Name = oldName;
            actual.Save();
            Assert.AreEqual("testUpdateName", actualName);
        }

    }
}
