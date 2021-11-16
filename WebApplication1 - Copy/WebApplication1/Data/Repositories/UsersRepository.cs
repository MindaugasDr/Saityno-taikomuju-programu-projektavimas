using BidToBuy.Data;
using BidToBuy.Data.Dtos.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data.Entities;

namespace WebApplication1.Data.Repositories
{
    public interface IUsersRepository
    {
        Task<IEnumerable<RestUser>> GetAll();
        Task<RestUser> Get(string Id);
        //Task Create(RestUser user);
        //Task Put(RestUser user);
        Task Delete(string Id);
    }

    public class UsersRepository : IUsersRepository
    {
        private DataContext _context;
        private readonly UserManager<RestUser> _userManager;
        public UsersRepository(DataContext context, UserManager<RestUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IEnumerable<RestUser>> GetAll()
        {
           // return await _userManager.
            return await _context.Users.ToListAsync();
            //return new List<User>
            //{
            //new User(){
            //Name = "Naudotojas",
            //PhoneNumber = "860634269",
            //Password = "assoaenjkeeknwenkwefknl"
            //},
            //new User(){
            //Name = "Naudotojas2",
            //PhoneNumber = "860634268",
            //Password = "assoaenjkeeknwenkwefknl"
            //}
            //};
        }

        public async Task<RestUser> Get(string id)
        {
            return await _context.Users.FindAsync(id);
            //return new User()
            //{
            //    Name = "Vardas",
            //    PhoneNumber = "886545657",
            //    Password = "aaaaaaaaaaaaa"
            //};
        }
       
        public async Task Delete(string Id)
        {
            var userToDelete = await _context.Users.FindAsync(Id);
            if (userToDelete == null)
                throw new NullReferenceException();
            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
