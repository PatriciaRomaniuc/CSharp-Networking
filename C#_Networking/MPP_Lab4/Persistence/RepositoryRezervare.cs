using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Model;
namespace Persistence
{
    public class RepositoryRezervare : IRepositoryRezervare
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public RepositoryRezervare()
        {
            log.Info("Creare Repository Rezervare");
        }

        public int size()
        {
            log.Info("Returnare dimensiune");
            IDbConnection con = DBUtils.getConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select count(*) from rezervare";

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        int nr = dataR.GetInt32(0);
                        log.Info("Am reusit returnare dimensiune");
                        return nr;
                    }
                }
            }
            return 0;
        }

        public void save(Rezervare rezervare)
        {
            log.Info("Adaugare rezervare");
            var con = DBUtils.getConnection();

            if (findOne(rezervare.Id) == new Rezervare(-1, null, null, -1, -1))
            {

                using (var comm = con.CreateCommand())
                {
                    comm.CommandText = "insert into rezervare  values (@idRezervare, @numeClient,@nrTelefon,@nrBilete,@idExcursie)";

                    var paramId = comm.CreateParameter();
                    paramId.ParameterName = "@idRezervare";
                    paramId.Value = rezervare.Id;
                    comm.Parameters.Add(paramId);

                    var paramName = comm.CreateParameter();
                    paramName.ParameterName = "@numeClient";
                    paramName.Value = rezervare.NumeClient;
                    comm.Parameters.Add(paramName);

                    var paramNr = comm.CreateParameter();
                    paramNr.ParameterName = "@nrTelefon";
                    paramNr.Value = rezervare.NrTelefon;
                    comm.Parameters.Add(paramNr);

                    var paramBilete = comm.CreateParameter();
                    paramBilete.ParameterName = "@nrBilete";
                    paramBilete.Value = rezervare.NrBilete;
                    comm.Parameters.Add(paramBilete);

                    var paramEx = comm.CreateParameter();
                    paramEx.ParameterName = "@idExcursie";
                    paramEx.Value = rezervare.IdExcursie;
                    comm.Parameters.Add(paramEx);


                    var result = comm.ExecuteNonQuery();
                    if (result == 0)
                        log.Info("Nu a fost adaugata rezervarea");
                    else
                        log.Info("Rezervare adaugata cu succes");
                }
            }
            else
                log.Info("Exista deja Rezervare cu acest id");

        }
        public void delete(int idRezervare)
        {
            log.Info("Stergere rezervare");
            IDbConnection con = DBUtils.getConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "delete from rezervare where idRezervare=@idRezervare";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@idRezervare";
                paramId.Value = idRezervare;
                comm.Parameters.Add(paramId);
                var dataR = comm.ExecuteNonQuery();
                if (dataR == 0)
                    log.Info("Nu a fost stearsa rezervarea");
                else
                    log.Info("Rezervarea a fost stearsa");
            }
        }

        public void update(Rezervare rezervare)
        {
            log.Info("Update rezervare");
            var con = DBUtils.getConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "update rezervare set  numeClient=@numeClient, nrTelefon=@nrTelefon,nrBilete=@nrBilete, idExcursie=@idExcursie  where idRezervare=@idRezervare";


                var paramName = comm.CreateParameter();
                paramName.ParameterName = "@numeClient";
                paramName.Value = rezervare.NumeClient;
                comm.Parameters.Add(paramName);

                var paramNr = comm.CreateParameter();
                paramNr.ParameterName = "@nrTelefon";
                paramNr.Value = rezervare.NrTelefon;
                comm.Parameters.Add(paramNr);

                var paramBilete = comm.CreateParameter();
                paramBilete.ParameterName = "@nrBilete";
                paramBilete.Value = rezervare.NrBilete;
                comm.Parameters.Add(paramBilete);

                var paramEx = comm.CreateParameter();
                paramEx.ParameterName = "@idExcursie";
                paramEx.Value = rezervare.IdExcursie;
                comm.Parameters.Add(paramEx);

                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@idRezervare";
                paramId.Value = rezervare.Id;
                comm.Parameters.Add(paramId);

                var result = comm.ExecuteNonQuery();
                if (result == 0)
                    log.Info("Nu a fost modificata excursia");
                else
                    log.Info("Excursie modificata");
            }
        }

        public Rezervare findOne(int idRezervare)
        {
            log.InfoFormat("Cautare excursie cu id {0}", idRezervare);
            IDbConnection con = DBUtils.getConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select numeClient, nrTelefon, nrBilete, idExcursie  from rezervare where idRezervare=@idRezervare";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@idRezervare";
                paramId.Value = idRezervare;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        string numeClient = dataR.GetString(0);
                        string nrTelefon = dataR.GetString(1);
                        int nrBilete = dataR.GetInt32(2);
                        int idExcursie = dataR.GetInt32(3);
                        Rezervare rezervare = new Rezervare(idRezervare, numeClient, nrTelefon, nrBilete, idExcursie);
                        log.Info("Am gasit rezervare cu id-ul dat");
                        return rezervare;
                    }
                }
            }
            log.Info("Nu am gasit rezervare cu id-ul dat");
            return new Rezervare(-1, null, null, -1, -1);
        }


        public List<Rezervare> findAll()
        {
            IDbConnection con = DBUtils.getConnection();
            List<Rezervare> rezervari = new List<Rezervare>();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from rezervare";

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int id = dataR.GetInt32(0);
                        string numeClient = dataR.GetString(1);
                        string nrTelefon = dataR.GetString(2);
                        int nrBilete = dataR.GetInt32(3);
                        int idExcursie = dataR.GetInt32(4);

                        Rezervare rezervare = new Rezervare(id, numeClient, nrTelefon, nrBilete, idExcursie);
                        rezervari.Add(rezervare);
                    }
                }
            }
            return rezervari;

        }
    }
}
