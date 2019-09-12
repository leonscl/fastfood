using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuisine
{
    class DishReady
    {
        private int idOrder;
        private string nom;
        public DishReady(int idOrder, string nom)
        {
            IdOrder = idOrder;
            Nom = nom;
        }
        public string Nom { get => nom; set => nom = value; }
        public int IdOrder { get => idOrder; set => idOrder = value; }
    }
}
