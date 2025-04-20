using BookStore.Helpers;
using BookStore.Interfaces;
using BookStore.Models;
using BookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    partial class Program
    {
        #region Authors
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
        #endregion

        #region Categories
        static async Task ReviewCategories()
        {
            var allCategories = await _categories.GetAllCategoriesAsync();
            var categories = allCategories.Select(e => new ItemView { Id = e.Id, Value = e.Name }).ToList();
            int result = ItemHelper.MultipleChoice(true, categories, true);
            if (result != 0)
            {
                var currentCategory = await _categories.GetCategoryAsync(result);
                await CategoryInfo(currentCategory);
            }
        }
        static async Task CategoryInfo(Category currentCategory)
        {
            int result = ItemHelper.MultipleChoice(true, new List<ItemView>
            {
                new ItemView { Id = 1, Value = "Browse books" },
                new ItemView { Id = 2, Value = "Edit category" },
                new ItemView { Id = 3, Value = "Delete category" },
            },
            isMenu: true, message: String.Format("{0}\n", currentCategory), startY: 5, optionsPerLine: 1);

            switch (result)
            {
                case 1:
                    {
                        // calling method to display books of this category
                        break;
                    }
                case 2:
                    {
                        await EditCategory(currentCategory);
                        Console.ReadLine();
                        break;
                    }
                case 3:
                    {
                        await RemoveCategory(currentCategory);
                        Console.ReadLine();
                        break;
                    }
            }
            await ReviewCategories();
        }

        static async Task AddCategory()
        {
            string categoryName = InputHelper.GetString("category 'Name'");
            await _categories.AddCategoryAsync(new Category
            {
                Name = categoryName,
            });
            Console.WriteLine("Category successfully added");
        }

        static async Task EditCategory(Category currentCategory)
        {
            Console.WriteLine("Changing: {0}", currentCategory.Name);
            currentCategory.Name = InputHelper.GetString("category name");
            await _categories.EditCategoryAsync(currentCategory);
            Console.WriteLine("Category successfully changed.\r\n");
        }

        static async Task RemoveCategory(Category currentCategory)
        {
            int result = ItemHelper.MultipleChoice(true, new List<ItemView>
            {
                new ItemView {Id = 1, Value = "Yes" },
                new ItemView {Id = 2, Value = "No" },
            }, message: String.Format("[Are you sure you want to delete the category {0} ?]\n", currentCategory.Name), startY: 2);

            if (result == 1)
            {
                await _categories.DeleteCategoryAsync(currentCategory);
                Console.WriteLine("The category was successfully deleted.");
            }
            else
            {
                Console.WriteLine("Press any key to continue...");
            }
        }

        //static async Task SearchCategories()
        //{
        //    string categoryName = InputHelper.GetString("category name");
        //    var currentCategories = await _categories.GetAllCategoriesByNameAsync(categoryName);
        //    if (currentCategories.Count() > 0)
        //    {
        //        var categories = currentCategories.Select(e => new ItemView { Id = e.Id, Value = e.Name}).ToList();
        //        int result = ItemHelper.MultipleChoice(true, categories, true);
        //        if ( result != 0)
        //        {
        //            var currentCategory = await _categories.GetCategoryAsync(result);
        //            await CategoryInfo(currentCategory);
        //        }
        //        else
        //        {
        //            Console.WriteLine("No categories found by this name");
        //        }
        //    }
        //}

        #endregion

        #region Books

        static async Task ReviewBooks(ICollection<Book>? authorBooks = null)
        {
            var allBooks = authorBooks is null ? await _books.GetAllBooksAsync() : authorBooks;
            var books = allBooks.Select(e => new ItemView {Id = e.Id, Value = e.Title}).ToList();
            int result = ItemHelper.MultipleChoice(true, books, true);

            if (result != 0)
            {
                var currentBook = await _books.GetBookWithCategoryAndAuthorsAsync(result);
                await BookInfo(currentBook);
            }
        }

        static async Task BookInfo(Book currentBook)
        {
            int result = ItemHelper.MultipleChoice(true, new List<ItemView>
            {
                new ItemView {Id = 1, Value = "Browse authors"},
                new ItemView {Id = 2, Value = "Edit book" },
                new ItemView {Id = 3, Value = "Delete book" },
                new ItemView {Id = 4, Value = "Promotion Info" }
            },
            isMenu: true, message: String.Format("[{0}]\n", currentBook), startY: 10, optionsPerLine: 1);

            switch (result)
            {
                case 1:
                    {
                        await BrowseAuthors(currentBook);
                        break;
                    }
                case 2:
                    {
                        await EditBook(currentBook);
                        Console.ReadLine();
                        break;
                    }
                case 3:
                    {
                        // calling promotions
                        Console.ReadLine();
                        break;
                    }
                case 4:
                    {
                        await EditBook(currentBook);
                        Console.ReadLine();
                        break;
                    }
            }
            await ReviewBooks();
        }

        static async Task<Book> CreateOrUpdateBook(Book? currentBook = null)
        {
            if (currentBook is not null)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("When editting, all fields are changed");
                Console.ResetColor();
            }
            var allCategories = await _categories.GetAllCategoriesAsync();
            var allAuthors = await _authors.GetAllAuthorsAsync();

            string bookName = InputHelper.GetString("book name");
            string bookDesc = InputHelper.GetString("book description");
            decimal bookPrice = InputHelper.GetDecimal("book price");

            // saving input history as display gets cleared every time we choose an option
            string buffer = String.Format("Enter book 'Name': {0}\nEnter book 'Description': {1}\nEnter book 'Price': {2}\nChoose 'Category':", 
                bookName, bookDesc, bookPrice);

            var categories = allCategories.Select(e => new ItemView { Id = e.Id, Value = e.Name}).ToList();
            int categoryId = ItemHelper.MultipleChoice(true, categories, message: buffer, startY: 10);

            buffer += String.Format("Category with id: {0}\nChoose author:", categoryId);

            var authors = allAuthors.Select(e => new ItemView {Id = e.Id, Value = e.Name}).ToList();
            List<Author> selectedAuthors = new List<Author>(1);
            int authorId = -1;
            while (authorId != 0)
            {
                // passing new collection so that we do not change original collection
                authorId = ItemHelper.MultipleChoice(true, new List<ItemView>(authors), true, message: buffer, startY: 10);
                if (authorId != 0)
                {
                    selectedAuthors.Add(new Author { Id = authorId });
                    authors = authors.Where(e => e.Id == authorId).ToList();
                }
            }
            return new Book
            {
                Id = currentBook?.Id ?? 0,
                Title = bookName,
                Description = bookDesc,
                Price = bookPrice,
                CategoryId = categoryId,
                Authors = selectedAuthors,
            };

        }
        static async Task AddBook()
        {
            await _books.AddBookAsync(await CreateOrUpdateBook());
            Console.WriteLine("Book successfully added");
        }
        static async Task EditBook(Book currentBook)
        {
            await _books.EditBookAsync(await CreateOrUpdateBook());
            Console.WriteLine("Book successfully changed");
        }
        static async Task RemoveBook(Book currentBook)
        {
            int result = ItemHelper.MultipleChoice(true, new List<ItemView>
            {
                new ItemView { Id = 1, Value = "Yes" },
                new ItemView { Id = 2, Value = "No" },
            }, message: String.Format("[Are you sure you want to delete this book {0} ?\n", currentBook.Title), startY: 2);

            if (result == 1)
            {
                await _books.DeleteBookAsync(currentBook);
                Console.WriteLine("The book has been successfully deleted.");
            }
            else
            {
                Console.WriteLine("Press any key to continue...");
            }

        }
        static async Task BrowseAuthors(Book currentBook)
        {
            var authors = currentBook.Authors.Select(e => new ItemView { Id = e.Id, Value = e.Name}).ToList();
            if (authors.Count() > 0)
            {
                int result = ItemHelper.MultipleChoice(true, authors, true);
                if (result !=0)
                {
                    var currentAuthor = await _authors.GetAuthorAsync(result);
                    await AuthorInfo(currentAuthor);
                }
                else
                {
                    Console.WriteLine("This book has no added authors");
                    Console.ReadLine();
                }
            }
        }

        static async Task SeacrhBook()
        {
            string bookName = InputHelper.GetString("book title");
            var currentBooks = await _books.GetBooksByNameAsync(bookName);
            if (currentBooks.Count() > 0)
            {
                var books = currentBooks.Select(e => new ItemView
                {
                    Id = e.Id,
                    Value = e.Title
                }).ToList();

                int result = ItemHelper.MultipleChoice(true, books, true);

                if (result != 0)
                {
                    await BookInfo(await _books.GetBookAsync(result));
                }
            }
            else
            {
                Console.WriteLine("No books were found by this name");
            }
        }

        #endregion
    }
}
