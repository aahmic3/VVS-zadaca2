using System;
using Cvjecara;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using System.Globalization;
using System.Linq;
using System.Xml;

namespace TestCvjecara
{
    [TestClass]
    public class TestMušterija {
        static IEnumerable<object[]> PoklonNeispravniCSV
        {
            get
            {
                return UčitajPodatkeCSV();
            }
        }
        static IEnumerable<object[]> PoklonIspravniXML
        {
            get
            {
                return UčitajPodatkeXML();
            }
        }

        Mušterija m1, m2;
        Buket b1;
        private Poklon p1, p2, p3;
        [TestInitialize]
        public void InicijalizacijaPrijeSvakogTesta()
        {
            m1 = new Mušterija("Mini Maus");
            m2 = new Mušterija("Miki Maus");
            b1 = new Buket(55.0);
            p1 = new Poklon("rođendan", 0.1);
            p2 = new Poklon("diplomski", 0.2);
            p3 = new Poklon("godišnjica", 0.3);
        }
        #region TestGeteriMušterija
        //Radila Dženeta Ahmić (18482)
        [TestMethod]
        public void TestGetUkupranBrojKupovina()
        {
            m1.RegistrujKupovinu(b1, p1);
            m1.RegistrujKupovinu(b1, p2);
            Assert.AreEqual(m1.UkupanBrojKupovina, 2);
        }
        [TestMethod]
        public void TestGetKupljeniPokloni()
        {
            m1.RegistrujKupovinu(b1, p1);
            m1.RegistrujKupovinu(b1, p2);
            m1.RegistrujKupovinu(b1, p3);
            List<Poklon> pokloni = new List<Poklon> { p1, p2, p3 };
            Assert.AreEqual(m1.KupljeniPokloni.Count, pokloni.Count);
        }
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestPraznoImeMušterije()
        {
            m1.ImeIPrezime = "";
        }
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestImeWhiteSpaceMušterije()
        {
            m1.ImeIPrezime = "   ";
        }
        [TestMethod]
        public void TestGetIme()
        {
            Assert.AreEqual(m2.ImeIPrezime, "Miki Maus");
        }
        #endregion

        #region TestMetodeMušterija
        //Radila Dženeta Ahmić (18482)
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestNullBuketIliPoklon()
        {
            m1.RegistrujKupovinu(null,null);
        }
            [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestNagradnaKupovinaNijeOstvarenaPoBrojuKupovina(){
            m1.NagradnaKupovina(p1);

        }
        
        [TestMethod]
        public void TestNagradnaKupovinaOstvarena()
        {
            for(int i=0; i<100; i++)
            {
                m2.RegistrujKupovinu(b1, p1);
            }
            Assert.IsTrue(m2.NagradnaKupovina(p1));
        }
        //Uradila Nedina Muratović
        [TestMethod]
        public void TestNagradnaKupovinaOstvarenaZaVelikiBroj()
        {
            for (int i = 0; i < 10000; i++)
            {
                m2.RegistrujKupovinu(b1, p1);
            }
            Assert.IsTrue(m2.NagradnaKupovina(p1));
        }
        #endregion

        #region TestKonstruktorPoklon
        //uradila Azra Ahmić(18390)
        [TestMethod]
        [DynamicData("PoklonNeispravniCSV")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestKonstrukoraIzuzetak(String opis, double postotak)
        {
           Poklon poklon = new Poklon(opis,postotak);
        }
        //uradila Azra Ahmić(18390)
        [TestMethod]
        [DynamicData("PoklonIspravniXML")]
        public void TestKonstrukora(String opis, double postotak)
        {
            Poklon poklon = new Poklon(opis, postotak);
            StringAssert.Equals(poklon.Opis, opis);
            Assert.AreEqual(poklon.PostotakPopusta, postotak);
        }
        #endregion

        #region TestMetodaPoklon
        //uradila Azra Ahmić(18390)
        [TestMethod]
        public void TestGeterŠifraPoklona()
        {
            StringAssert.Equals(p1.Šifra, "10000");
        }
        #endregion

        #region PomoćneMetode

        public static IEnumerable<object[]> UčitajPodatkeCSV()
        {
            using (var reader = new StreamReader("PoklonNeispravni.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var rows = csv.GetRecords<dynamic>();
                foreach (var row in rows)
                {
                    var values = ((IDictionary<String, Object>)row).Values;
                    var elements = values.Select(elem => elem.ToString()).ToList();
                    yield return new object[] {elements[0], double.Parse(elements[1])};
                }
            }
        }
        public static IEnumerable<object[]> UčitajPodatkeXML()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("PoklonIspravni.xml");
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                List<string> elements = new List<string>();
                foreach (XmlNode innerNode in node)
                {
                    elements.Add(innerNode.InnerText);
                }
                yield return new object[] {elements[0], double.Parse(elements[1]) };
            }
        }
        #endregion
    }
}
