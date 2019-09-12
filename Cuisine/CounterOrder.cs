using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Cuisine
{
    class CounterOrder
    {
        //propriétés
        private Thread counterOrderThread;
        Int32 port = 13000;
        IPAddress localAddress = IPAddress.Parse("127.0.0.1");
        private List<Order> listOrders;

        //Constructeur
        public CounterOrder()
        {
            ListOrders = new List<Order>();

            CounterOrderThread = new Thread(CounterorderWorkThread);
            CounterOrderThread.Start();
        }

        //thread du TcpListener pour écouter la salle et récupérer les commandes
        public void CounterorderWorkThread()
        {
            ReceiveOrder();
        }

        //Méthode pour recevoir les commandes faites via un TcpListener
        public void ReceiveOrder()
        {
            TcpListener server = null;

            try
            {
                server = new TcpListener(LocalAddress, Port);
                // Start listening for client requests.
                server.Start();

                // Enter the listening loop.
                while (true)
                {
                    Console.WriteLine("Le comptoir des commandes attend... ");

                    // Perform a blocking call to accept requests.
                    TcpClient client = server.AcceptTcpClient();

                    MemoryStream stream1 = new MemoryStream();

                    using (NetworkStream ns = client.GetStream())
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        stream1 = (MemoryStream)bf.Deserialize(ns);
                    }

                    //Juste afficher mon stream jSON
                    /*stream1.Position = 0;
                    StreamReader sr = new StreamReader(stream1);
                    Console.Write("JSON form of Person object: ");
                    Console.WriteLine(sr.ReadToEnd());*/

                    //créer mon objet à partir de mon Json et afficher id de la commande pour vérifier
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Order));
                    stream1.Position = 0;
                    Order newOrder = (Order)ser.ReadObject(stream1);
                    Console.Write("id de la commande" + newOrder.IdOrder);

                    ListOrders.Add(newOrder);
                    Console.WriteLine("Une commande est arrivée");
                    // Shutdown and end connection
                    client.Close();

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

        //getter et setter
        public int Port { get => port; set => port = value; }
        public Thread CounterOrderThread { get => counterOrderThread; set => counterOrderThread = value; }
        public IPAddress LocalAddress { get => localAddress; set => localAddress = value; }
        internal List<Order> ListOrders { get => listOrders; set => listOrders = value; }
    }
}
