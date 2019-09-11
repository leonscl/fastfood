using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rattrapage_MCI_cuisine
{
    class ChefCuisine
    {
        
        public List<ChefPartie> Cookers { get; set; }

        private ChefPartie chefPartie1;
        private ChefPartie chefPartie2;

        //Propriétés
        private Thread chefCuisineThread;

        /// Disponibilité
        public bool IsAvailable { get; set; }

        public ChefCuisine()
        {
            ChefCuisineThread = new Thread(ChefCuisineWorkThread);
            ChefCuisineThread.Start();

            ChefPartie1 = new ChefPartie(1);
            ChefPartie2 = new ChefPartie(2);
        }

        private Order order;
        private List<Order> liste_commande;


        //thread du Chef de cuisine
        public void ChefCuisineWorkThread()
        {
            Console.WriteLine("Le thread Chef de cuisine prêt");
            while (true)
            {
                //Récupération de la liste des Commandes envoyées 
                Liste_commande = Kitchen.Instance.CounterOrder.ListOrders;

                if (Liste_commande.Count() > 0)
                {
                    Console.WriteLine("Chef Cuisine : Commande reçue");
                    Order = Liste_commande.First();
                    Thread.Sleep(2000);
                    if (ChefPartie1.IsAvailable == true)
                    {
                        ChefPartie1.PrepareReady(Order);
                        Console.WriteLine("ChefPartie1 s'occupe de la commande");
                        ChefPartie1.IsAvailable = false;
                    }

                    else if (ChefPartie2.IsAvailable == true)
                    {
                        ChefPartie2.PrepareReady(Order);
                        Console.WriteLine("ChefPartie2 s'occupe de la commande");
                        ChefPartie2.IsAvailable = false;
                    }

                    Liste_commande.Remove(Order);
                }
                Thread.Sleep(3000);
            }

        }

        public void PrepareReady(Order order)
        {
            IsAvailable = false;

            DishReady dishReady = new DishReady(order.IdOrder, "plats");

            Kitchen.Instance.CounterDishes.DishReadies.Add(dishReady);

            Thread.Sleep(1000);

            Console.WriteLine("Commande prête");
            this.IsAvailable = true;
        }

        internal Order Order { get => order; set => order = value; }
        internal List<Order> Liste_commande { get => liste_commande; set => liste_commande = value; }
        internal ChefPartie ChefPartie1 { get => chefPartie1; set => chefPartie1 = value; }
        internal ChefPartie ChefPartie2 { get => chefPartie2; set => chefPartie2 = value; }
        public Thread ChefCuisineThread { get => chefCuisineThread; set => chefCuisineThread = value; }
    }
}
