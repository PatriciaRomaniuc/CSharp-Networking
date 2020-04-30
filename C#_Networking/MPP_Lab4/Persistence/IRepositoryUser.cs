using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace Persistence
{
    public interface IRepositoryUser
    {
        void save(User user );

        void delete(string username);

        void update(User user);

        User findOne(string username);
        List<User> findAll();

    }
}
