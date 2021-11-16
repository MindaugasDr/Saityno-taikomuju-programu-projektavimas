using BidToBuy.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data.Entities;

namespace WebApplication1.Data.Repositories
{
    public interface IEventsRepository
    {
        Task<IEnumerable<Event>> GetAll();
        Task<Event> Get(int Id);
        Task Create(Event eventt);
        Task Put(Event eventt);
        Task Delete(Event eventt);
    }
    public class EventsRepository : IEventsRepository
    {
        private DataContext _context;
        public EventsRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Event>> GetAll()
        {
            return await _context.Events.ToListAsync();
            //return new List<Event>
            //{
            //new Event(){
            //Action = "Idejo",
            ////Date = DateTime.Today(),
            //CurrentSum = 0
            //},
            //new Event(){
            //Action = "kazka padare",
            //CurrentSum = 10
            //}
            //};
        }

        public async Task<Event> Get(int id)
        {
            return await _context.Events.FindAsync(id);
            //return new Event()
            //{
            //    Action = "kazka padare",
            //    CurrentSum = 10
            //};
        }
        public async Task Create(Event eventt)
        {
            eventt.Date = DateTime.Now;
            _context.Events.Add(eventt);
            await _context.SaveChangesAsync();
            //return new Event()
            //{
            //    Action = "daiktasVienas",
            //    CurrentSum = 5,
            //    User_id = 1,
            //    Item_id = 1
            //};
        }
        public async Task Put(Event eventt)
        {
            _context.Events.Update(eventt);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Event eventt)
        {
            //eventt.Date = DateTime.Now;
            _context.Events.Remove(eventt);
            await _context.SaveChangesAsync();
        }
    }
}
