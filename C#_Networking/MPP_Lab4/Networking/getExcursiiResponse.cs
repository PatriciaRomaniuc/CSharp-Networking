using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking
{
    [Serializable]

    class getExcursiiResponse:Response
    {
        private List<Excursie> list;
        public getExcursiiResponse(List<Excursie> l)
        {
            this.list = l;
        }
        public virtual List<Excursie> getList()
        {
            return list;
        }
    }
}
