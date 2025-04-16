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
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(new Category[]
                {
            new Category
            {
                Name = "Dreamlike & Surreal"
            },
            new Category
            {
                Name = "Love & Loneliness"
            },
            new Category
            {
                Name = "Psychological & Dark"
            }
                });
                context.SaveChanges();
            }
            if (!context.Authors.Any())
            {
                context.Authors.AddRange(new Author[]
                {
            new Author { Name = "Jess Kidd"},
            new Author { Name = "Martha McPhee"},
            new Author { Name = "Megan Miranda"},
            new Author { Name = "Helen Phillips"},
            new Author { Name = "Karen Kingsbury"}
                });
                context.SaveChanges();
            }
            if (!context.Books.Any())
            {
                Book theNightShip = new Book
                {
                    Title = "The Night Ship",
                    Description = "Based on a real-life event, an epic historical novel from the award-winning author of Things in Jars.",
                    Price = 70,
                    Authors = new List<Author>()
            {
                context.Authors.FirstOrDefault(e => e.Name.Equals("Jess Kidd"))
            },
                    Category = context.Categories.FirstOrDefault(e => e.Name.Equals("Dreamlike & Surreal"))
                };

                Book theOnlySurvivors = new Book
                {
                    Title = "The Only Survivors",
                    Description = "Thrilling mystery about a group of former classmates who reunite to mark the tenth anniversary of a tragic accident—only" +
                    " to have one of the survivors disappear, casting fear and suspicion on the original tragedy.",
                    Price = 59,
                    Authors = new List<Author>()
            {
                context.Authors.FirstOrDefault(e=>e.Name.Equals("Megan Miranda")),
                context.Authors.FirstOrDefault(e=>e.Name.Equals("Helen Phillips"))
            },
                    Category = context.Categories.FirstOrDefault(e => e.Name.Equals("Love & Loneliness"))
                };

                context.Books.AddRange(new Book[]
                {
           theNightShip,
           theOnlySurvivors
                });

                context.SaveChanges();
            }

            if (!context.Reviews.Any())
            {
                context.Reviews.AddRange
                    (
                    new Review
                    {
                        UserName = "Alex",
                        UserEmail = "alex@gmail.com",
                        Comment = "Good book!",
                        Stars = 5,
                        Book = context.Books.FirstOrDefault(e => e.Title.Equals("The Only Survivors"))
                    },
                    new Review
                    {
                        UserName = "Marry",
                        UserEmail = "marry@gmail.com",
                        Comment = "Nice to read.",
                        Stars = 4,
                        Book = context.Books.FirstOrDefault(e => e.Title.Equals("The Only Survivors"))
                    },
                    new Review
                    {
                        UserName = "John",
                        UserEmail = "john@gmail.com",
                        Comment = "Best thing I've ever read!",
                        Stars = 5,
                        Book = context.Books.FirstOrDefault(e => e.Title.Equals("The Night Ship"))
                    }
                    );
                context.SaveChanges();
            }
            if (!context.Promotions.Any())
            {
                context.Promotions.AddRange
                    (
                    new Promotion
                    {
                        Name = "Christmas Eve discount!",
                        Percent = 15,
                        Book = context.Books.FirstOrDefault(e => e.Title.Equals("The Night Ship"))
                    },
                     new Promotion
                     {
                         Name = "Christmas Eve discount!",
                         Amount = 25,
                         Book = context.Books.FirstOrDefault(e => e.Title.Equals("The Only Survivors"))
                     }
                    );
                context.SaveChanges();
            }

        }
    }
}
