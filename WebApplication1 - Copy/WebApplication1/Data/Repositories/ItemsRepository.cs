using BidToBuy.Data;
using BidToBuy.Data.Dtos.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data.Entities;

namespace WebApplication1.Data.Repositories
{
    public interface IItemsRepository
    {
        Task<IEnumerable<Item>> GetAll();
        Task<Item> Get(int Id);
        Task Create(Item item);
        Task Put(Item item);
        Task Delete(int Id);
    }

    public class ItemsRepository:IItemsRepository
    {
        private DataContext _context;
        public ItemsRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Item>> GetAll()
        {
            return await _context.Items.ToListAsync();

            //return new List<Item>
            //{
            //new Item(){
            //Item_name = "daiktas",
            //Item_description = "Aprasymas",
            //CreationDate = DateTime.ParseExact("2021-09-05 22:12","yyyy-MM-dd HH:mm",null)// Convert.ToDateTime("02/23/2001")
            //},
            //new Item(){
            //Item_name = "daiktas2",
            //Item_description = "Kitas Aprasymas",
            //CreationDate =  DateTime.ParseExact("2021-09-29 22:12","yyyy-MM-dd HH:mm",null)
            //}
            //};
        }
        //public async Task<RestUser> GetUser()
        public async Task<Item> Get(int id)
        {/////
            return await _context.Items.FindAsync(id);
            ////
            //return new Item()
            //{
            //    Name = "daiktasVienas",
            //    Description = "Aprasymas",
            //    CreationDate = DateTime.ParseExact("2021-09-05 22:12", "yyyy-MM-dd HH:mm", null)
            //};
        }
        public async Task Create(Item item)
        {
            item.CreationDate = DateTime.Now;
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
           // var same_item = await _context.Items.FindAsync(item.CreationDate);

            //Event eventt = new Event ("Created",item.CreationDate,0,item.User_id,item.Item_id);
            //_context.Events.Add(eventt);
           // await _context.SaveChangesAsync();
            //////
            //return new Item()
            //{
            //    Name = "daiktasVienas",
            //    Description = "Aprasymas",
            //    CreationDate = DateTime.ParseExact("2021-09-05 22:12", "yyyy-MM-dd HH:mm", null)

            //};
        }
        public async Task Put(Item item)
        {
            var itemToUpdate = await _context.Items.FindAsync(item.Item_id);
            if (itemToUpdate == null)
                throw new NullReferenceException();
            itemToUpdate.Item_description = item.Item_description;
            await _context.SaveChangesAsync();
            ////
            //return new Item()
            //{
            //    Name = "daiktasVienas",
            //    Description = "NaujasAprasymas",
            //    CreationDate = DateTime.ParseExact("2021-09-05 22:12", "yyyy-MM-dd HH:mm", null)

            //};
        }
        public async Task Delete(int Id)
        {
            var itemToDelete = await _context.Items.FindAsync(Id);
            if (itemToDelete == null)
                throw new NullReferenceException();
            _context.Items.Remove(itemToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
