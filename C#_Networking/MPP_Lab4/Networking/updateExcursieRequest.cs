using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking
{
    [Serializable]
    class updateExcursieRequest:Request
    {
        private Excursie excursie;
        public updateExcursieRequest(Excursie e)
        {
            excursie = e;
        }


        public virtual Excursie getExcursie1()
        {
            return excursie;
        }

    }
}
