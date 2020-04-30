using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Model;
namespace Persistence
{
    public class RepositoryExcursie : IRepositoryExcursie
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public RepositoryExcursie()
        {
            log.Info("Creare Repository Excursie");
        }

        public int size()
        {
            log.Info("Returnare dimensiune");
            IDbConnection con = DBUtils.getConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select count(*) from excursie";

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

        public void save(Excursie excursie)
        {
            log.Info("Adaugare excursie");
            var con = DBUtils.getConnection();

            if (findOne(excursie.Id) == new Excursie(-1, null, null, null, -1, -1))
            {

                using (var comm = con.CreateCommand())
                {
                    comm.CommandText = "insert into excursie  values (@idExcursie, @numeObiectiv,@numeFirmaTransport,@oraPlecarii,@pret, @nrLocuriDisponibile)";

                    var paramId = comm.CreateParameter();
                    paramId.ParameterName = "@idExcursie";
                    paramId.Value = excursie.Id;
                    comm.Parameters.Add(paramId);

                    var paramName = comm.CreateParameter();
                    paramName.ParameterName = "@numeObiectiv";
                    paramName.Value = excursie.NumeObiectiv;
                    comm.Parameters.Add(paramName);

                    var paramNumeF = comm.CreateParameter();
                    paramNumeF.ParameterName = "@numeFirmaTransport";
                    paramNumeF.Value = excursie.NumeFirmaTransport;
                    comm.Parameters.Add(paramNumeF);

                    var paramOra = comm.CreateParameter();
                    paramOra.ParameterName = "@oraPlecarii";
                    paramOra.Value = excursie.OraPlecarii;
                    comm.Parameters.Add(paramOra);

                    var paramPret = comm.CreateParameter();
                    paramPret.ParameterName = "@pret";
                    paramPret.Value = excursie.Pret;
                    comm.Parameters.Add(paramPret);

                    var paramLocuri = comm.CreateParameter();
                    paramLocuri.ParameterName = "@nrLocuriDisponibile";
                    paramLocuri.Value = excursie.NrLocuriDisponibile;
                    comm.Parameters.Add(paramLocuri);


                    var result = comm.ExecuteNonQuery();
                    if (result == 0)
                        log.Info("Nu a fost adaugata excursia");
                    else
                        log.Info("Excursie adaugata cu succes");
                }
            }
            else
                log.Info("Exista deja excursie cu acest id");

        }
        public void delete(int idExcursie)
        {
            log.Info("Stergere excursie");
            IDbConnection con = DBUtils.getConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "delete from excursie where idExcursie=@idExcursie";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@idExcursie";
                paramId.Value = idExcursie;
                comm.Parameters.Add(paramId);
                var dataR = comm.ExecuteNonQuery();
                if (dataR == 0)
                    log.Info("Nu a fost stearsa excursia");
                else
                    log.Info("Excursia a fost stearsa");
            }
        }

        public void update(Excursie excursie)
        {
            log.Info("Update excursie");
            var con = DBUtils.getConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "update excursie set numeObiectiv=@numeObiectiv,numeFirmaTransport=@numeFirmaTransport,oraPlecarii=@oraPlecarii,pret=@pret, nrLocuriDisponibile=@nrLocuriDisponibile where idExcursie=@idExcursie";

                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@idExcursie";
                paramId.Value = excursie.Id;
                comm.Parameters.Add(paramId);

                var paramName = comm.CreateParameter();
                paramName.ParameterName = "@numeObiectiv";
                paramName.Value = excursie.NumeObiectiv;
                comm.Parameters.Add(paramName);

                var paramNumeF = comm.CreateParameter();
                paramNumeF.ParameterName = "@numeFirmaTransport";
                paramNumeF.Value = excursie.NumeFirmaTransport;
                comm.Parameters.Add(paramNumeF);

                var paramOra = comm.CreateParameter();
                paramOra.ParameterName = "@oraPlecarii";
                paramOra.Value = excursie.OraPlecarii;
                comm.Parameters.Add(paramOra);

                var paramPret = comm.CreateParameter();
                paramPret.ParameterName = "@pret";
                paramPret.Value = excursie.Pret;
                comm.Parameters.Add(paramPret);

                var paramLocuri = comm.CreateParameter();
                paramLocuri.ParameterName = "@nrLocuriDisponibile";
                paramLocuri.Value = excursie.NrLocuriDisponibile;
                comm.Parameters.Add(paramLocuri);

                var result = comm.ExecuteNonQuery();
                if (result == 0)
                    log.Info("Nu a fost modificata excursia");
                else
                    log.Info("Excursie modificata");
            }
        }

        public Excursie findOne(int idExcursie)
        {
            log.InfoFormat("Cautare excursie cu id {0}", idExcursie);
            IDbConnection con = DBUtils.getConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select numeObiectiv, numeFirmaTransport, oraPlecarii, pret,  nrLocuriDisponibile from excursie where idExcursie=@idExcursie";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@idExcursie";
                paramId.Value = idExcursie;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        string numeObiectiv = dataR.GetString(0);
                        string numeFirma = dataR.GetString(1);
                        string oraPlecarii = dataR.GetString(2);
                        float pret = dataR.GetFloat(3);
                        int nrLocuri = dataR.GetInt32(4);
                        Excursie excursie = new Excursie(idExcursie, numeObiectiv, numeFirma, oraPlecarii, pret, nrLocuri);
                        log.Info("Am gasit excursie cu id-ul dat");
                        return excursie;
                    }
                }
            }
            log.Info("Nu am gasit Excursie cu id-ul dat");
            return new Excursie(-1, null, null, null, -1, -1);
        }
        public List<Excursie> findAll()
        {
            IDbConnection con = DBUtils.getConnection();
            List<Excursie> excursii = new List<Excursie>();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from excursie";

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int id = dataR.GetInt32(0);
                        string numeObiectiv = dataR.GetString(1);
                        string numeFirma = dataR.GetString(2);
                        string oraPlecarii = dataR.GetString(3);
                        float pret = dataR.GetFloat(4);
                        int nrLocuri = dataR.GetInt32(5);
                        Excursie excursie = new Excursie(id, numeObiectiv, numeFirma, oraPlecarii, pret, nrLocuri);
                        excursii.Add(excursie);
                    }
                }
            }
            return excursii;

        }
        public List<Excursie> findAllObiectivOra(String obiectiv, String dupa, String inainte)
        {
            IDbConnection con = DBUtils.getConnection();
            List<Excursie> excursii = new List<Excursie>();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "SELECT * FROM excursie WHERE numeObiectiv=@numeObiectiv AND @dupa<=oraPlecarii AND oraPlecarii<=@inainte";
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@numeObiectiv";
                paramId.Value = obiectiv;
                comm.Parameters.Add(paramId);

                var paramName = comm.CreateParameter();
                paramName.ParameterName = "@dupa";
                paramName.Value = dupa;
                comm.Parameters.Add(paramName);

                var paramNumeF = comm.CreateParameter();
                paramNumeF.ParameterName = "@inainte";
                paramNumeF.Value = inainte;
                comm.Parameters.Add(paramNumeF);
                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int id = dataR.GetInt32(0);
                        string numeObiectiv = dataR.GetString(1);
                        string numeFirma = dataR.GetString(2);
                        string oraPlecarii = dataR.GetString(3);
                        float pret = dataR.GetFloat(4);
                        int nrLocuri = dataR.GetInt32(5);
                        Excursie excursie = new Excursie(id, numeObiectiv, numeFirma, oraPlecarii, pret, nrLocuri);
                        excursii.Add(excursie);
                    }
                }
            }
            return excursii;

        }
    }
}
