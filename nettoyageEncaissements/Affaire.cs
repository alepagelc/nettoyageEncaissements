using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nettoyageEncaissements
{
    class Affaire : IEqualityComparer<Affaire>
    {
        public int id { get; set; }
        public string type { get; set; }
        public double acompte { get; set; }
        public int uid { get; set; }
        public string numaff { get; set; }
        public Encaissement[] encaissements { get; set; }

        public Affaire(int idaff, string typeaff, double acompteaff, int uidaff, string numaffaire)
        {
            this.id = idaff;
            this.type = typeaff;
            this.acompte = acompteaff;
            this.uid = uidaff;
            this.numaff = numaffaire;
        }

        public bool Equals(Affaire a1, Affaire a2)
        {
            if (a1 == null && a2 == null)
                return true;
            else if (a1 == null || a2 == null)
                return false;
            else if (a1.type == a2.type && a1.uid == a2.uid)
                return true;
            else
                return false;
        }

        public int GetHashCode(Affaire obj)
        {
            return uid;
        }
    }
}
