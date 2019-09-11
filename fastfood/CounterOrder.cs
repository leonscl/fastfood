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

namespace Rattrapage_MCI.Model
{

    class CounterOrder
    {
        //propriétés
        private Thread counterOrderThread;
        private List<Order> orders;

        Int32 port = 13000;
        string serverIp = "127.0.0.1";

        //Constructeur
        public CounterOrder()
        {
            Orders = new List<Order>();

            CounterOrderThread = new Thread(CounterOrderWorkThread);
            CounterOrderThread.Start();
        }

        //thread du CounterOrder
        public void CounterOrderWorkThread()
        {
            while (true)
            {
                //List<Order> listOrders = Orders;
                if (Orders == null)
                {
                    Thread.Sleep(1000);
                }
                else if (Orders.Count > 0)
                {
                    Order order = Orders.First();
                    SendOrder(order);
                    Orders.Remove(order);
                }
                Thread.Sleep(1000);
            }
            
        }


        //Envoie de la commande via un TcpClient
        public void SendOrder(Order order)
        {
            try
            {
                //sérialisation de la commande dans un JSON
                MemoryStream stream1 = new MemoryStream();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Order));
                ser.WriteObject(stream1, order);

                //Instanciation du TcpClient
                TcpClient client = new TcpClient(ServerIp, Port);

                //Envois de la commande au server TcpListener via NetworkStream en utilisant BinaryFormatter
                using (NetworkStream ns = client.GetStream())
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(ns, stream1);
                    stream1.Close();
                }
                
                //on stope le TcpClient
                client.Close();
                
                Console.Write("----------Commande" + order.IdOrder + "envoyée dans la cuisine --------- \n");

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
        internal List<Order> Orders { get => orders; set => orders = value; }
        public string ServerIp { get => serverIp; set => serverIp = value; }
        public Thread CounterOrderThread { get => counterOrderThread; set => counterOrderThread = value; }
    }
}
