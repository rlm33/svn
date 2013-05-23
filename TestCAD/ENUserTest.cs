using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClasesAlicanTeam.EN;
using System.Collections.Generic;

namespace TestCAD
{
    [TestClass]
    public class ENUserTest
    {
        [TestMethod]
        public void UserConnectingAndReadingAll()
        {
            var actual = new List<ENUser>();
            var user = new ENUser();
            actual = user.ReadAll();
        }
    }
}
