using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repositories
{
    public class OrderRepository : IOrder
    {
        public async  Task AddOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersByAddressAsync(string address)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> GetOrderAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> GetOrderWithOrderLinesAndBooksAsync(int id)
        {
            throw new NotImplementedException();
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
