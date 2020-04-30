using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking
{
    [Serializable]
    class findExcursieResponse: Response
    {
        private Excursie excursie;
        public findExcursieResponse(Excursie exc)
        {
            excursie = exc;
        }
        public virtual Excursie getExcursie1()
        {
            return excursie;
        }
    }
}
