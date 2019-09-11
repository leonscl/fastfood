using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rattrapage_MCI_cuisine
{
    class CounterPlate
    {
        //propriétés
        private Thread counterPlateThread;
        private List<Dish> dishes;

        Int32 port = 13002;
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");

        //Constructeur
        public CounterPlate()
        {
            Dishes = new List<Dish>();

            CounterPlateThread = new Thread(CounterPlateWorkThread);
            CounterPlateThread.Start();
        }

        //thread du CounterPlate
        public void CounterPlateWorkThread()
        {
            ReceivePlate();
        }

        //méthode pour recevoir les plats sales avec un TcpListener
        public void ReceivePlate()
        {
            TcpListener server = null;

            try
            {

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);
                // Start listening for client requests.
                server.Start();

                // Enter the listening loop.
                while (true)
                {
                    Console.WriteLine("Le comptoire de vaisselle sale attend... ");

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

                    //créer mon objet à partir de mon Json et afficher id ce la commande pour vérifier
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Dish));
                    stream1.Position = 0;
                    Dish newDish = (Dish)ser.ReadObject(stream1);

                    // Shutdown and end connection
                    client.Close();

                    Dishes.Add(newDish);
                    Console.WriteLine("Des plats sales sont arrivés");
                    Thread.Sleep(1000);
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

        //getters et setters
        public int Port { get => port; set => port = value; }
        public IPAddress LocalAddr { get => localAddr; set => localAddr = value; }
        internal List<Dish> Dishes { get => dishes; set => dishes = value; }
        public Thread CounterPlateThread { get => counterPlateThread; set => counterPlateThread = value; }
    }
}
