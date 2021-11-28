using CsvHelper;
using Cvjecara;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;

namespace TestCvjecara
{
    [TestClass]
    public class UnitTestsCvijet
    {
        static IEnumerable<object[]> CvijetNeisravniCSV
        {
            get
            {
                return UčitajPodatkeCSV();
            }
        }
        static IEnumerable<object[]> CvijetIspravniXML
        {
            get
            {
                return UčitajPodatkeXML();
            }
        }
        Cvijet c1, c2, c3, c4, c5, c6;
        //Uradila Nedina Muratović
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
        #region TestKonstruktor
        [TestMethod]
        [DynamicData("CvijetNeisravniCSV")]
        [ExpectedException(typeof(FormatException))]
        public void TestKonstrukoraIzuzetak(Vrsta vrsta, string ime, string boja, DateTime datumBranja, int kol)
        {
            Cvijet cvijet = new Cvijet(vrsta, ime, boja, datumBranja, kol);
        }
        [TestMethod]
        [DynamicData("CvijetIspravniXML")]
        public void TestKonstrukora(Vrsta vrsta, string ime, string boja, DateTime datumBranja, int kol)
        {
            Cvijet cvijet = new Cvijet(vrsta, ime, boja, datumBranja, kol);
            Assert.AreEqual(cvijet.Vrsta, vrsta);
            StringAssert.Equals(cvijet.LatinskoIme, ime);
            StringAssert.Equals(cvijet.Boja, boja);
            StringAssert.Equals(cvijet.DatumBranja, datumBranja);
            Assert.AreEqual(cvijet.Kolicina, kol);
        }
        #endregion

        #region TestMetode

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestIzuzetkaPonovnoPostavljanjeLatinskogImena()
        {
            c1.LatinskoIme = "Rosa";
        }
        //Radila Nedina Muratovic
        [TestMethod]
        public void TestNajboljaSvježinaCvijeća()
        {
            Assert.AreEqual(c1.OdrediSvježinuCvijeća(), 5.0);

        }
        //Uradila Nedina Muratović
        [TestMethod]
        public void TestSmanjeneSvjezineOrhideja()
        {
            Assert.AreEqual(c2.OdrediSvježinuCvijeća(), 4.5);
        }
        //Uradila Nedina Muratović
        [TestMethod]
        public void TestSmanjeneSvjezineLjiljan()
        {
            Assert.AreEqual(c3.OdrediSvježinuCvijeća(), 2.6);
        }
        //Uradila Nedina Muratović
        [TestMethod]
        public void TestSmanjeneSvjezineMargarete()
        {
            Assert.AreEqual(c4.OdrediSvježinuCvijeća(), 3.4);
        }
        //Uradila Nedina Muratovic
        [TestMethod]
        public void TestSmanjeneSvjezineRuza()
        {
            Assert.AreEqual(c6.OdrediSvježinuCvijeća(), 3.4);
        }
        //uradila Nedina Muratovic
        [TestMethod]
        public void TestSmanjeneSvjezineNevena()
        {
            Assert.AreEqual(c5.OdrediSvježinuCvijeća(), 4.4);
        }
        //uradila Nedina Muratovic
        [TestMethod]
        public void TestSvjezinaNula()
        {
            c3.DatumBranja = DateTime.Now.AddDays(-7);
            Assert.AreEqual(c3.OdrediSvježinuCvijeća(), 0.0);
        }
        //uradila Azra Ahmić(18390)
        [TestMethod]
        public void TestProvjeriSezonskoJesteSzonsko1()
        {
            c5.ProvjeriKrajSezone();
            Assert.IsTrue(c5.Sezonsko);
            Assert.AreEqual(c5.Kolicina, 0);
        }
        //uradila Azra Ahmić(18390)
        [TestMethod]
        public void TestProvjeriSezonskoJesteSzonsko2()
        {
            c5.DatumBranja.AddMonths(-10);
            c5.ProvjeriKrajSezone();
            Assert.IsTrue(c5.Sezonsko);
            Assert.AreEqual(c5.Kolicina, 0);
        }
        //uradila Azra Ahmić(18390)
        [TestMethod]
        public void TestProvjeriSezonskoNijeSezonsko()
        {
            c1.ProvjeriKrajSezone();
            Assert.IsFalse(c1.Sezonsko);
            Assert.AreEqual(c1.Kolicina, 1);
        }
        #endregion

        #region Pomoćne metode
        public static IEnumerable<object[]> UčitajPodatkeCSV()
        {
            using (var reader = new StreamReader("CvijetNeispravni.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var rows = csv.GetRecords<dynamic>();
                foreach (var row in rows)
                {
                    var values = ((IDictionary<String, Object>)row).Values;
                    var elements = values.Select(elem => elem.ToString()).ToList();
                    yield return new object[] {(Vrsta)Enum.Parse(typeof(Vrsta), elements[0], true), elements[1],
                    elements[2],DateTime.Parse(elements[3]),Int32.Parse(elements[4])};
                }
            }
        }
        public static IEnumerable<object[]> UčitajPodatkeXML()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("CvijetIspravni.xml");
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                List<string> elements = new List<string>();
                foreach (XmlNode innerNode in node)
                {
                    elements.Add(innerNode.InnerText);
                }
                yield return new object[] {(Vrsta)Enum.Parse(typeof(Vrsta), elements[0], true), elements[1],
                    elements[2],DateTime.Parse(elements[3]),Int32.Parse(elements[4]) };
            }
        }

        #endregion
    }
}

