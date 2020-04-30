using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IService
    {
        List<Excursie> findAllExcursii();
        List<Excursie> getExcursiiTableFiltru(String obiectiv, String dupa, String inainte);
        Excursie findOneExcursie(int id);
        void updateExcursie(Excursie excursie);
        void addRezervare(Rezervare rezervare);
        void login(User user, IObserver observer);
        void logout(User user);


    }
}
