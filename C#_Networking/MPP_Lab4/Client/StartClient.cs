
using Networking;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    static class StartClient
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            IService server = new ServerProxy("127.0.0.1", 55555);
            AppController appController = new AppController(server);
            LoginController loginController = new LoginController(server);
            LoginWindow login = new LoginWindow(loginController);
            AppWindow mainWindow = new AppWindow(appController);
            appController.set(loginController, login, mainWindow);
            loginController.set(appController, login, mainWindow);
            Application.Run(login);
        }
    }
}
