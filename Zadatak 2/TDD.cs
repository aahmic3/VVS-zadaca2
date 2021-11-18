using Cvjecara;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Zadatak_2
{
    [TestClass]
    public class TDD
    {
        #region Zamjenski objekat

        [TestMethod]
        public void TestZamjenskiObjekat()
        {
            Cvijet c1 = new Cvijet(Vrsta.Ruža, "Rosa rubiginosa", "Crvena", DateTime.Now.AddDays(-2), 2);
            Cvijet c2 = new Cvijet(Vrsta.Neven, "Ne znam latinsko ime", "Žuta", DateTime.Now.AddDays(-1), 5);
            Cvjećara c = new Cvjećara();
            c.RadSaCvijećem(c1, 0);
            c.RadSaCvijećem(c2, 0);
            
            Leksikon leksikon = new Leksikon();
            c.ProvjeriLatinskaImenaCvijeća(leksikon);

            Assert.AreEqual(c.Cvijeće.Count, 1);
            Assert.IsTrue(c.Cvijeće.Contains(c1));
            Assert.IsFalse(c.Cvijeće.Contains(c2));
        }

        #endregion

        #region TDD

        [TestMethod]
        public void ProljetnaNabavka()
        {
            Cvijet cvijet = new Cvijet(Vrsta.Ljiljan, "Lilium bosniacum", "Žuta", DateTime.Now.AddDays(-1), 10);
            Cvjećara c = new Cvjećara();
            c.RadSaCvijećem(cvijet, 0);
            List<Cvijet> cvijeće = new List<Cvijet>();
            for (int i = 0; i < 5; i++)
                cvijeće.Add(cvijet);
            c.DodajBuket(cvijeće, new List<string>() { "Lišće" }, null, 20.0);
            Assert.AreEqual(c.Cvijeće.Count, 1);
            Assert.IsTrue(c.DajSveBukete().FindAll(b => b.Cijena == 20.0).Count == 1);
            
            c.IzvršiNabavku("Proljeće", "Mala");
            
            Assert.IsTrue(c.Cvijeće.Contains(cvijet));
            Cvijet ljiljan = c.Cvijeće.Find(cv => cv.Vrsta == Vrsta.Ljiljan);
            Assert.AreEqual(ljiljan.Kolicina, 10);
            Cvijet neven = c.Cvijeće.Find(cv => cv.Vrsta == Vrsta.Neven);
            Assert.AreEqual(neven.Kolicina, 10);
            Cvijet margareta = c.Cvijeće.Find(cv => cv.Vrsta == Vrsta.Margareta);
            Assert.AreEqual(margareta.Kolicina, 10);
            Assert.IsFalse(c.Cvijeće.FindAll(cv => cv.Vrsta == Vrsta.Ruža).Count > 0);
            Assert.IsTrue(c.DajSveBukete().FindAll(b => b.Cijena == 20.0).Count == 1);
        }

        [TestMethod]
        public void JesenjaNabavka()
        {
            Cvijet cvijet = new Cvijet(Vrsta.Ljiljan, "Lilium bosniacum", "Žuta", DateTime.Now.AddDays(-1), 10);
            Cvjećara c = new Cvjećara();
            c.RadSaCvijećem(cvijet, 0);
            List<Cvijet> cvijeće = new List<Cvijet>();
            for (int i = 0; i < 5; i++)
                cvijeće.Add(cvijet);
            c.DodajBuket(cvijeće, new List<string>() { "Lišće" }, null, 20.0);
            Assert.AreEqual(c.Cvijeće.Count, 1);
            Assert.IsTrue(c.DajSveBukete().FindAll(b => b.Cijena == 20.0).Count == 1);
            
            c.IzvršiNabavku("Jesen", "Velika");
            
            Assert.IsFalse(c.Cvijeće.Contains(cvijet));
            Cvijet ruža = c.Cvijeće.Find(cv => cv.Vrsta == Vrsta.Ruža);
            Assert.AreEqual(ruža.Kolicina, 100);
            Cvijet orhideja = c.Cvijeće.Find(cv => cv.Vrsta == Vrsta.Orhideja);
            Assert.AreEqual(orhideja.Kolicina, 100);
            Assert.IsFalse(c.Cvijeće.FindAll(cv => cv.Vrsta == Vrsta.Ljiljan).Count > 0);
            Assert.IsFalse(c.DajSveBukete().FindAll(b => b.Cijena == 20.0).Count == 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NedozvoljenaNabavkaLjetiIliZimi()
        {
            Cvijet cvijet = new Cvijet(Vrsta.Ljiljan, "Lilium bosniacum", "Žuta", DateTime.Now.AddDays(-1), 10);
            Cvjećara c = new Cvjećara();
            c.RadSaCvijećem(cvijet, 0);
            List<Cvijet> cvijeće = new List<Cvijet>();
            for (int i = 0; i < 5; i++)
                cvijeće.Add(cvijet);
            c.DodajBuket(cvijeće, new List<string>() { "Lišće" }, null, 20.0);
            Assert.AreEqual(c.Cvijeće.Count, 1);
            Assert.IsTrue(c.DajSveBukete().FindAll(b => b.Cijena == 20.0).Count == 1);

            c.IzvršiNabavku("Ljeto", "Mala");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NedozvoljeneSrednjeNabavke()
        {
            Cvijet cvijet = new Cvijet(Vrsta.Ljiljan, "Lilium bosniacum", "Žuta", DateTime.Now.AddDays(-1), 10);
            Cvjećara c = new Cvjećara();
            c.RadSaCvijećem(cvijet, 0);
            List<Cvijet> cvijeće = new List<Cvijet>();
            for (int i = 0; i < 5; i++)
                cvijeće.Add(cvijet);
            c.DodajBuket(cvijeće, new List<string>() { "Lišće" }, null, 20.0);
            Assert.AreEqual(c.Cvijeće.Count, 1);
            Assert.IsTrue(c.DajSveBukete().FindAll(b => b.Cijena == 20.0).Count == 1);

            c.IzvršiNabavku("Proljeće", "Srednja");
        }

        #endregion
    }

}
