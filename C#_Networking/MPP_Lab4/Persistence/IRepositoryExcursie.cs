using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace Persistence
{
    public interface IRepositoryExcursie:ICrudRepository<int, Excursie>
    {
        int size();
        List<Excursie> findAll();
        List<Excursie> findAllObiectivOra(String obiectiv, String dupa, String inainte);

    }
}
