using System;
using System.Net.Sockets;

using System.Threading;

using log4net.Config;
namespace Server
{
    using Networking;
    using Networking.serverUtils;
    using Persistence;
    using Services;

    public class StartServer
    {
        public static void Main(string[] args)
        {
            XmlConfigurator.Configure(new System.IO.FileInfo("App.config"));
            IRepositoryExcursie repositoryExcursie = new RepositoryExcursie();
            IRepositoryRezervare repositoryRezervare = new RepositoryRezervare();
            IRepositoryUser repositoryUser = new RepositoryUser();
            IService serviceImpl = new Service(repositoryRezervare,repositoryUser, repositoryExcursie);


            // IChatServer serviceImpl = new ChatServerImpl();
            AbstractServer server = new SerialServer("127.0.0.1", 55555, serviceImpl);
            server.Start();
            Console.WriteLine("Server started ...");
            //Console.WriteLine("Press <enter> to exit...");
            Console.ReadLine(); 
           
        }
    }

    public class SerialServer: ConcurrentServer 
    {
        private IService server;
        private ClientWorker worker;
        public SerialServer(string host, int port, IService server) : base(host, port)
            {
                this.server = server;
                Console.WriteLine("SerialServer...");
        }
        protected override Thread createWorker(TcpClient client)
        {
            worker = new ClientWorker(server, client);
            return new Thread(new ThreadStart(worker.run));
        }
    }
    
}
