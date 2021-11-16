using BidToBuy.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data.Entities;

namespace WebApplication1.Data.Repositories
{
    public interface IRatingsRepository
    {
        Task<IEnumerable<Rating>> GetAll();
        Task<Rating> Get(int Id);
        Task Create(Rating rating);
        Task Put(Rating rating);
        Task Delete(Rating rating);
    }

    public class RatingsRepository : IRatingsRepository
    {
        private DataContext _context;
        public RatingsRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Rating>> GetAll()
        {
            return await _context.Ratings.ToListAsync();
            //return new List<Rating>
            //{
            //new Rating(){
            //UserId = 1,
            //ReviewierId = 2,
            //rating = 5
            //},
            //new Rating(){
            //UserId = 2,
            //ReviewierId = 3,
            //rating = 5
            //}
            //};
        }

        public async Task<Rating> Get(int id)
        {
            return await _context.Ratings.FindAsync(id);
        }
        public async Task Create(Rating rating)
        {
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
        }
        public async Task Put(Rating rating)
        {
            _context.Ratings.Update(rating);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Rating rating)
        {
            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();
        }
    }
}
