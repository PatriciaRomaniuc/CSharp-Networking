using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking
{
    [Serializable]
    class updateGetAllExcursii:Response
    {
        private List<Excursie> list;
        public updateGetAllExcursii(List<Excursie> l)
        {
            list = l;
        }
        public virtual List<Excursie> getList()
        {
            return list;
        }
    }

}
