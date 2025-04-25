using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repositories
{
    public class OrderRepository : IOrder
    {
        public async  Task AddOrderAsync(Order order)
        {
            using (ApplicationContext context = Program.DbContext())
            {
                await context.Orders.AddAsync(order);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteOrderAsync(Order order)
        {
            using (ApplicationContext context = Program.DbContext())
            {
                context.Orders.Remove(order);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            using (ApplicationContext context = Program.DbContext())
            {
                return await context.Orders.ToListAsync();
            }
        }

        public async Task<IEnumerable<Order>> GetAllOrdersByAddressAsync(string address)
        {
            using (ApplicationContext context = Program.DbContext())
            {
                return await context.Orders.Where(e => e.Address.Contains(address)).ToListAsync();
            }
        }

        public async Task<IEnumerable<Order>> GetAllOrdersByNameAsync(string name)
        {
            using (ApplicationContext context = Program.DbContext())
            {
                return await context.Orders.Where(e => e.CustomerName.Contains(name)).ToListAsync();
            }
        }

        public async Task<Order> GetOrderAsync(int id)
        {
            using (ApplicationContext context = Program.DbContext())
            {
                return await context.Orders.FirstOrDefaultAsync(e => e.Id == id);
            }
        }

        public async Task<Order> GetOrderWithOrderLinesAndBooksAsync(int id)
        {
            using (ApplicationContext context = Program.DbContext())
            {
                return await context.Orders.Include(e => e.Lines).ThenInclude(e => e.Book).FirstOrDefaultAsync(e => e.Id == id);
            }
        }

        public async Task UpdateOrderAsync(Order order)
        {
            using (ApplicationContext context = Program.DbContext())
            {
                var currentOrder = await context.Orders.Include(e => e.Lines).FirstOrDefaultAsync(e => e.Id == order.Id);
                if (currentOrder is not null)
                {
                    currentOrder.CustomerName = order.CustomerName;
                    currentOrder.Address = order.Address;
                    currentOrder.City = order.City;
                    currentOrder.Shipped = order.Shipped;
                    currentOrder.Lines = new List<OrderLine>();
                    foreach (OrderLine line in order.Lines)
                    {
                        currentOrder.Lines.Add(line);
                    }
                    context.Orders.Update(currentOrder);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
