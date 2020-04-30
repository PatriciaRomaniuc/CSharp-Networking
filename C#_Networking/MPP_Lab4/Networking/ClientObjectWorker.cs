using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Services;
using Model;
using System.Collections.Generic;
using Persistence;

namespace Networking
{
    public class ClientWorker : IObserver
    {
        private IService server;
        private TcpClient connection;

        private NetworkStream stream;
        private IFormatter formatter;
        private volatile bool connected;
        public ClientWorker(IService server, TcpClient connection)
        {
            this.server = server;
            this.connection = connection;
            try
            {

                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                connected = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public virtual void run()
        {
            while (connected)
            {
                try
                {
                    object request = formatter.Deserialize(stream);
                    object response = handleRequest((Request)request);
                    if (response != null)
                    {
                        sendResponse((Response)response);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }

                try
                {
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
            try
            {
                stream.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
            }
        }

        public void updateTrips(List<Excursie> excursies)
        {
            try
            {
                sendResponse(new updateGetAllExcursii(excursies));
            }
            catch (PersistenceException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private Response handleRequest(Request request)
        {
            Response response = null;
            if (request is LoginRequest)
            {
                Console.WriteLine("Login request ...");
                LoginRequest logReq = (LoginRequest)request;
                User user = logReq.User;
                try
                {
                    lock (server)
                    {
                        server.login(user, this);
                    }
                    return new OkResponse();
                }
                catch (PersistenceException e)
                {
                    connected = false;
                    return new ErrorResponse(e.Message);
                }
            }
            if (request is LogoutRequest)
            {
                Console.WriteLine("Logout request");
                LogoutRequest logReq = (LogoutRequest)request;
                User user = logReq.User;
                try
                {
                    lock (server)
                    {

                        server.logout(user);
                    }
                    connected = false;
                    return new OkResponse();

                }
                catch (PersistenceException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }
            if (request is getExcursiiRequest){
                Console.WriteLine("Get trips request");
                try
                {
                    List<Excursie> list;
                    lock (server)
                    {
                        list = server.findAllExcursii();
                    }
                    return new getExcursiiResponse(list);

                }
                catch (PersistenceException exc)
                {
                    return new ErrorResponse(exc.Message);
                }
            }
            if (request is getExcursiiFilteredRequest){
                getExcursiiFilteredRequest request1 = (getExcursiiFilteredRequest)request;
                try
                {
                    List<Excursie> all;
                    lock (server)
                    {
                        all = server.getExcursiiTableFiltru(request1.getObiectiv(), request1.getDupaOra().ToString(), request1.getInainteDe().ToString());
                    }
                    return new getExcursiiFilteredResponse(all);
                }
                catch (PersistenceException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }
            if (request is findExcursieRequest){
                try
                {
                    Excursie exc;
                    lock (server)
                    {
                         exc = server.findOneExcursie(((findExcursieRequest)request).getIdExc());
                    }
                    return new findExcursieResponse(exc);
                }
                catch (PersistenceException exc)
                {
                    return new ErrorResponse(exc.Message);
                }
            }
            if (request is updateExcursieRequest)
        {
                try
                {
                    updateExcursieRequest request1 = (updateExcursieRequest)request;

                    lock (server)
                    {
                        server.updateExcursie(request1.getExcursie1());
                    }
                    return new OkResponse();
                }
                catch (PersistenceException exc)
                {
                    return new ErrorResponse(exc.Message);
                }
            }
            if (request is addRezervareRequest){
                try
                {
                    lock (server)
                    {
                        server.addRezervare(((addRezervareRequest)request).getRezervare());
                    }
                    return new OkResponse();
                }
                catch (PersistenceException exc)
                {
                    return new ErrorResponse(exc.Message);
                }
            }
            return null;
        }
        private void sendResponse(Response response)
        {
            Console.WriteLine("sending response " + response);
            formatter.Serialize(stream, response);
            stream.Flush();

        }
    }
}
