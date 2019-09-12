using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cuisine
{
    class ChefCuisine
    { 
        //Propriétés
        private Thread chefCuisineThread;

        /// Disponibilité
        public bool IsAvailable { get; set; }

        public ChefCuisine()
        {
            chefCuisine1 = new ChefCuisine();
            chefCuisine = new ChefCuisine();
            ChefCuisineThread = new Thread(ChefCuisineWorkThread);
            ChefCuisineThread.Start();
            //ThreadPool.QueueUserWorkItem(Cuisine);
        }

        private Order order;
        private List<Order> liste_commande;
        private ChefCuisine chefCuisine1;
        private ChefCuisine chefCuisine;


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
                    if (chefCuisine.IsAvailable == true)
                    {
                        chefCuisine.PrepareReady(Order);
                        Console.WriteLine("ChefPartie1 s'occupe de la commande");
                        chefCuisine.IsAvailable = false;
                    }

                    else if (chefCuisine1.IsAvailable == true)
                    {
                        chefCuisine1.PrepareReady(Order);
                        Console.WriteLine("ChefPartie2 s'occupe de la commande");
                        chefCuisine1.IsAvailable = false;
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
        public Thread ChefCuisineThread { get => chefCuisineThread; set => chefCuisineThread = value; }
    }
}
