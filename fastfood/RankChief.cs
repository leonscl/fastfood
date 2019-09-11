using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using static Rattrapage_MCI.Model.Actions;
using System.IO;
using Rattrapage_MCI.Model;

namespace Rattrapage_MCI.Model
{
    class RankChief
    {
        //propriétés
        private Thread rankChiefThread;
        private int id;
        private static int idTrack = 0;
        private List<Actions> toDoRankChief = null;
        private actionDelegate theDelagate;

        //Constructeur
        public RankChief()
        {
            Id = IdTrack;

            ToDoRankChief = new List<Actions>();

            RankChiefThread = new Thread(RankChiefWorkThread);
            RankChiefThread.Start();

            IdTrack++;
        }

        //thread du RankChief
        public void RankChiefWorkThread()
        {
            Console.WriteLine("Thread Chef de rang " + Id + " pret." );

            while (true)
            {
                if (ToDoRankChief.Count != 0)
                {
                    Console.WriteLine("Chef de rang : je suis là");
                    theDelagate = ToDoRankChief.First().MyFunctionDelegate;
                    theDelagate(ToDoRankChief.First().Group);
                    ToDoRankChief.Remove(ToDoRankChief.First());
                }
                Thread.Sleep(1000);
            }
        }

        //prends la commande du client
        public void TakeOrder(CustomerGroup group)
        {
            Order order = new Order(group);

            group.StateGroup = "waiting";
            Console.WriteLine("Serveur : J'ai pris la commande de " + group.IdCustomer);

            //On transmet la commande au Comptoir de commande
            Room.Instance.CurrentOrders.Add(order);
            Room.Instance.CounterOrder.Orders.Add(order);
            group.Order = order;

        }


        //sert la commande au client
        public void ServeClient(CustomerGroup group)
        {
            if (group.StateGroup == "waiting")
            {
                Dish dish = group.Order.WaitingDish.First();
               
                Room.Instance.CounterDishes.WaitingDishs.Remove(dish);
                
                Console.WriteLine("Waiter : Voici les " + dish.TypeDish);
                group.CurrentMeal = dish;
                group.Order.WaitingDish.Remove(dish);
                group.StateGroup = "eating";
            }

        }


        //getter et setter
        public Thread RankChiefThread { get => rankChiefThread; set => rankChiefThread = value; }
        public int Id { get => id; set => id = value; }
        public static int IdTrack { get => idTrack; set => idTrack = value; }
        internal List<Actions> ToDoRankChief { get => toDoRankChief; set => toDoRankChief = value; }
    }
}
