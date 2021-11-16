using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Data.Entities;

namespace BidToBuy.Data
{
    public interface IDataContext
    {
        DbSet<Item> Items { get; set; }
        //DbSet<User> Users { get; set; }
        DbSet<Image> Images { get; set; }
        DbSet<Event> Events { get; set; }
        DbSet<Rating> Ratings { get; set; }
       // Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
