using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking
{
    [Serializable]
    class getExcursiiFilteredRequest:Request
    {
        String obiectiv;
        String dupaOra;
        String inainteDe;
        public getExcursiiFilteredRequest(String o, String d, String i)
        {
            obiectiv = o;
            dupaOra = d;
            inainteDe = i;
        }

        public virtual String getObiectiv()
        {
            return obiectiv;
        }

        public virtual String getDupaOra()
        {
            return dupaOra;
        }

        public virtual String getInainteDe()
        {
            return inainteDe;
        }
    }
}
