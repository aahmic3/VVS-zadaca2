
using Cvjecara;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TestCvjecara
{
    [TestClass]
    public class UnitTestsBuket
    {
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestCijenaIzuzetak()
        {
            Buket buket = new Buket(0.0);
        }

        [TestMethod]
        public void TestDodavanjeIspravnihDodatka()
        {
            Buket buket = new Buket(10.0);
            buket.DodajDodatak("Lišće");
            buket.DodajDodatak("Lišće");
            Assert.AreEqual(buket.Dodaci.Count, 2);
            CollectionAssert.AreEqual(buket.Dodaci, new List<String> { "Lišće", "Lišće" });
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestDodavanjeNeispravnogDodatka()
        {
            Buket buket = new Buket(10.0);
            buket.DodajDodatak("List");
        }
        [TestMethod]
        public void TestDodavanjePoklona()
        {
            Buket buket = new Buket(10.0);
            Poklon p = new Poklon("Rođendanski", 0.1);
            buket.DodajPoklon(p);
            Assert.AreEqual(p, buket.Poklon);
        }
    }
}
