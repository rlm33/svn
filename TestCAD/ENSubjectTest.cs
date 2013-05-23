using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClasesAlicanTeam.EN;
using System.Collections.Generic;

namespace TestCAD
{
    [TestClass]
    public class ENSubjectTest
    {
        [TestMethod]
        public void SubjectConnectingAndReadingAll()
        {
            var actual = new List<ENSubject>();
            var advert = new ENSubject();
            Assert.AreEqual(0, advert.Id);
            actual = advert.ReadAll();
        }

        [TestMethod]
        public void SubjectInsert()
        {
            var course = new ENCourse();
            var subject = new ENSubject();
            subject.Name = "nameTest";
            subject.Course = course.Read(1);
            subject.Save();
            var subjects = subject.ReadAll();
            var actual = subjects[subjects.Count - 1];
            actual.Delete();
            Assert.AreEqual("nameTest", actual.Name);
        }

        [TestMethod]
        public void SubjectUpdate()
        {
            var subject = new ENSubject().Read(1);
            var oldName = subject.Name;
            subject.Name = "testUpdateName";
            subject.Save();
            var actual = subject.Read(1);
            var actualName = subject.Name;
            actual.Name = oldName;
            actual.Save();
            Assert.AreEqual("testUpdateName", actualName);
        }

    }
}
