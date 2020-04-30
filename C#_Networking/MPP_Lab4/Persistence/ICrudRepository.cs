using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public interface ICrudRepository<ID,E>
    {
        void save(E entity);

        void delete(ID id);

        void update(E entity);

        E findOne(ID id);
    }
}
