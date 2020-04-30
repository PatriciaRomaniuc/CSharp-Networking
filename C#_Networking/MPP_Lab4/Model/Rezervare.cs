using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]

    public class Rezervare:Entity<int>
    {
        private string numeClient { get; set; }
        private string nrTelefon { get; set; }
        private int nrBilete { get; set; }
        private int idExcursie { get; set; }

        public Rezervare(int id, string numeClient, string nrTelefon, int nrBilete, int idExcursie):base(id)
        {
            this.numeClient = numeClient;
            this.nrTelefon = nrTelefon;
            this.nrBilete = nrBilete;
            this.idExcursie = idExcursie;
        }
        public string NumeClient
        {
            get { return numeClient; }
            set { this.numeClient = value; }
        }
        public string NrTelefon
        {
            get { return nrTelefon; }
            set { this.nrTelefon = value; }
        }
        public int NrBilete
        {
            get { return nrBilete; }
            set { this.nrBilete = value; }
        }
        public int IdExcursie
        {
            get { return idExcursie; }
            set { this.idExcursie = value; }
        }
        public static bool operator ==(Rezervare e1, Rezervare e2)
        {
            return e1.Id == e2.Id;
        }

        public static bool operator !=(Rezervare e1, Rezervare e2)
        {
            return e1.Id != e2.Id;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj is Rezervare)
            {
                Rezervare rezervare = obj as Rezervare;
                return base.Id == rezervare.Id;
            }
            return false;
        }
    }

}
