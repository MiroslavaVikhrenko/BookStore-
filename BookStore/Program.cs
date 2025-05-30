﻿using BookStore.Data;
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
        private static ICategory _categories;
        private static IPromotion _promotions;

        public static ApplicationContext DbContext() => new ApplicationContextFactory().CreateDbContext();
        static void Initialize()
        {
            new DbInit().Init(DbContext());

            _books = new BookRepository();
            _orders = new OrderRepository();
            _authors = new AuthorRepository();
            _categories = new CategoryRepository();
            _promotions = new PromotionRepository();
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
                        await ReviewBooks();
                        break;
                    case ShopMenu.Authors:
                        await ReviewAuthors();
                        break;
                    case ShopMenu.Orders:
                        await ReviewOrders();
                        break;
                    case ShopMenu.SearchAuthor:
                        await SearchAuthors();
                        break;
                    case ShopMenu.SearchBooks:
                        await SeacrhBooks();
                        break;
                    case ShopMenu.SearchOrders:
                        await SearchOrders();
                        break;
                    case ShopMenu.AddBook:
                        await AddBook();
                        break;
                    case ShopMenu.AddAuthor:
                        await AddAuthor();
                        break;
                    case ShopMenu.AddOrder:
                        await AddOrder();
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
