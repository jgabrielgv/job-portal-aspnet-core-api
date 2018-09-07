using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Core.Domain
{
    public class Party
    {
        public Party()
        {
            Orders = new HashSet<Order>();
        }

        public int PartyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public char Type { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
