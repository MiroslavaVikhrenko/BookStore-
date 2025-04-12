using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Interfaces
{
    public interface ICategory
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryAsync(int id);
        Task<Category> GetCategoryWithBooksAsync(int id);

        Task AddCategoryAsync(Category category);
        Task EditCategoryAsync(Category category);
        Task DeleteCategoryAsync(Category category);
    }
}
