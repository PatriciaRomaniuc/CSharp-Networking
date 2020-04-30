using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking
{
    [Serializable]
    class findExcursieRequest : Request
    {
        private int id;
        public findExcursieRequest(int idE)
        {
            id = idE;
        }
        public virtual int getIdExc()
        {
            return id;
        }

    }
}
