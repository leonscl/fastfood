using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Rattrapage_MCI.Model.Actions;

namespace Rattrapage_MCI.Model
{
    class CounterDishes
    {
        //propriétés
        private Thread counterDishesThread;

        Int32 port = 13001;
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");

        private List<Dish> waitingDishs;

        //Constructeur
        public CounterDishes()
        {
            WaitingDishs = new List<Dish>();

            CounterDishesThread = new Thread(CounterDishesWorkThread);
            CounterDishesThread.Start();
        }

        //thread du CounterDishes
        public void CounterDishesWorkThread()
        {
            ReceiveDishes();
        }


        //méthode pour recevoir les plats avec un TcpListener
        public void ReceiveDishes()
        {
            TcpListener server = null;
            List<string> dishes = new List<string>();

            try
            {
                // TcpListener server
                server = new TcpListener(localAddr, port);
                // Start listening for client requests.
                server.Start();

                // Enter the listening loop.
                while (true)
                {
                    Console.Write("\n Le comptoire Dishes attend des plats... \n");

                    // Perform a blocking call to accept requests.
                    TcpClient client = server.AcceptTcpClient();
                    //Console.WriteLine("\n Connected!");

                    using (NetworkStream ns = client.GetStream())
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        dishes = (List<string>)bf.Deserialize(ns);
                    }

                    Console.Write("\n Des plats sont arrivés \n");
                    // Shutdown and end connection
                    client.Close();

                    //appel de la méthode AddActionWaiter()
                    AddActionWaiter(dishes);

                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }

        }

        //méthode pour ajouter l'action de donner le plat qui vient d'arriver aux clients concernés
        public void AddActionWaiter(List<string> dishes)
        {
            //retrouver la commande
            Order order = Room.Instance.CurrentOrders.Find(x => x.IdOrder == int.Parse(dishes[0]));

            List<string> myDish = new List<string>();
            if (dishes[1] == "entrees")
            {
                myDish = order.Entriees;
            }
            else if(dishes[1] == "plats")
            {
                myDish = order.Plats;
            }
            else if (dishes[1] == "desserts")
            {
                myDish = order.Deserts;
            }

            //instancier l'objet Dish et ajouter Dish à l'order et la waitingDish
            Dish theDish = new Dish(order, dishes[1], myDish);
            order.WaitingDish.Add(theDish);
            WaitingDishs.Add(theDish);

            //récupérer la liste des waiters suivant le carré donc suivant où sont les clients
            List<Waiter> waiters = order.CustomerGroup.Table.TheSquare.Waiters;

            //récupérer le waiter qui a la liste de chose à faire la moins grande
            Waiter theWaiter = waiters.OrderBy(x => x.ToDoWaiter.Count()).First();

            //Ajout de l'action à la toDoliste pour le waiter
            actionDelegate myActionDelegate = new actionDelegate(theWaiter.ServeClient);
            Actions toDo = new Actions(myActionDelegate, order.CustomerGroup);
            theWaiter.ToDoWaiter.Add(toDo);
        }


        //getter et setter
        public int Port { get => port; set => port = value; }
        public IPAddress LocalAddr { get => localAddr; set => localAddr = value; }
        internal List<Dish> WaitingDishs { get => waitingDishs; set => waitingDishs = value; }
        public Thread CounterDishesThread { get => counterDishesThread; set => counterDishesThread = value; }
    }
}
