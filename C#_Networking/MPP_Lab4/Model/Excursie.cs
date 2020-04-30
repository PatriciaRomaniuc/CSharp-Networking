using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]

    public class Excursie:Entity<int>
    {
        private string numeObiectiv { get; set; }
        private string numeFirmaTransport { get; set; }
        private string oraPlecarii { get; set; }
        private float pret { get; set; }
        private int nrLocuriDisponibile { get; set; }

        public Excursie(int id,string numeObiectiv, string numeFirmaTransport, string oraPlecarii, float pret, int nrLocuriDisponibile):base(id)
        {
            this.numeObiectiv = numeObiectiv;
            this.numeFirmaTransport = numeFirmaTransport;
            this.oraPlecarii = oraPlecarii;
            this.pret = pret;
            this.nrLocuriDisponibile = nrLocuriDisponibile;
        }
        public string NumeObiectiv {
            get { return numeObiectiv; }
            set { this.numeObiectiv = value; } }

        public string NumeFirmaTransport
        {
            get { return numeFirmaTransport; }
            set { this.numeFirmaTransport = value; }
        }
        public string OraPlecarii
        {
            get { return oraPlecarii; }
            set { this.oraPlecarii = value; }

        }
        public float Pret
        {
            get { return pret; }
            set { this.Pret = value; }
        }
        public int NrLocuriDisponibile
        {
            get { return nrLocuriDisponibile; }
            set { this.nrLocuriDisponibile = value; }
        }


        public static bool operator ==(Excursie e1, Excursie e2)
        {
            return e1.Id == e2.Id;
        }

        public static bool operator !=(Excursie e1, Excursie e2)
        {
            return e1.Id != e2.Id;
        }

        public override bool Equals(object obj)
        {
            if (obj is Excursie)
            {
                Excursie excursie = obj as Excursie;
                return base.Id == excursie.Id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

       
    }
}
