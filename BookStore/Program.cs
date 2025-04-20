using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Models;
using BookStore.Repositories;

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

        static async Task Main()
        {
            Initialize();
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
