using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class OrderLine
    {
        public int Id { get; set; }
        public int BookId { get; set; } // FK
        public int OrderId { get; set; } // FK
        public int Quantity { get; set; }
        public Book Book { get; set; } // nav prop
        public Order Order { get; set; } // nav prop
    }
}
