using BookStore.Data;
using BookStore.Helpers;
using BookStore.Interfaces;
using BookStore.Models;
using BookStore.Repositories;
using BookStore.ViewModels;

namespace BookStore
{
    partial class Program
    {
        enum ShopMenu
        {
            Books, Authors, Categories, Orders, SearchAuthor, SearchBooks, SearchCategories, SearchOrders, AddBook, AddAuthor, AddCategory, AddOrder, Exit
        }
        private static IBook _books;
        private static IOrder _orders;
        private static IAuthor _authors;

        public static ApplicationContext DbContext() => new ApplicationContextFactory().CreateDbContext();
        static void Initialize()
        {
            new DbInit().Init(DbContext());

            _books = new BookRepository();
            _orders = new OrderRepository();
            _authors = new AuthorRepository();
        }

        // Authors

        static async Task ReviewAuthors()
        {
            var allAuthors = await _authors.GetAllAuthorsAsync();
            var authors = allAuthors.Select(e => new ItemView { Id = e.Id, Value = e.Name }).ToList();
            int result = ItemHelper.MultipleChoice(true, authors, true);
            if (result != 0)
            {
                var currentAuthor = await _authors.GetAuthorAsync(result);
                await AuthorInfo(currentAuthor);
            }
        }

        static async Task AuthorInfo(Author currentAuthor)
        {
            int result = ItemHelper.MultipleChoice(true, new List<ItemView>
            {
                new ItemView {Id = 1, Value = "Browse books"},
                new ItemView {Id = 2, Value = "Edit author"},
                new ItemView {Id = 3, Value = "Delete author"}
            },
            isMenu: true, message: String.Format("{0}\n", currentAuthor), startY: 5, optionsPerLine: 1);

            switch (result)
            {
                case 1:
                    {
                        // call method to display all books by this author
                        break;
                    }
                case 2:
                    {
                        await EditAuthor(currentAuthor);
                        Console.ReadLine();
                        break;
                    }
                case 3:
                    {
                        await RemoveAuthor(currentAuthor);
                        Console.ReadLine();
                        break;
                    }
            }

            await ReviewAuthors();
        }

        static async Task AddAuthor()
        {
            string authorName = InputHelper.GetString("author 'Name' with 'Surname'");
            await _authors.AddAuthorAsync(new Author
            {
                Name = authorName,
            });
            Console.WriteLine("Author successfully added");
        }

        static async Task EditAuthor(Author currentAuthor)
        {
            Console.WriteLine("Changing: {0}", currentAuthor.Name);
            currentAuthor.Name = InputHelper.GetString("author 'Name' with 'Surname'");
            await _authors.EditAuthorAsync(currentAuthor);
            Console.WriteLine("Author successfully changed");
        }

        static async Task RemoveAuthor(Author currentAuthor)
        {
            int result = ItemHelper.MultipleChoice(true, new List<ItemView>
            {
                new ItemView { Id = 1, Value = "Yes"},
                new ItemView { Id = 2, Value = "No" },
            }, message: String.Format("[Are you sure you want to delete the author {0} ?]\n", currentAuthor.Name), startY: 2);

            if (result == 1)
            {
                await _authors.DeleteAuthorAsync(currentAuthor);
                Console.WriteLine("Author successfully deleted");
            }
            else
            {
                Console.WriteLine("Press any key to continue...");
            }
        }

        static async Task SearchAuthors()
        {
            string authorName = InputHelper.GetString("author name or surname");
            var currentAuthors = await _authors.GetAuthorsByName(authorName);

            if (currentAuthors.Count() > 0)
            {
                var authors = currentAuthors.Select(e => new ItemView { Id = e.Id, Value = e.Name }).ToList();
                int result = ItemHelper.MultipleChoice(true, authors, true);
                if (result != 0)
                {
                    var currentAuthor = await _authors.GetAuthorAsync(result);
                    await AuthorInfo(currentAuthor);
                }
            }
            else
            {
                Console.WriteLine("No authors found by this name.");
            }
        }


        // Menu()

        static async Task Menu()
        {
            int input = new int();

            do
            {
                input = ConsoleHelper.MultipleChoice(true, new ShopMenu());

                switch ((ShopMenu)input)
                {
                    case ShopMenu.Books:
                        break;
                    case ShopMenu.Authors:
                        await ReviewAuthors();
                        break;
                    case ShopMenu.Orders:
                        break;
                    case ShopMenu.SearchAuthor:
                        await SearchAuthors();
                        break;
                    case ShopMenu.SearchBooks:
                        break;
                    case ShopMenu.SearchOrders:
                        break;
                    case ShopMenu.AddBook:
                        break;
                    case ShopMenu.AddAuthor:
                        await AddAuthor();
                        break;
                    case ShopMenu.AddOrder:
                        break;
                    case ShopMenu.Exit:
                        break;
                    default:
                        break;
                }
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadLine();
            } while (ShopMenu.Exit != (ShopMenu)input);
        }

        static async Task Main()
        {
            Initialize();

            await Menu();
            //var allBooks = await _books.GetAllBooksWithAuthorsAsync();
            ////Очищаем консоль от Sql команд
            //Console.Clear();
            //foreach (Book book in allBooks)
            //{
            //    Console.WriteLine("{0}\nAuthors:\n{1}\n\n", book, String.Join("\n", book.Authors));
            //}
        }
    }

}
