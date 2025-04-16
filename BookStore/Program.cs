using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Models;
using BookStore.Repositories;

namespace BookStore
{
    //internal class Program
    //{
    //    public static ApplicationContext DbContext() => new ApplicationContextFactory().CreateDbContext(); 
    //    static void Main(string[] args)
    //    {
    //        //using (ApplicationContext db = DbContext())
    //        //{
    //        //    new DbInit().Init(DbContext());

    //        //}

    //    }
    //}
    public class Program
    {
        private static IBook _books;

        public static ApplicationContext DbContext() => new ApplicationContextFactory().CreateDbContext();
        static void Initialize()
        {
            new DbInit().Init(DbContext());
            _books = new BookRepository();
        }

        static async Task Main()
        {
            Initialize();
            var allBooks = await _books.GetAllBooksWithAuthorsAsync();
            //Очищаем консоль от Sql команд
            Console.Clear();
            foreach (Book book in allBooks)
            {
                Console.WriteLine("{0}\nAuthors:\n{1}\n\n", book, String.Join("\n", book.Authors));
            }
        }
    }

}
