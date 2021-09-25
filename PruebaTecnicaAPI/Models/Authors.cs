using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnicaAPI.Models
{
    public class Authors
    {
        public int authorId { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String country { get; set; }
        public String birthdateStr { get; set; }
        public String deathDateStr { get; set; }
        public DateTime birthdate { get; set; }
        public DateTime deathDate { get; set; }

        public void setFormatDate() 
        {
            birthdate = DateTime.Parse(birthdateStr);
            deathDate = DateTime.Parse(deathDateStr);
        }
        public void getFormatDate()
        {
            birthdateStr = birthdate.ToString("dd/MM/yyyy");
            deathDateStr = deathDate.ToString("dd/MM/yyyy");
        }
    }
}
