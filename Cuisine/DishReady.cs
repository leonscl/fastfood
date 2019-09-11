using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rattrapage_MCI_cuisine
{
    class DishReady
    {
        private int idOrder;
        private string nom;
        private TypeRecette type;
        public DishReady(int idOrder, string nom)
        {
            IdOrder = idOrder;
            Nom = nom;
        }

        public TypeRecette Type { get => type; set => type = value; }
        public string Nom { get => nom; set => nom = value; }
        public int IdOrder { get => idOrder; set => idOrder = value; }
    }
}
