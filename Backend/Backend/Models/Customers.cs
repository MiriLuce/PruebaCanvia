using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnicaAPI.Models
{
    public class Customers
    {
        public int customerId { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String phone { get; set; }
        public String email { get; set; }
        public String addressStreet { get; set; }
    }
}
