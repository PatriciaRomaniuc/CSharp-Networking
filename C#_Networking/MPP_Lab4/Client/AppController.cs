using Model;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class AppController: IObserver
    {
        private readonly IService service;
        private LoginController loginController;
        private LoginWindow loginWindow;
        private AppWindow appWindow;

        public AppController(IService service)
        {
            this.service = service;
        }
        public void set(LoginController loginController, LoginWindow loginWindow, AppWindow appWindow)
        {
            this.loginController = loginController;
            this.loginWindow = loginWindow;
            this.appWindow = appWindow;
        }

        public void updateTrips(List<Excursie> excursies)
        {
            UpdateTabelEventArgs args = new UpdateTabelEventArgs(UpdateTabelEvent.UpdateTabel, excursies);
            appWindow.updateModificare(this, args);
        }
        public List<Excursie> getAllExcursii()
        {
            return service.findAllExcursii();
        }

        public List<Excursie> cautaExcursii(string obiectiv, string dupa, string inainte)
        {
            return service.getExcursiiTableFiltru(obiectiv, dupa, inainte);


        }
        public void addRezervare(string nume, string telefon, int locuri, int idEx)
        {
            int id = service.findAllExcursii().Capacity + 1;
            service.addRezervare(new Rezervare(id, nume, telefon, locuri, idEx));
            Excursie ex = service.findOneExcursie(idEx);
            ex.NrLocuriDisponibile = ex.NrLocuriDisponibile - locuri;
            service.updateExcursie(ex);
        }

        public void logout()
        {
            service.logout(loginController.getUser());
            appWindow.Close();
        }
    }
}
