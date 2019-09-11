using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rattrapage_MCI_cuisine
{
    class CounterDishes
    {
        //propriétés
        private Thread socketDishesThread;
        //private List<string> dishes;
        private List<DishReady> dishReadies;

        Int32 port = 13001;
        string serverIp = "127.0.0.1";

        //Constructeur
        public CounterDishes()
        {
            DishReadies = new List<DishReady>();

            SocketDishesThread = new Thread(SocketDishesWorkThread);
            SocketDishesThread.Start();
        }

        //thread du headWaiter
        public void SocketDishesWorkThread()
        {
            while (true)
            {

                if(DishReadies.Count() > 0)
                {
                    DishReady dishes = DishReadies.First();
                    SendDishs(dishes);
                    DishReadies.Remove(dishes);
                }
                else
                {
                    Thread.Sleep(1000);
                }
                Thread.Sleep(1000);
            }

        }


        //Méthode pour envoier les plats prets via un TcpClient
        public void SendDishs(DishReady readyDishes)
        {
            try
            {

                //récupérer le type des dishs et les dish (en string) pour le mettre dans une liste
                List<string> dishes = new List<string>();
                dishes.Add(readyDishes.IdOrder.ToString());
                dishes.Add(readyDishes.Nom);
                /*switch (readyDishes.Type)
                {
                    case (TypeRecette.ENTREE):
                        dishes.Add("entrees");
                        break;
                    case (TypeRecette.PLAT):
                        dishes.Add("plats");
                        break;
                    case (TypeRecette.DESSERT):
                        dishes.Add("desserts");
                        break;
                }*/

                //Instanciation du TcpClient
                TcpClient client = new TcpClient(ServerIp, Port);

                //Envois de la commande au server TcpListener via NetworkStream en utilisant BinaryFormatter
                using (NetworkStream ns = client.GetStream())
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(ns, dishes);
                }
                Console.WriteLine("Commande " + dishes[0] + " dish envoyé ----" + dishes[1] + " --- \n");
                //on stope le TcpClient
                client.Close();

            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

        }



        //getter et setter
        public int Port { get => port; set => port = value; }
        public string ServerIp { get => serverIp; set => serverIp = value; }
        public Thread SocketDishesThread { get => socketDishesThread; set => socketDishesThread = value; }
        public List<DishReady> DishReadies { get => dishReadies; set => dishReadies = value; }
        //public List<string> Dishes { get => dishes; set => dishes = value; }

    }
}
