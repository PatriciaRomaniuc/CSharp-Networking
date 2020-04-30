using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence;
using Model;

namespace Server
{
    class Service : IService
    {
        private readonly IDictionary<String, IObserver> loggedClients;
        private IRepositoryRezervare repoRezervare;
        private IRepositoryUser repoUsers;
        private IRepositoryExcursie repoExcursie;

        public Service(IRepositoryRezervare repoRezervare, IRepositoryUser repoUsers, IRepositoryExcursie repoExcursie)
        {
            loggedClients = new Dictionary<String, IObserver>();
            this.repoRezervare = repoRezervare;
            this.repoUsers = repoUsers;
            this.repoExcursie = repoExcursie;

        }
        public void login(User user, IObserver client)
        {
            User userOk = repoUsers.findOne(user.Username);
            if (userOk != new User(null,null))
            {
                if (loggedClients.ContainsKey(user.Username))
                    throw new PersistenceException("User already logged in.");
                loggedClients[user.Username] = client;
            }
            else
                throw new PersistenceException("Authentication failed.");
        }
        public void logout(User user)
        {
            loggedClients.Remove(user.Username);
        }
        public Excursie findOneExcursie(int id)
        {
            return repoExcursie.findOne(id);
        }

        public List<Excursie> findAllExcursii()
        {
            return repoExcursie.findAll();
        }

        public List<Excursie> getExcursiiTableFiltru(String obiectiv, String dupa, String inainte)
        {
            return repoExcursie.findAllObiectivOra(obiectiv, dupa, inainte);
        }

        public void updateExcursie(Excursie excursie)
        {
            repoExcursie.update(excursie);
            foreach (IObserver observer in loggedClients.Values)
                observer.updateTrips(findAllExcursii());
        }

        public  void addRezervare(Rezervare rezervare)
        {
            repoRezervare.save(rezervare);
        }
    }
}
