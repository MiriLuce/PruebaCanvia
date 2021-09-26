using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnicaAPI.Models
{
    public class OrderItems
    {
        public int orderId { get; set; }
        public int orderItemId { get; set; }
        public int bookId { get; set; }
        public Books book { get; set; }
        public decimal quantity { get; set; }
        public decimal total { get; set; }
    }
}
