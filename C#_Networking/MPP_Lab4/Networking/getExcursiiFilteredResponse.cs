using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking
{
    [Serializable]
    class getExcursiiFilteredResponse:Response
    {
        private List<Excursie> list;

        public virtual List<Excursie> getList()
        {
            return list;
        }

        public  getExcursiiFilteredResponse(List<Excursie> l)
        {
            list = l;
        }
    }
}
