using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking
{
    [Serializable]

    public class addRezervareRequest : Request
    {
        private Rezervare rezervare;
        public addRezervareRequest(Rezervare rezervare)
        {
            this.rezervare = rezervare;
        }
        public virtual Rezervare getRezervare()
        {
            return rezervare;
        }
    }
}
