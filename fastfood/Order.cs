using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Rattrapage_MCI.Model
{
    [DataContract]
    class Order
    {
        //Propriétés
        [DataMember]
        private int idOrder;
        private static int idIncrementor = 0;
        private CustomerGroup customerGroup;
        private Table table;
        [DataMember]
        private List<string> entriees;
        [DataMember]
        private List<string> plats;
        [DataMember]
        private List<string> deserts;

        private int price = 0;

        private List<Dish> waitingDish = null;

        //constructeur
        public Order(CustomerGroup group)
        {
            IdOrder = IdIncrementor;
            IdIncrementor++;

            CustomerGroup = group;
            Table = group.Table;

            Entriees = group.Entriees;
            Plats = group.Plats;
            Deserts = group.Deserts;

            foreach (string entry in Entriees)
            {
                Price = Price + 3;
            }
            foreach (string plat in Plats)
            {
                Price = Price + 5;
            }
            foreach (string dessert in Deserts)
            {
                Price = Price + 4;
            }

            WaitingDish = new List<Dish>();

            Console.WriteLine("La commande " + IdOrder + " a été faite.");
        }

        //get et set
        public List<string> Plats { get => plats; set => plats = value; }
        public int IdOrder { get => idOrder; set => idOrder = value; }
        internal CustomerGroup CustomerGroup { get => customerGroup; set => customerGroup = value; }
        public static int IdIncrementor { get => idIncrementor; set => idIncrementor = value; }
        internal Table Table { get => table; set => table = value; }
        public List<string> Entriees { get => entriees; set => entriees = value; }
        public List<string> Deserts { get => deserts; set => deserts = value; }
        internal List<Dish> WaitingDish { get => waitingDish; set => waitingDish = value; }
        public int Price { get => price; set => price = value; }
    }
}
