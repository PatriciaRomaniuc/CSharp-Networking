using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace Persistence
{
    public interface IRepositoryRezervare:ICrudRepository<int,Rezervare>
    {
         int size();
         //List<Rezervare> findAll();

    }
}
