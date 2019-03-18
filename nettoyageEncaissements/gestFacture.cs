using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nettoyageEncaissements
{
    public class GestFacture : IComparable
    {
        public string identifiant { get; set; }
        public string numero { get; set; }
        public string uid { get; set; }

        public int CompareTo(object obj)
        {
            GestFacture facture = (GestFacture)obj;
            if (this.uid == facture.uid)
                return 1;
            return 0;
        }
    }
}
