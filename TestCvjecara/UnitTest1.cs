
using Cvjecara;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TestCvjecara
{
    [TestClass]
    public class UnitTest1
    {
        Mušterija m1, m2, m3;
        Buket b1, b2, b3;
        Poklon p;
        List<Mušterija> musterije;
        [TestInitialize]
        public void InicijalizacijaPrijeSvakogTesta()
        {
            m1 = new Mušterija("Dženeta Ahmić");
            m2 = new Mušterija("Nedina Muratović");
            m3 = new Mušterija("Azra Ahmić");
            b1 = new Buket(55.0);
            b2 = new Buket(25.0);
            b3 = new Buket(30.0);
            b1.DodajCvijet(new Cvijet(Vrsta.Ruža, "majska", "Crvena", DateTime.Parse("03/11/2021"), 20));
            b2.DodajCvijet(new Cvijet(Vrsta.Orhideja, "Bosanski ljiljan", "Bijela", DateTime.Parse("03/11/2021"), 3));
            b3.DodajCvijet(new Cvijet(Vrsta.Orhideja, "orhideja", "Žuta", DateTime.Parse("10/11/2021"), 6));
            p = new Poklon("rođendan", 0.1);
            musterije = new List<Mušterija> { m1, m2, m3 };
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestNajboljaMušterijaIzuzetakBezMušterija(){
            Cvjećara cvjećara = new Cvjećara();
            cvjećara.DajNajboljuMušteriju();
        }

        [TestMethod]
        public void TestNajboljaMušterijaPoBrojuCvijeća()
        {
            Cvjećara cvjećara = new Cvjećara();
            m1.RegistrujKupovinu(b1, p);
            m1.RegistrujKupovinu(b2, p);
            m2.RegistrujKupovinu(b2, p);
            m3.RegistrujKupovinu(b3, p);
            cvjećara.Mušterije = musterije;
            Assert.AreEqual(cvjećara.DajNajboljuMušteriju().IdentifikacijskiBroj, m1.IdentifikacijskiBroj);
        }
        [TestMethod]
        public void TestNajboljaMušterijaPoCijeni()
        {
            Cvjećara cvjećara = new Cvjećara();
            m1.RegistrujKupovinu(b2, p);
            m1.RegistrujKupovinu(b2, p);
            m2.RegistrujKupovinu(b3, p);
            m3.RegistrujKupovinu(b2, p);
            cvjećara.Mušterije = musterije;
            Assert.AreEqual(cvjećara.DajNajboljuMušteriju().IdentifikacijskiBroj, m1.IdentifikacijskiBroj);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestNajboljaMušterijaIzuzetakZbogJednakosti()
        {
            Cvjećara cvjećara = new Cvjećara();
            m1.RegistrujKupovinu(b1, p);
            m2.RegistrujKupovinu(b1, p);
            m3.RegistrujKupovinu(b2, p);
            cvjećara.Mušterije = musterije;
            cvjećara.DajNajboljuMušteriju();
        }
    }
}
