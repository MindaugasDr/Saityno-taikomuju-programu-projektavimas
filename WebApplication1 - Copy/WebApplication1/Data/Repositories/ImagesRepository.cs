using BidToBuy.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data.Entities;
//using static System.Net.Mime.MediaTypeNames;

namespace WebApplication1.Data.Repositories
{
    public interface IImagesRepository
    {
        Task<IEnumerable<Image>> GetAll(int itemId);
        Task<Image> Get(int itemId, int imageId);
        Task Create(Image image);
        Task Put(Image image);
        Task Delete(Image image);
    }
    public class ImagesRepository: IImagesRepository
    {
        private DataContext _context;
        public ImagesRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Image>> GetAll(int itemId)
        {
            return await _context.Images.Where(o => o.Item_id == itemId).ToListAsync();
           // return await _context.Images.ToListAsync();
            //return new List<Image>
            //{
            //new Image(){
            //Url = "aaaaaa/1"
            //},
            //new Image(){
            //Url = "aaaaaaaa/2"
            //}
        //}
        }

        public async Task<Image> Get(int itemId, int imageId)
        {
            return await _context.Images.FirstOrDefaultAsync(o=>o.Image_id == imageId && itemId == o.Item_id);
            //return new Image()
            //{
            //    Url = "aaaaaa/1"
            //};
    }
        public async Task Create( Image image)
        {
            _context.Images.Add(image);
            await _context.SaveChangesAsync();
            //return new Image()
            //{
            //    Url = "aaaaaa/3"
            //};
        }
        public async Task Put(Image image)
        {
            //var imageToUpdate = await _context.Users.FindAsync(image.Image_id);
            //if (imageToUpdate == null)
            //    throw new NullReferenceException();
            _context.Images.Update(image);
            await _context.SaveChangesAsync();

        }
        public async Task Delete(Image image)
        {
            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
        }
    }
}
