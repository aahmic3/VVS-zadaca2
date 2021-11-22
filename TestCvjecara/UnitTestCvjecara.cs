
using Cvjecara;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TestCvjecara
{
    [TestClass]
    public class UnitTestCvjecara
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
            b1.DodajCvijet(new Cvijet(Vrsta.Ruža, "majska", "Crvena", DateTime.Now.AddDays(-4), 20));
            b2.DodajCvijet(new Cvijet(Vrsta.Orhideja, "Bosanski ljiljan", "Bijela", DateTime.Now.AddDays(-7), 3));
            b3.DodajCvijet(new Cvijet(Vrsta.Orhideja, "orhideja", "Žuta", DateTime.Now.AddDays(-10), 6));
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

        [TestMethod]
        public void TestDodajBuket()
        {
            Cvjećara cvjećara = new Cvjećara();
            b1.DodajDodatak("Slama");
            b2.DodajDodatak("Lišće");
            cvjećara.DodajBuket(b1.Cvijeće, b1.Dodaci, b1.Poklon, b1.Cijena);
            cvjećara.DodajBuket(b2.Cvijeće, b2.Dodaci, b2.Poklon, b2.Cijena);
            List<Buket> buketi = cvjećara.DajSveBukete();
            List<Buket> buketi1=new List<Buket>() { b1, b2, b3};
            Assert.AreEqual(buketi.Count, 2);

        }
        [TestMethod]
        public void TestBrisanjaBuketa()
        {
            Cvjećara cvjećara = new Cvjećara();
            cvjećara.DodajBuket(b1.Cvijeće, b1.Dodaci, b1.Poklon, b1.Cijena);
            cvjećara.DodajBuket(b2.Cvijeće, b2.Dodaci, b2.Poklon, b2.Cijena);
            cvjećara.ObrišiBuket(cvjećara.DajSveBukete()[0]);
            List<Buket> buketi = cvjećara.DajSveBukete();
            Assert.AreEqual(buketi.Count, 1);
            CollectionAssert.AreEqual(buketi, cvjećara.DajSveBukete()); 

        }
        
        [TestMethod]
        public void TestPregledajCvijeće()
        {
            Cvjećara cvjećara = new Cvjećara();
            Cvijet c1 = new Cvijet(Vrsta.Orhideja, "Bosanski ljiljan", "Bijela", DateTime.Now.AddDays(-7), 3);
            cvjećara.RadSaCvijećem(c1, 0);
            cvjećara.PregledajCvijeće();
            CollectionAssert.DoesNotContain(cvjećara.Cvijeće, c1);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestRadSaCvijećemNepoznataOpcija()
        {
            Cvjećara cvjećara = new Cvjećara();
            Cvijet c1 = new Cvijet(Vrsta.Orhideja, "Bosanski ljiljan", "Bijela", DateTime.Now.AddDays(-7), 3);
            cvjećara.RadSaCvijećem(c1, 4);

        }
    }
}
