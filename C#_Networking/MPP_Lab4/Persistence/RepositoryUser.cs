using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Data;
using Model;
namespace Persistence
{
    public class RepositoryUser : IRepositoryUser

    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public RepositoryUser()
        {
            log.Info("Creare RepoUseri");
        }

       
        public User findOne(string username)
        {
            log.InfoFormat("Cautare user cu username {0}", username);
            IDbConnection con = DBUtils.getConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select password from user where username=@username";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@username";
                paramId.Value = username;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        string password = dataR.GetString(0);
                        User user = new User(username, password);
                        log.Info("Am gasit user cu username-ul dat");
                        return user;
                    }
                }
            }
            log.Info("Nu am gasit user cu username-ul dat");
            return new User( null, null);

        }

        public void save(User user)
        {
            log.Info("Adaugare user");
            var con = DBUtils.getConnection();

            if (findOne(user.Username) == new User(null, null))
            {

                using (var comm = con.CreateCommand())
                {
                    comm.CommandText = "insert into user  values (@username, @password)";
                   
                    var paramName = comm.CreateParameter();
                    paramName.ParameterName = "@username";
                    paramName.Value = user.Username;
                    comm.Parameters.Add(paramName);

                    var paramPassword = comm.CreateParameter();
                    paramPassword.ParameterName = "@password";
                    paramPassword.Value = user.Password;
                    comm.Parameters.Add(paramPassword);

                    var result = comm.ExecuteNonQuery();
                    if (result == 0)
                        log.Info("Nu a fost adaugat user-ul");
                    else
                        log.Info("User adaugat cu succes");
                }
            }
            else
                log.Info("Exista deja user cu acest username");

        }
        public void delete(string username)
        {
            log.Info("Stergere user");
            IDbConnection con = DBUtils.getConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "delete from user where username=@username";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@username";
                paramId.Value = username;
                comm.Parameters.Add(paramId);
                var dataR = comm.ExecuteNonQuery();
                if (dataR == 0)
                    log.Info("Nu a fost sters user-ul");
                else
                    log.Info("User sters cu succes");
            }
        }

        public void update(User user)
        {
            log.Info("Update user");
            var con = DBUtils.getConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "update user set  password=@password where username=@username";

                var paramName = comm.CreateParameter();
                paramName.ParameterName = "@username";
                paramName.Value = user.Username;
                comm.Parameters.Add(paramName);

                var paramPassword = comm.CreateParameter();
                paramPassword.ParameterName = "@password";
                paramPassword.Value = user.Password;
                comm.Parameters.Add(paramPassword);

               

                var result = comm.ExecuteNonQuery();
                if (result == 0)
                    log.Info("Nu a fost modificat user-ul");
                else
                    log.Info("User modificat cu succes");
            }
        }
        public List<User> findAll()
        {
            IDbConnection con = DBUtils.getConnection();
            List<User> users = new List<User>();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from user";

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        string username = dataR.GetString(0);
                        string password = dataR.GetString(1);
                        User user = new User(username, password);
                        users.Add(user);
                    }
                }
            }
            return users;

        }
    }
}
