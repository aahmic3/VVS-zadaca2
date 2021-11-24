using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvjecara
{
    public interface ILeksikon
    {
        public bool ValidnoLatinskoIme(string ime);
    }

    public class Leksikon : ILeksikon
    {
        public bool ValidnoLatinskoIme(string ime)
        {
            throw new NotImplementedException();
        }
    }
        public class Spy : ILeksikon
        {
            public bool ValidnoLatinskoIme(string ime)
            {
                List<String> latinskaImena = new List<string> { "Rosa rubiginosa", "Lilum", "Calendula officinalis", "Orchidaceae", "Chrysanthemum" };
                if (latinskaImena.Contains(ime))
                    return true;
                return false;
            }
        }
    }
