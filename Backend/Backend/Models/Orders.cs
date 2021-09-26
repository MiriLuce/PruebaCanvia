using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnicaAPI.Models
{
    public class Orders
    {
        public int orderId { get; set; }
        public int customerId { get; set; }
        public Customers customer { get; set; }
        public String orderStatus { get; set; }
        public String requiredDateStr { get; set; }
        public String shippedDateStr { get; set; }
        public DateTime requiredDate { get; set; }
        public DateTime shippedDate { get; set; }
        public decimal subtotalPrice { get; set; }
        public decimal taxes { get; set; }
        public decimal totalPrice { get; set; }

        public void setFormatDate()
        {
            shippedDate = DateTime.Parse(shippedDateStr);
        }
        public void getFormatDate()
        {
            requiredDateStr = requiredDate.ToString("dd/MM/yyyy");
            shippedDateStr = shippedDate.ToString("dd/MM/yyyy");
        }
    }
}
