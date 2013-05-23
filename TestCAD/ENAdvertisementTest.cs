using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClasesAlicanTeam.EN;
using System.Collections.Generic;

namespace TestCAD
{
    [TestClass]
    public class ENAdvertisementTest
    {
        [TestMethod]
        public void AdvertisementConnectingAndReadingAll()
        {
            var actual = new List<ENAdvertisement>();
            var advert = new ENAdvertisement();
            Assert.AreEqual(0, advert.Id);
            actual = advert.ReadAll();
        }

        [TestMethod]
        public void AdvertisementInsert()
        {
            var advertisement = new ENAdvertisement();
            advertisement.Customer = new ENCustomer().Read(1);
            advertisement.Description = "descripcionTest";
            advertisement.Picture = "";
            advertisement.Save();
            var advertisementBooks = advertisement.ReadAll();
            var actual = advertisementBooks[advertisementBooks.Count - 1];
            actual.Delete();
            Assert.AreEqual("descripcionTest", actual.Description);
        }

        [TestMethod]
        public void AdvertisementUpdate()
        {
            var advertisement = new ENAdvertisement();
            advertisement = advertisement.Read(1);
            var oldDescription = advertisement.Description;
            advertisement.Description = "testUpdateDescription";
            advertisement.Save();
            var actual = advertisement.Read(1);
            var actualDescription = advertisement.Description;
            actual.Description = oldDescription;
            actual.Save();
            Assert.AreEqual("testUpdateDescription", actualDescription);
        }
    }
}
