using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClasesAlicanTeam.EN;

namespace TestCAD
{
    [TestClass]
    public class ENDistributorTest
    {
        [TestMethod]
        public void DistributorZeroIDProblemFixed()
        {
            var distributorList = (new ENDistributor()).ReadAll();
            foreach (var distributor in distributorList)
            {
                Assert.AreNotEqual(0, distributor.Id);
            }
        }
    }
}
