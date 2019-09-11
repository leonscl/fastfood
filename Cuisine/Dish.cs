using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Rattrapage_MCI_cuisine
{
    [DataContract]
    class Dish
    {
        [DataMember]
        private string typeDish;
        [DataMember]
        private List<string> theDishs;


        public string TypeDish { get => typeDish; set => typeDish = value; }
        public List<string> TheDishs { get => theDishs; set => theDishs = value; }

        /// <summary>
        /// Unique idnetifier of a dish
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Recipe of the dish ordered
        /// </summary>
        public Recette Recipe { get; set; }

        /// <summary>
        /// State of the dish
        /// </summary>
        public bool Ready { get; set; }

        /// <summary>
        /// Instantiate a new dish
        /// </summary>
        public Dish()
        {
            this.Ready = false;
        }
    }
}
