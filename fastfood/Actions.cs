using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rattrapage_MCI.Model
{
    class Actions
    {
        public delegate void actionDelegate(CustomerGroup customer);

        private actionDelegate myFunctionDelegate;
        private CustomerGroup group;

        public Actions(actionDelegate action, CustomerGroup groups)
        {
            MyFunctionDelegate = action;
            Group = groups;

        }

        internal CustomerGroup Group { get => group; set => group = value; }
        internal actionDelegate MyFunctionDelegate { get => myFunctionDelegate; set => myFunctionDelegate = value; }
    }
}
