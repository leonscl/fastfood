using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rattrapage_MCI.Model
{
    class CounterPlate
    {

        //propriétés
        private Thread counterPlateThread;
        private List<Dish> dishes;

        Int32 port = 13002;
        string serverIp = "127.0.0.1";

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
            while (true)
            {
                if (Dishes == null)
                {
                    Thread.Sleep(1000);
                }
                else if (Dishes.Count > 0)
                {
                    Dish dish = Dishes.First();
                    SendPlats(dish);
                    Dishes.Remove(dish);
                }
                Thread.Sleep(1000);
            }

        }


        //Envoie des plats sales via un TcpClient
        public void SendPlats(Dish dish)
        {
            try
            {
                //sérialisation de la commande dans un JSON
                MemoryStream stream1 = new MemoryStream();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Dish));
                ser.WriteObject(stream1, dish);

                //Instanciation du TcpClient
                TcpClient client = new TcpClient(ServerIp, Port);

                //Envois des plats sales au server TcpListener via NetworkStream en utilisant BinaryFormatter
                using (NetworkStream ns = client.GetStream())
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(ns, stream1);
                    stream1.Close();
                }

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
        internal List<Dish> Dishes { get => dishes; set => dishes = value; }
        public Thread CounterPlateThread { get => counterPlateThread; set => counterPlateThread = value; }
    }
}
