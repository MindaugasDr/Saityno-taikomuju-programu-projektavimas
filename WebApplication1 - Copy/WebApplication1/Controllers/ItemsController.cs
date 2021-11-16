using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BidToBuy.Auth.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.Dtos.Events;
using WebApplication1.Data.Dtos.Items;
using WebApplication1.Data.Entities;
using WebApplication1.Data.Repositories;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/items")]
    public class ItemsController:ControllerBase
    {
        private readonly IItemsRepository _itemsRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IEventsRepository _eventsRepository;
        private readonly IImagesRepository _imagesRepository;
        public static IWebHostEnvironment _env;
        public ItemsController(IItemsRepository itemsRepository, IMapper mapper, IAuthorizationService authorizationService, IEventsRepository eventsRepository, IImagesRepository imagesRepository, IWebHostEnvironment env)
        {
            _itemsRepository = itemsRepository;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _eventsRepository = eventsRepository;
            _imagesRepository = imagesRepository;
            _env = env;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetAll()
        {
            return (await _itemsRepository.GetAll()).Select(o=>new ItemDto(o.Item_id,o.Item_name, o.Item_description,o.CreationDate,o.User_id));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> Get(int id)
        {
            var item = await _itemsRepository.Get(id);
            if (item == null) return NotFound($"Item with id '{id}' not found!");
            return Ok(_mapper.Map<ItemDto>(item));
        }
        
        [HttpPost]
        [Authorize(Roles = RestUserRoles.RegularUser)]
        public async Task<ActionResult<ItemDto>> Post(CreateItemDto itemDto)
        {
            
            var item = _mapper.Map<Item>(itemDto);
            item.User_id = User.FindFirstValue(CustomClaims.UserId);
            await _itemsRepository.Create(item);

            var ITEMS = await (_itemsRepository.GetAll());
            // FindAll(e => e > 0); ;
            ITEMS.ToList().Where(e => e.User_id == item.User_id);
            var id =  ITEMS.ToList().Select(e=> e.Item_id).Max();
            CreateEventDto eventDto = new CreateEventDto("Created",0,id);
            var eventt = _mapper.Map<Event>(eventDto);
            eventt.User_id = item.User_id;
            await _eventsRepository.Create(eventt);
            return Created($"/api/items/{item.Item_id}", _mapper.Map<ItemDto>(item));

        }

        [HttpPut("{id}")]
        [Authorize(Roles = RestUserRoles.RegularUser)]
        public async Task<ActionResult<ItemDto>> Put(int id, EditItemDto itemDto)
        {
           // var item = _mapper.Map<Item>(itemDto);
            var item = await _itemsRepository.Get(id);
            if (item == null) return NotFound($"Item with id '{id}' not found!");
            
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, item, PolicyNames.SameUser);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            _mapper.Map(itemDto,item);
            //item.Description = itemDto.Description;
            await _itemsRepository.Put(item);

            return Ok(_mapper.Map<ItemDto>(item));

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RestUserRoles.RegularUser)]
        public async Task<ActionResult<ItemDto>> Delete(int id)
        {
            // var item = _mapper.Map<Item>(itemDto);
            var item = await _itemsRepository.Get(id);
            if (item == null) return NotFound($"Item with id '{id}' not found!");
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, item, PolicyNames.SameUser);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }
            var images = await _imagesRepository.GetAll(id);
            await _itemsRepository.Delete(id);

           // var images = await _imagesRepository.GetAll(id);
            var targetsToDelete = images.ToList().Select(e => e.Image_url);
            foreach (var name in targetsToDelete)
            {
                string path = _env.WebRootPath + "/Upload/" + name;
                FileInfo file = new FileInfo(path);
                if (file.Exists)//check file exsit or not  
                {
                    file.Delete();
                }
            }

            return NoContent();

        }

    }
}
