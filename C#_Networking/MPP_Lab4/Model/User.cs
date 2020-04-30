using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]

    public class User
    {
        private string username { get; set;}
        private string password{get;set;}

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public string Username
        {
            get { return username; }
            set { this.username = value; }
        }

        public string Password
        {
            get { return password; }
            set { this.password = value; }
        }
        public static bool operator ==(User u1, User u2)
        {
            return u1.Username == u2.Username;
        }

        public static bool operator !=(User u1, User u2)
        {
            return u1.Username != u2.Username;
        }

      

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
