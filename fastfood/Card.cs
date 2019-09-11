using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rattrapage_MCI.Model
{
    class Card
    {
        //Propriétés
        private int cardQuantity;
        private List<string> plats;

        //constructeur
        public Card(int quantity)
        {
            cardQuantity = quantity;

            plats = new List<string> { "burger", "frites", "salade" };

            Console.WriteLine("Cartes initialisées");
        }

        //getter et setter
        public int CardQuantity { get => cardQuantity; set => cardQuantity = value; }
        public List<string> Plats { get => plats; set => plats = value; }
    }
}
