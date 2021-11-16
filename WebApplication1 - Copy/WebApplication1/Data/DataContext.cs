using BidToBuy.Data.Dtos.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Data.Entities;


namespace BidToBuy.Data
{
    public class DataContext : IdentityDbContext<RestUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

            
        }
        public DbSet<Item> Items { get; set; }
        //public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Rating> Ratings { get; set; }



        //public virtual System.Threading.Tasks.Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default);

        //public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //   // System.Threading.SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default);
        //}
        //public DbSet<Item> items { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //public Task<int> SaveChangesAsyc(CancellationToken cancellationToken = default)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
