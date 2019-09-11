using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rattrapage_MCI_cuisine
{
    class ChefPartie
    {
        /// Identification du chef de partie
        public int Id { get; set; }

        /// Disponibilité
        public bool IsAvailable { get; set; }

        /// instanciation du chef de de partie
        public ChefPartie(int id)
        {
            this.Id = id;
            this.IsAvailable = true;
        }

        //private List<string> counterDishes;

        public void PrepareReady(Order order)
        {
            IsAvailable = false;

            DishReady dishReady = new DishReady(order.IdOrder, "plats");

            Kitchen.Instance.CounterDishes.DishReadies.Add(dishReady);

            Thread.Sleep(1000);

            Console.WriteLine("Commande prête");
            this.IsAvailable = true;
        }        
    }
}

