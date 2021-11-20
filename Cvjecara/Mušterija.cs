using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvjecara
{
    public class Mušterija
    {
        #region Atributi

        string identifikacijskiBroj, imeIPrezime;
        int ukupanBrojKupovina;
        List<Poklon> kupljeniPokloni;
        List<Buket> kupljeniBuketi;

        #endregion

        #region Properties

        public string IdentifikacijskiBroj { get => identifikacijskiBroj; }
        public string ImeIPrezime 
        { 
            get => imeIPrezime;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new NotSupportedException("Ime i prezime mušterije se mora navesti!");
                imeIPrezime = value;
            }
        }
        public int UkupanBrojKupovina { get => ukupanBrojKupovina; }
        public List<Buket> KupljeniBuketi { get => kupljeniBuketi; }
        public List<Poklon> KupljeniPokloni { get => kupljeniPokloni; }

        #endregion

        #region Konstruktor

        public Mušterija(string ime)
        {
            string sifra = "";
            Random r = new Random();
            for (int i = 0; i < 10; i++)
                sifra += r.Next(0, 9).ToString();
            identifikacijskiBroj = sifra;
            ImeIPrezime = ime;
            ukupanBrojKupovina = 0;
            kupljeniBuketi = new List<Buket>();
            kupljeniPokloni = new List<Poklon>();
        }

        #endregion

        #region Metode

        public void RegistrujKupovinu(Buket b, Poklon p)
        {
            if (b == null || p == null)
                throw new NotSupportedException("Buket i poklon se moraju navesti!");

            ukupanBrojKupovina++;
            kupljeniBuketi.Add(b);
            kupljeniPokloni.Add(p);
        }

        /// <summary>
        ///5
        /// dodaje mu se nagradni poklon koji je poslan kao parametar,
        /// ali samo pod uslovom da poklon ispunjava kriterij da njegov postotak popusta
        /// odgovara broju kupovina (za 100 kupovina maksimalni popust je 10%, za 1,000
        /// kupovina maksimalni popust je 20%, za 10,000 kupovina je 30% i sl.)
        /// Ukoliko mušterija nije napravila tačan broj kupovina koji se zahtijeva
        /// ili je proslijeđen poklon koji ne ispunjava parametar, potrebno je baciti izuzetak.
        /// </summary>
        /// <param name="nagrada"></param>
        /// <returns></returns>
       
        //Uradila Dženeta Ahmić (18482)
        public bool NagradnaKupovina(Poklon nagrada)
        {
            int i, stepen = 10;
            for(i = 1; i <= 10; i++)
            {
                stepen *= 10;
                if(stepen == ukupanBrojKupovina)
                    break;
            }
            //samo ako je petlja prije prekinuta i uslov ispunjen
            if(i != 11 && i*10 >= nagrada.PostotakPopusta * 100)
            {
                kupljeniPokloni.Add(nagrada);
                return true;
            }
            throw new InvalidOperationException("Niste ostvarili pravo na nagradni poklon!");
        }

        #endregion
    }
}
