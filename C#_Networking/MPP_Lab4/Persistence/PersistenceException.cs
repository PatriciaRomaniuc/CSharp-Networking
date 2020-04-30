using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
   public class PersistenceException : Exception
    {
        public PersistenceException(String mesaj) : base(mesaj){}
        

    }
}
