using Cvjecara;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
namespace TestCvjecara
{
    [TestClass]
    public class UnitTestsCvijet
    {
        Cvijet c1, c2, c3, c4, c5, c6;

        [TestInitialize]
        public void InicijalizacijaPrijeSvakogTesta()
        {
            c1 = new Cvijet(Vrsta.Ruža, "Majska", "Crvena", DateTime.Now.AddDays(-2), 1);
            c2 = new Cvijet(Vrsta.Orhideja, "Orhideja", "Bijela", DateTime.Now.AddDays(-3), 2);
            c3 = new Cvijet(Vrsta.Ljiljan, "Bosanski ljiljan", "Žuta", DateTime.Now.AddDays(-5), 1);
            c4 = new Cvijet(Vrsta.Margareta, "Ivančica", "Bijela", DateTime.Now.AddDays(-5), 4);
            c5 = new Cvijet(Vrsta.Neven, "Žutelj", "Žuta", DateTime.Now.AddDays(-4), 7);
            c6 = new Cvijet(Vrsta.Ruža, "Majska", "Crvena", DateTime.Now.AddDays(-6), 1);

        }
        [TestMethod]
        public void TestNajboljaSvježinaCvijeća()
        {
            Assert.AreEqual(c1.OdrediSvježinuCvijeća(), 5.0);

        }
        [TestMethod]
        public void TestSmanjeneSvjezineOrhideja()
        {
            Assert.AreEqual(c2.OdrediSvježinuCvijeća(), 4.5);
        }

        [TestMethod]
        public void TestSmanjeneSvjezineLjiljan()
        {
            Assert.AreEqual(c3.OdrediSvježinuCvijeća(), 2.6);
        }
        [TestMethod]
        public void TestSmanjeneSvjezineMargarete()
        {
            Assert.AreEqual(c4.OdrediSvježinuCvijeća(), 3.4);
        }
        [TestMethod]
        public void TestSmanjeneSvjezineRuza()
        {
            Assert.AreEqual(c6.OdrediSvježinuCvijeća(), 3.4);
        }
        [TestMethod]
        public void TestSmanjeneSvjezineNevena()
        {
            Assert.AreEqual(c5.OdrediSvježinuCvijeća(), 4.4);
        }
        [TestMethod]
        public void TestSvjezinaNula()
        {
            c3.DatumBranja = DateTime.Now.AddDays(-7);
            Assert.AreEqual(c3.OdrediSvježinuCvijeća(), 0.0);
        }

    }
}
