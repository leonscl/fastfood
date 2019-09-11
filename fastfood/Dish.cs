using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Rattrapage_MCI.Model
{
    [DataContract]
    class Dish
    {
        private Order order;
        [DataMember]
        private string typeDish;
        [DataMember]
        private List<string> theDishs;

        public Dish(Order order, string type, List<string> dish)
        {
            Order = order;
            TypeDish = type;
            TheDishs = dish;
        }

        public string TypeDish { get => typeDish; set => typeDish = value; }
        public List<string> TheDishs { get => theDishs; set => theDishs = value; }
        internal Order Order { get => order; set => order = value; }
    }
}
