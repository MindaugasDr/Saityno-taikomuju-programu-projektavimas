using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data.Dtos.Images;
using WebApplication1.Data.Entities;
using WebApplication1.Data.Repositories;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using BidToBuy.Auth.Model;
using Microsoft.AspNetCore.Http;


namespace WebApplication1.Controllers
{

    [ApiController]
    [Route("api/items/{itemId}/images")]
    public class ImagesController : ControllerBase
    {
        private readonly IImagesRepository _imagesRepository;
        private readonly IMapper _mapper;
        private readonly IItemsRepository _itemsRepository;
        //private readonly IWebHostEnvironment _env;
        public static IWebHostEnvironment _env;
        private readonly IAuthorizationService _authorizationService;
        public ImagesController(IImagesRepository imagesRepository, IMapper mapper,IItemsRepository itemsRepository, IWebHostEnvironment env, IAuthorizationService authorizationService)
        {
            _imagesRepository = imagesRepository;
            _mapper = mapper;
            _itemsRepository = itemsRepository;
            _env = env;
            _authorizationService =  authorizationService;
        }

        [HttpGet]
        public async Task<IEnumerable<ImageDto>> GetAll(int itemId)
        {
            var items = await _imagesRepository.GetAll(itemId);
            return (await _imagesRepository.GetAll(itemId)).Select(o => new ImageDto(o.Image_id, o.Image_url, o.Item_id));
        }
        [HttpGet("{imageId}")]
        public async Task<ActionResult<Image>> Get(int itemId, int imageId)
        {
            var item = await _itemsRepository.Get(itemId);
            if (item == null) return NotFound($"Couldn't find an item with id {itemId}");
            var image = await _imagesRepository.Get(itemId,imageId);
            if (image == null) return NotFound($"Image with id '{imageId}' not found!");
            return Ok(_mapper.Map<ImageDto>(image));
        }
        //[HttpPost]
        //[Authorize(Roles = RestUserRoles.RegularUser)]
        //public async Task<ActionResult<ImageDto>> Post(int itemId, CreateImageDto imageDto)
        //{
        //    var item = await _itemsRepository.Get(itemId);
        //    if (item == null) return NotFound($"Couldn't find an item with id {itemId}");
        //    var authorizationResult = await _authorizationService.AuthorizeAsync(User, item, PolicyNames.SameUser);
        //    if (!authorizationResult.Succeeded)
        //    {
        //        return Forbid();
        //    }
        //    var image = _mapper.Map<Image>(imageDto);
        //    image.Item_id = itemId;
        //    await _imagesRepository.Create(image);
        //    return Created($"/api/items/{itemId}/images/{image.Image_id}", _mapper.Map<ImageDto>(image));

        //}

        //[HttpPut("{imageId}")]
        //[Authorize(Roles = RestUserRoles.RegularUser)]
        //public async Task<ActionResult<ImageDto>> Put(int itemId, int imageId, EditImageDto imageDto)
        //{

        //    var item = await _itemsRepository.Get(itemId);
        //    if (item == null) return NotFound($"Couldn't find an item with id {itemId}");
        //    var authorizationResult = await _authorizationService.AuthorizeAsync(User, item, PolicyNames.SameUser);
        //    if (!authorizationResult.Succeeded)
        //    {
        //        return Forbid();
        //    }
        //    //if()
        //    //var oldImage = await _imagesRepository.Get(itemId, imageId);
        //    //if (oldImage == null)
        //    //return NotFound();
        //    //_mapper.Map(imageDto, oldImage);
        //    //await _imagesRepository.Put(oldImage);

        //    //// var image = _mapper.Map<Image>(imageDto);
        //    var image = await _imagesRepository.Get(itemId,imageId);
        //    if (image == null) return NotFound($"Image with id '{imageId}' not found!");
        //    _mapper.Map(imageDto, image);
        //    //image.Description = imageDto.Description;
        //    await _imagesRepository.Put(image);

        //    return Ok(_mapper.Map<ImageDto>(image));

        //}


        //[Authorize(Roles = RestUserRoles.RegularUser)]
        //[HttpDelete("{imageId}")]
        //public async Task<ActionResult<ImageDto>> Delete(int itemId,int imageId)
        //{
        //    var item = await _itemsRepository.Get(itemId);
        //    if (item == null) return NotFound($"Couldn't find an item with id {itemId}");
        //    // var image = _mapper.Map<Image>(imageDto);
        //    var authorizationResult = await _authorizationService.AuthorizeAsync(User, item, PolicyNames.SameUser);
        //    if (!authorizationResult.Succeeded)
        //    {
        //        return Forbid();
        //    }
        //    var image = await _imagesRepository.Get(itemId,imageId);
        //    if (image  == null) return NotFound($"Image with id '{imageId}' not found!");

        //    await _imagesRepository.Delete(image);

        //    return NoContent();

        //}

