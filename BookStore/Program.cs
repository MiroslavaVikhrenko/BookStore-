using BookStore.Data;

namespace BookStore
{
    internal class Program
    {
        public static ApplicationContext DbContext() => new ApplicationContextFactory().CreateDbContext(); 
        static void Main(string[] args)
        {
            using (ApplicationContext db = DbContext())
            {
                new DbInit().Init(DbContext());
            }
        }
    }
}
