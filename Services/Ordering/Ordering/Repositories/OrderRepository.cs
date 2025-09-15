﻿using Microsoft.EntityFrameworkCore;
using Ordering.Data;
using Ordering.Entities;

namespace Ordering.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            var orderList = await _dbContext.Orders
                                    .AsNoTracking()
                                    .Where(o => o.UserName == userName)
                                    .ToListAsync();
            return orderList;
        }

        public async Task AddOutboxMessageAsync(OutboxMessage outboxMessage)
        {
            await _dbContext.outboxMessages.AddAsync(outboxMessage);
            await _dbContext.SaveChangesAsync();
        }
    }
}
