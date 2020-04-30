using System;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using Services;
using Model;
using Persistence;

namespace Networking
{
    ///
    /// <summary> * Created by IntelliJ IDEA.
    /// * User: grigo
    /// * Date: Mar 18, 2009
    /// * Time: 4:36:34 PM </summary>
    /// 
    public class ServerProxy : IService
	{
		private string host;
		private int port;

		private IObserver client;

		private NetworkStream stream;
		
        private IFormatter formatter;
		private TcpClient connection;

		private Queue<Response> responses;
		private volatile bool finished;
        private EventWaitHandle _waitHandle;
		public ServerProxy(string host, int port)
		{
			this.host = host;
			this.port = port;
			responses=new Queue<Response>();
		}

		public virtual void login(User user, IObserver client)
		{
			initializeConnection();
			sendRequest(new LoginRequest(user));
			Response response =readResponse();
			if (response is OkResponse)
			{
				this.client=client;
				return;
			}
			if (response is ErrorResponse)
			{
				ErrorResponse err =(ErrorResponse)response;
				closeConnection();
				throw new PersistenceException(err.getMessage());
			}
		}


	public virtual void logout(User user)
		{
			sendRequest(new LogoutRequest(user));
			Response response =readResponse();
			closeConnection();
			if (response is ErrorResponse)
			{
				ErrorResponse err =(ErrorResponse)response;
				throw new PersistenceException(err.getMessage());
			}
		}


		private void closeConnection()
		{
			finished=true;
			try
			{
				stream.Close();
				//output.close();
				connection.Close();
                _waitHandle.Close();
				client=null;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}

		}

		private void sendRequest(Request request)
		{
			try
			{
                formatter.Serialize(stream, request);
                stream.Flush();
			}
			catch (Exception e)
			{
				throw new PersistenceException("Error sending object "+e);
			}

		}

		private Response readResponse()
		{
			Response response =null;
			try
			{
                _waitHandle.WaitOne();
				lock (responses)
				{
                    //Monitor.Wait(responses); 
                    response = responses.Dequeue();
                
				}
				

			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
			return response;
		}
		private void initializeConnection()
		{
			 try
			 {
				connection=new TcpClient(host,port);
				stream=connection.GetStream();
                formatter = new BinaryFormatter();
				finished=false;
                _waitHandle = new AutoResetEvent(false);
				startReader();
			}
			catch (Exception e)
			{
                Console.WriteLine(e.StackTrace);
			}
		}
		private void startReader()
		{
			Thread tw =new Thread(run);
			tw.Start();
		}


		private void handleUpdate(Response response)
		{
            if (response is updateGetAllExcursii){
                updateGetAllExcursii response1 = (updateGetAllExcursii)response;
                List<Excursie> excursies = response1.getList();
                try
                {
                    client.updateTrips(excursies);

                }
                catch (PersistenceException exc)
                {
                    Console.WriteLine(exc.StackTrace);
                }
            }


        }
		public virtual void run()
			{
				while(!finished)
				{
					try
					{
                        object response = formatter.Deserialize(stream);
						Console.WriteLine("response received "+response);
						if (response is updateGetAllExcursii)
						{
							 handleUpdate((updateGetAllExcursii)response);
						}
						else
						{
							
							lock (responses)
							{
                                					
								 
                                responses.Enqueue((Response)response);
                               
							}
                            _waitHandle.Set();
						}
					}
					catch (Exception e)
					{
						Console.WriteLine("Reading error "+e);
					}
					
				}
			}

        public List<Excursie> findAllExcursii()
        {
            sendRequest(new getExcursiiRequest());
            Response response = readResponse();
            if (response is ErrorResponse){
                ErrorResponse err = (ErrorResponse)response;
                throw new PersistenceException(err.getMessage());
            }
            getExcursiiResponse response1 = (getExcursiiResponse)response;
            return response1.getList();
        }

        public List<Excursie> getExcursiiTableFiltru(string obiectiv, string dupa, string inainte)
        {
            sendRequest(new getExcursiiFilteredRequest(obiectiv, dupa, inainte));
            Response response = readResponse();
            if (response is ErrorResponse){
                ErrorResponse err = (ErrorResponse)response;
                throw new PersistenceException(err.getMessage());
            }
            getExcursiiFilteredResponse response1 = (getExcursiiFilteredResponse)response;
            return response1.getList();
        }

        public Excursie findOneExcursie(int id)
        {
            sendRequest(new findExcursieRequest(id));
            Response response = readResponse();
            if (response is ErrorResponse){
                ErrorResponse err = (ErrorResponse)response;
                throw new PersistenceException(err.getMessage());
            }
            findExcursieResponse response1 = (findExcursieResponse)response;
            return response1.getExcursie1();
        }

        public void updateExcursie(Excursie excursie)
        {
            sendRequest(new updateExcursieRequest(excursie));
            Response response = readResponse();
            if (response is OkResponse){
                Console.WriteLine("modificat");
            }
        else {
                ErrorResponse response1 = (ErrorResponse)response;
                Console.WriteLine("Nu s-a putut modifica" + response1.getMessage());
            }
        }

        public void addRezervare(Rezervare rezervare)
        {
            sendRequest(new addRezervareRequest(rezervare));
            Response response = readResponse();
            if (response is OkResponse){
                Console.WriteLine("adaugat");
            }
        else {
                ErrorResponse response1 = (ErrorResponse)response;
                Console.WriteLine("Nu s-a putut adauga" + response1.getMessage());
            }
        }

       
        //}
    }

}