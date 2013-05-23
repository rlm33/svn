using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClasesAlicanTeam.EN;

namespace TestCAD
{
    [TestClass]
    public class ENDistributorsOrderTest
    {
        [TestMethod]
        public void DistributorsOrderCreateWithoutLines()
        {
            var order = new ENDistributorsOrder();
            var orderList = order.ReadAll();
            var oldCount = orderList.Count;
            var now = DateTime.Now;
            order.DataOrder = now;
            order.Distributor = 1;
            order.Save();
            orderList = order.ReadAll();
            order.Delete();
            Assert.IsTrue(oldCount < orderList.Count);
            var actual = orderList[orderList.Count - 1];
            Assert.IsTrue(actual.Id > 0);
            Assert.AreEqual(now, actual.DataOrder); //Por lo visto no guarda bien la hora, pero sí la fecha
        }

        [TestMethod]
        public void DistributorsOrderReadNullProblemFixed()
        {
            var order = (new ENDistributorsOrder()).Read(7);
            Assert.AreNotEqual(null, order);
        }
    }
}
