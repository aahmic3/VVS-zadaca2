using System;
using Cvjecara;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TestCvjecara
{
    [TestClass]
    public class TestMušterija { 
       
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

    }
}
