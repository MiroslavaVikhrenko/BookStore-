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

            if (!context.Books.Any())
            {
                context.Books.AddRange(new Book[]
                {
                    new Book
                    {
                        Title = "Kafka on the Shore",
                        Description = "A surreal and philosophical journey intertwining the lives of a runaway teen and an elderly man who talks to cats, blurring the lines between reality and dreams.",
                        Price = 50.0m,
                        Authors = new List<Author>()
                        {
                            context.Authors.FirstOrDefault(e => e.Name.Equals("Haruki Murakami"))
                        }
                    },
                    new Book
                    {
                        Title = "Kitchen",
                        Description = "A quietly powerful story about love, loss, and healing, centered around a young woman who finds comfort in cooking after the death of her grandmother.",
                        Price = 40.0m,
                        Authors = new List<Author>()
                        {
                            context.Authors.FirstOrDefault(e => e.Name.Equals("Banana Yoshimoto"))
                        }
                    },
                    new Book
                    {
                        Title = "Kokoro",
                        Description = "A poignant exploration of isolation, guilt, and the changing values of Meiji-era Japan, told through the complex relationship between a young man and a mysterious older mentor.",
                        Price = 60.0m,
                        Authors = new List<Author>()
                        {
                            context.Authors.FirstOrDefault(e => e.Name.Equals("Natsume Soseki"))
                        }
                    },
                    new Book
                    {
                        Title = "Snow Country",
                        Description = "A hauntingly lyrical tale of a doomed love affair between a Tokyo intellectual and a provincial geisha, set against the stark beauty of Japan’s snowy mountains.",
                        Price = 70.0m,
                        Authors = new List<Author>()
                        {
                            context.Authors.FirstOrDefault(e => e.Name.Equals("Kawabata Yasunari"))
                        }
                    },
                    new Book
                    {
                        Title = "In the Miso Soup",
                        Description = "A chilling psychological thriller following a Tokyo nightlife guide who suspects his American client may be a dangerous killer lurking in the city’s seedy underworld.",
                        Price = 55.0m,
                        Authors = new List<Author>()
                        {
                            context.Authors.FirstOrDefault(e => e.Name.Equals("Ryu Murakami"))
                        }
                    },
                    new Book
                    {
                        Title = "Almost Transparent Blue",
                        Description = "A raw, hallucinatory portrayal of disaffected youth in 1970s Japan, immersed in a world of sex, drugs, and existential aimlessness.",
                        Price = 45.0m,
                        Authors = new List<Author>()
                        {
                            context.Authors.FirstOrDefault(e => e.Name.Equals("Ryu Murakami"))
                        }
                    }

                });
                context.SaveChanges();
            }

            if (!context.Reviews.Any())
            {
                context.Reviews.AddRange(new Review[]
                {
                    new Review
                    {
                        UserName = "Kenji",
                        UserEmail = "kenji@gmail.com",
                        Comment = "Haunting and beautifully written.",
                        Stars = 4,
                        Book = context.Books.FirstOrDefault(e => e.Title.Equals("Kafka on the Shore"))
                    },
                    new Review
                    {
                        UserName = "Yumi",
                        UserEmail = "yui@gmail.com",
                        Comment = "Couldn’t put it down.",
                        Stars = 5,
                        Book = context.Books.FirstOrDefault(e => e.Title.Equals("Kafka on the Shore"))
                    },
                    new Review
                    {
                        UserName = "Naoko",
                        UserEmail = "naoko@gmail.com",
                        Comment = "Dark, raw, and unforgettable.",
                        Stars = 4,
                        Book = context.Books.FirstOrDefault(e => e.Title.Equals("Almost Transparent Blue"))
                    },
                    new Review
                    {
                        UserName = "Makoto",
                        UserEmail = "makoto@gmail.com",
                        Comment = "Strange, but strangely moving.",
                        Stars = 4,
                        Book = context.Books.FirstOrDefault(e => e.Title.Equals("In the Miso Soup"))
                    },
                    new Review
                    {
                        UserName = "Taro",
                        UserEmail = "taro@gmail.com",
                        Comment = "Poetic and deeply human.",
                        Stars = 5,
                        Book = context.Books.FirstOrDefault(e => e.Title.Equals("Kitchen"))
                    }
                });
                context.SaveChanges();
            }

            if (!context.Promotions.Any())
            {
                context.Promotions.AddRange(new Promotion[]
                {
                    new Promotion
                    {
                        Name = "Summer promotion",
                        Percent = 10.0m,
                        Book = context.Books.FirstOrDefault(e => e.Title.Equals("Kafka on the Shore"))
                    },
                    new Promotion
                    {
                        Name = "Golden week promotion",
                        Amount = 5.0m,
                        Book = context.Books.FirstOrDefault(e => e.Title.Equals("In the Miso Soup"))
                    },
                    new Promotion
                    {
                        Name = "Member promotion",
                        Percent = 5.0m,
                        Book = context.Books.FirstOrDefault(e => e.Title.Equals("Kitchen"))
                    },
                    new Promotion
                    {
                        Name = "First purchase promotion",
                        Amount = 3.0m,
                        Book = context.Books.FirstOrDefault(e => e.Title.Equals("Almost Transparent Blue"))
                    },
                    new Promotion
                    {
                        Name = "Happy customer promotion",
                        Percent = 20.0m,
                        Book = context.Books.FirstOrDefault(e => e.Title.Equals("Snow Country"))
                    }
                });
                context.SaveChanges();
            }
        }
    }
}
