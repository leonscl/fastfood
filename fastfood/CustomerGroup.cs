using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Rattrapage_MCI.Model.Actions;

namespace Rattrapage_MCI.Model
{
    class CustomerGroup
    {

        //Propriétés
        private int idCustomer;
        private static int idCustomerTrack = 0;

        private string stateGroup;
        private Dish currentMeal = null;

        private Thread groupThread;

        private int customerNumber;
        private Order order = null;

        private List<string> plats = null;
        

        private bool canbeDeleted = false;

        //Constructeur
        public CustomerGroup()
        {
            IdCustomer = IdCustomerTrack;
            
            Random rand = new Random();
            CustomerNumber = 1;
            Plats = new List<string>();

            groupThread = new Thread(CustomerGroupThread);
            groupThread.Start();

            IdCustomerTrack++;
        }

        //thread du Groupe de clients
        public void CustomerGroupThread()
        {
            try
            {
                Console.WriteLine("Thread Group de clients n° " + IdCustomer + "initialisé");
                while (true)
                {
                    switch (StateGroup)
                    {
                        case "waiting":
                            Console.WriteLine("Le groupe n° " + IdCustomer + " est en attente");
                            Thread.Sleep(6000);
                            break;

                        case "ordering":
                            ChooseOrder(Room.Instance.Card);
                            Thread.Sleep(3000);
                            break;

                        case "eating":
                            Eat();
                            break;
                        case "ended_good":
                            Console.WriteLine("On a fini et on a aimé");
                            break;
                        case "ended bad":
                            Console.WriteLine("On a fini mais ce n'était pas bon");
                            break;
                    }
                    Thread.Sleep(1000);

                }
            }
            catch (ThreadAbortException)
            {
                Console.WriteLine("Le groupe n° " + IdCustomer + " est partis ");
            }
        }

        //méthode pour choisir ce qu'ils vont manger (rand pour simuler des choix différents)
        public void ChooseOrder(Card card)
        {
            Random rand = new Random();

         
            Plats.Add(card.Plats[rand.Next(0, 3)]);
            

            StateGroup = "OrderComplete";

            Console.WriteLine("Le groupe n° " + IdCustomer + ": a réfléchi et veux commander");
        }

        public void Eat()
        {
            //Thread.Sleep(5000);

            if (CurrentMeal.TypeDish == "plats")
            {
                Console.WriteLine("Le groupe n°" + IdCustomer + " mange son Plat.");
                Thread.Sleep(25000);
                Random aleatoire = new Random();
                int satisfaction = aleatoire.Next(0, 1); // Génère un aléatoire pour déterminer 
                switch (satisfaction)
                {
                    case (0):
                        stateGroup = "ended good";
                        break;
                    case (1):
                        stateGroup = "ended bad";
                        break;
                }
                Console.WriteLine(stateGroup);
            }
        }

        public void Move(string depart, string arrivée)
        {
            Console.WriteLine("Le groupe de déplace de " + depart + " vers " + arrivée);
        }

        //getter et setter
        public int CustomerNumber { get => customerNumber; set => customerNumber = value; }
        public int IdCustomer { get => idCustomer; set => idCustomer = value; }
        public static int IdCustomerTrack { get => idCustomerTrack; set => idCustomerTrack = value; }
        public string StateGroup { get => stateGroup; set => stateGroup = value; }
        
       
        public List<string> Plats { get => plats; set => plats = value; }
      
        internal Order Order { get => order; set => order = value; }
        internal Dish CurrentMeal { get => currentMeal; set => currentMeal = value; }
        public bool CanbeDeleted { get => canbeDeleted; set => canbeDeleted = value; }
    }
}
