using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nettoyageEncaissements
{
    public class Affaire
    {
        public int id { get; set; }
        public string type { get; set; }
        public double acompte { get; set; }
        public string uid { get; set; }
        public string numaff { get; set; }

        public Affaire(int idaff, string typeaff, double acompteaff, string uidaff, string numaffaire)
        {
            this.id = idaff;
            this.type = typeaff;
            this.acompte = acompteaff;
            this.uid = uidaff;
            this.numaff = numaffaire;
        }
    }

    public class AffaireComparer : IEqualityComparer<Affaire>
    { 
        public bool Equals(Affaire a1, Affaire a2)
        {
            if (Object.ReferenceEquals(a1, a2))
                return true;
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
            int hashType = 0;
            if (obj.uid == "")
                return 0;
            else
                hashType = obj.type.GetHashCode();

            return hashType ^ obj.uid.GetHashCode();
        }
    }
}
