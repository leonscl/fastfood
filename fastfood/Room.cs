using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rattrapage_MCI.Model
{
    class Room
    {
        //Propriétés
        private Square square1;
        private Square square2;

        private RoomClerk roomClerk;
        private static List<string> toDoRoomClerk = null;

        private HeadWaiter headWaiter;
        private RankChief RankChief;

        private WaitingLine waitLine;
        private WaitingPay waitingPay;
        private Card card;
        private StockRoom stockRoom;
        private List<Order> currentOrders;

        private static Room instance;
        private static readonly object padlock = new object();

        private CounterOrder counterOrder;
        private CounterPlate counterPlate;
        private CounterDishes counterDishes;

        //constructeur
        public Room()
        {
            CurrentOrders = new List<Order>();
            RankChief = new RankChief();

            Console.WriteLine("Service ouvert");

            CounterOrder = new CounterOrder();
            CounterDishes = new CounterDishes();
            CounterPlate = new CounterPlate();

        }

        public static Room Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new Room();
                        }
                    }
                }
                return instance;
            }
        }

        //get et set
        internal RoomClerk RoomClerk { get => roomClerk; set => roomClerk = value; }
        internal Square Square1 { get => square1; set => square1 = value; }
        internal Square Square2 { get => square2; set => square2 = value; }
        internal HeadWaiter HeadWaiter { get => headWaiter; set => headWaiter = value; }
        internal WaitingLine WaitLine { get => waitLine; set => waitLine = value; }
        public static List<string> ToDoRoomClerk { get => toDoRoomClerk; set => toDoRoomClerk = value; }
        internal Card Card { get => card; set => card = value; }
        internal StockRoom StockRoom { get => stockRoom; set => stockRoom = value; }
        internal CounterPlate CounterPlate { get => counterPlate; set => counterPlate = value; }
        internal CounterDishes CounterDishes { get => counterDishes; set => counterDishes = value; }
        internal List<Order> CurrentOrders { get => currentOrders; set => currentOrders = value; }
        internal CounterOrder CounterOrder { get => counterOrder; set => counterOrder = value; }
        internal WaitingPay WaitingPay { get => waitingPay; set => waitingPay = value; }
    }
}
