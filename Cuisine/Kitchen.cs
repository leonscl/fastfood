using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rattrapage_MCI_cuisine
{
    class Kitchen
    {
        private ChefCuisine chefCuisine;
        private ChefCuisine chefCuisine1;

        private static Kitchen instance;
        private static readonly object padlock = new object();

        private CounterOrder counterOrder;
        private CounterPlate counterPlate;
        private CounterDishes counterDishes;

        public Kitchen()
        {
            counterOrder = new CounterOrder();
            counterDishes = new CounterDishes();
            counterPlate = new CounterPlate();

            chefCuisine1 = new ChefCuisine();
            chefCuisine = new ChefCuisine();
            Console.WriteLine("Cuisine prête");
        }

        public static Kitchen Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new Kitchen();
                        }
                    }
                }
                return instance;
            }
        }

        internal ChefCuisine ChefCuisine { get => chefCuisine; set => chefCuisine = value; }
        internal CounterPlate CounterPlate { get => counterPlate; set => counterPlate = value; }
        internal CounterDishes CounterDishes { get => counterDishes; set => counterDishes = value; }
        internal CounterOrder CounterOrder { get => counterOrder; set => counterOrder = value; }
    }
}
