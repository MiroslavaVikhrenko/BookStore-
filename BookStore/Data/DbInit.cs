using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data
{
    public class DbInit
    {
        public void Init(ApplicationContext context)
        {
            if (!context.Authors.Any())
            {
                context.Authors.AddRange(new Author[]
                {
                    new Author {Name = "Haruki Murakami" },
                    new Author {Name = "Natsume Soseki"},
                    new Author {Name = "Kawabata Yasunari"},
                    new Author {Name = "Ryu Murakami"},
                    new Author {Name = "Banana Yoshimoto"}
                });
                context.SaveChanges();
            }
        }
    }
}
