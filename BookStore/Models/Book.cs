using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<Author> Authors { get; set; } // nav prop
        public virtual ICollection<Review> Reviews { get; set; } // nav prop
        public Promotion? Promotion { get; set; } // nav prop
        public Category Category { get; set; } // nav prop

        public override string ToString()
        {
            return String.Format("Title - {0}\nDescription - {1}\nCategory - {2}\nPrice - {3}", Title, Description, Category, Price);
        }
    }
}
