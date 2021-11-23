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
    public class UnitTestsBuket
    {
        static IEnumerable<object[]> BuketNeispravniCSV
        {
            get
            {
                return UčitajPodatkeCSV();
            }
        }
        static IEnumerable<object[]> BuketIspravniXML
        {
            get
            {
                return UčitajPodatkeXML();
            }
        }
        #region TestKonstruktorBuket
        [TestMethod]
        [DynamicData("BuketNeispravniCSV")]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestKonstrukoraIzuzetak(double cijena)
        {
            Buket buket = new Buket(cijena);
        }
        [TestMethod]
        [DynamicData("BuketIspravniXML")]
        public void TestKonstrukora(double cijena)
        {
            Buket buket = new Buket(cijena);
            Assert.AreEqual(buket.Cijena, cijena);
        }
        #endregion

        #region TestMetodeBuket
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
        #endregion

        #region PomoćneMetode
        public static IEnumerable<object[]> UčitajPodatkeCSV()
        {
            using (var reader = new StreamReader("BuketNeispravni.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var rows = csv.GetRecords<dynamic>();
                foreach (var row in rows)
                {
                    var values = ((IDictionary<String, Object>)row).Values;
                    var elements = values.Select(elem => elem.ToString()).ToList();
                    yield return new object[] { double.Parse(elements[0]) };
                }
            }
        }
        public static IEnumerable<object[]> UčitajPodatkeXML()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("BuketIspravni.xml");
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                List<string> elements = new List<string>();
                foreach (XmlNode innerNode in node)
                {
                    elements.Add(innerNode.InnerText);
                }
                yield return new object[] { double.Parse(elements[0]) };
            }

        }

        #endregion
    }
}



