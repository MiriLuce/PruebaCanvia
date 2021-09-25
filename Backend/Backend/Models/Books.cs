using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnicaAPI.Models
{
    public class Books
    {
        public int bookId { get; set; }
        public int authorId { get; set; }
        public String title { get; set; }
        public String genre { get; set; }
        public decimal stock { get; set; }
        public decimal price { get; set; }
        public String publicationDateStr { get; set; }
        public DateTime publicationDate { get; set; }

        public void setFormatDate()
        {
            publicationDate = DateTime.Parse(publicationDateStr);
        }
        public void getFormatDate()
        {
            publicationDateStr = publicationDate.ToString("dd/MM/yyyy");
        }
    }
}
