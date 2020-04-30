using Model;
using Persistence;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
   
        public class LoginController
        {
            private readonly IService service;
            private AppController appController;
            private LoginWindow loginWindow;
            private AppWindow appWindow;
        private User user;
            public LoginController(IService service)
            {
                this.service = service;
            }

            public void set(AppController appController, LoginWindow loginWindow, AppWindow appWindow)
            {
                this.appController = appController;
                this.loginWindow = loginWindow;
                this.appWindow = appWindow;
            }


        public void login(string username, string password)
        {
            try
            {
                user = new User(username, password);
                service.login(user, appController);

                loginWindow.Hide();
                appWindow.ShowDialog();
                loginWindow.Close();
            }
            catch (PersistenceException ex)
            {
                MessageBox.Show("Date gresite! Username sau parola incorecte!");
            }
        }

            public void clear()
            {
                loginWindow.clear();
            }

            public User getUser()
            {
            return user;
            }


        }
    }