        //public class FileUpload {
        //    public IFormFile files{
        //        get;
        //        set;   
        //    }
        //}

        ////[Route("/{imageId}/SaveFile")]
        //[HttpPost("SaveFile")]
        //public async Task<ActionResult<string>> SaveFile(int itemId, [FromForm] FileUpload objFile)
        //{
        //    var item = await _itemsRepository.Get(itemId);
        //    if (item == null) return NotFound($"Couldn't find an item with id {itemId}");
        //    Image image = null;
        //    //var authorizationResult = await _authorizationService.AuthorizeAsync(User, item, PolicyNames.SameUser);
        //    //if (!authorizationResult.Succeeded)
        //    //{
        //    //    return Forbid();
        //    //}
        //    //var image = _mapper.Map<Image>(imageDto);
        //    // await _imagesRepository.Create(image);
        //    // return Created($"/api/items/{itemId}/images/{image.Image_id}", _mapper.Map<ImageDto>(image));
        //    if (objFile.files.Length > 0)
        //    {
        //        try
        //        {

        //            if (!Directory.Exists(_env.WebRootPath + "\\Uploads\\"))
        //            {
        //                Directory.CreateDirectory(_env.WebRootPath + "\\Uploads\\");
        //            }
        //            int id = 1;
        //            var images = await _imagesRepository.GetAll(itemId);
        //            if (images != null)
        //            {
        //                id = images.ToList().Select(e => e.Image_id).Max();
        //            }
        //            using (FileStream fileStream = System.IO.File.Create(_env.WebRootPath + "\\Uploads\\" + "I" + itemId.ToString() + "IM" + id))
        //            {
        //                objFile.files.CopyTo(fileStream);
        //                fileStream.Flush();
        //            }
        //            image.Image_url = "I" + itemId.ToString() + "IM" + id;
        //            image.Item_id = itemId;
        //            await _imagesRepository.Create(image);
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    else return NotFound();
        //    return Created($"/api/items/{itemId}/images/{image.Image_id}", _mapper.Map<ImageDto>(image));

        //}

        [HttpPost("SaveFile")]
        [Authorize(Roles = RestUserRoles.RegularUser)]
        public async Task<ActionResult<string>> SaveFile(int itemId)
        {
            var item = await _itemsRepository.Get(itemId);
            if (item == null) return NotFound($"Couldn't find an item with id {itemId}");
            int id = 1;
            var images = await _imagesRepository.GetAll(itemId);
            if (images != null)
            {
                if (images.Count() == 0) {
                    id = 1;
                }
                else id = images.ToList().Select(e => e.Image_id).Max();
            }
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, item, PolicyNames.SameUser);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            try
            {
                var httpRequest = Request.Form;

                var file = httpRequest.Files[0];
                var fullpath = _env.WebRootPath + "/Upload/" + "I" + itemId.ToString() + "IM" + id + ".png";
                using (var stream = new FileStream(fullpath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                
                CreateImageDto imageDto = new CreateImageDto("I" + itemId.ToString() + "IM" + id + ".png");
                var image = _mapper.Map<Image>(imageDto);
                image.Item_id = itemId;
                await _imagesRepository.Create(image);
                //return Ok($"File saved in path: '{fullpath}'");
                return Created($"/api/items/{itemId}/images/{image.Image_id}", _mapper.Map<ImageDto>(image));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPut("{imageId}")]
        [Authorize(Roles = RestUserRoles.RegularUser)]
        public async Task<ActionResult<ImageDto>> Put(int itemId, int imageId)
        {

            var item = await _itemsRepository.Get(itemId);
            if (item == null) return NotFound($"Couldn't find an item with id {itemId}");
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, item, PolicyNames.SameUser);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            var image = await _imagesRepository.Get(itemId, imageId);
            if (image == null) return NotFound($"Image with id '{imageId}' not found!");

            await _imagesRepository.Put(image);

            try
            {
                var httpRequest = Request.Form;

                var file = httpRequest.Files[0];
                var fullpath = _env.WebRootPath + "/Upload/" + image.Image_url;
                using (var stream = new FileStream(fullpath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return Ok("Image updated!");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        
        [HttpDelete("{imageId}")]
        [Authorize(Roles = RestUserRoles.RegularUser)]
        public async Task<ActionResult<ImageDto>> Delete(int itemId, int imageId)
        {
            var item = await _itemsRepository.Get(itemId);
            if (item == null) return NotFound($"Couldn't find an item with id {itemId}");
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, item, PolicyNames.SameUser);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }
            var image = await _imagesRepository.Get(itemId, imageId);
            if (image == null) return NotFound($"Image with id '{imageId}' not found!");

            await _imagesRepository.Delete(image);
            string path = _env.WebRootPath + "/Upload/" + image.Image_url;
            FileInfo file = new FileInfo(path);
            if (file.Exists)//check file exsit or not  
            {
                file.Delete();
            }
            return NoContent();

        }


    }
}
