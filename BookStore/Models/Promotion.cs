using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Promotion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Promotion can be either in percent or in fixed amount
        public decimal? Percent {  get; set; }
        public decimal? Amount { get; set; }
        // Navigation 
        public int BookId { get; set; }
        public Book? Book { get; set; }

        public override string ToString()
        {
            return String.Format("Name - {0}\nDiscount - {1}", Name, Percent ?? Amount);
        }
    }
}
