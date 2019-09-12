using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Cuisine
{
    [DataContract]
    class Order
    {
        //Propriétés
        [DataMember]
        private int idOrder;
        [DataMember]
        private List<string> entriees;
        [DataMember]
        private List<string> plats;
        [DataMember]
        private List<string> deserts;

        public int IdOrder { get => idOrder; set => idOrder = value; }
        public List<string> Plats { get => plats; set => plats = value; }

       
        public List<Dish> Dishes { get; set; }

        public Order()
        {
        }

    }
}
