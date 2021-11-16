using AutoMapper;
using BidToBuy.Auth.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Data.Dtos.Events;
using WebApplication1.Data.Entities;
using WebApplication1.Data.Repositories;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventsController : ControllerBase
    {
        private readonly IEventsRepository _eventsRepository;
        private readonly IItemsRepository _itemsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IAuthorizationService _authorizationService;
        //private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        public EventsController(IEventsRepository eventsRepository, IMapper mapper, IItemsRepository itemsRepository, IUsersRepository usersRepository, IAuthorizationService authorizationService)
        {
            _usersRepository = usersRepository;
            _eventsRepository = eventsRepository;
            _mapper = mapper;
            _itemsRepository = itemsRepository;
            _authorizationService = authorizationService;
            //_usersRepository = usersRepository;
        }

        [HttpGet]
        [Authorize(Roles = RestUserRoles.RegularUser)]
        public async Task<IEnumerable<EventDto>> GetAll()
        {
            return (await _eventsRepository.GetAll()).Select(o => new EventDto(o.Event_id, o.Action, o.Date, o.CurrentSum, o.User_id, o.Item_id));
        }
        [HttpGet("{id}")]
        [Authorize(Roles = RestUserRoles.RegularUser)]
        public async Task<ActionResult<Event>> Get(int id)
        {
            var eventt = await _eventsRepository.Get(id);
            if (eventt == null) return NotFound($"Event with id '{id}' not found!");
            return Ok(_mapper.Map<EventDto>(eventt));
        }
        [HttpPost]
        [Authorize(Roles = RestUserRoles.RegularUser)]
        public async Task<ActionResult<EventDto>> Post(CreateEventDto eventDto)
        {

            //var user = await _usersRepository.Get(eventDto.User_id);
            //if (user == null) return NotFound($"User with id '{eventDto.User_id}' not found!");
            var item = await _itemsRepository.Get(eventDto.Item_id);
            if (item == null) return NotFound($"Item with id '{eventDto.Item_id}' not found!");

            var eventt = _mapper.Map<Event>(eventDto);
            eventt.User_id = User.FindFirstValue(CustomClaims.UserId);
            await _eventsRepository.Create(eventt);
            return Created($"/api/events/{eventt.Event_id}", _mapper.Map<EventDto>(eventt));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = RestUserRoles.Admin)]
        public async Task<ActionResult<EventDto>> Put(int id, EditEventDto eventDto)
        {
            var eventt = await _eventsRepository.Get(id);
            if (eventt == null) return NotFound($"Event with id '{id}' not found!");
            //if(eventt.Action == "bid" && eventt.CurrentSum > )
            //var authorizationResult = await _authorizationService.AuthorizeAsync(User, eventt, PolicyNames.SameUser);
            //if (!authorizationResult.Succeeded)
            //{
            //    return Forbid();
            //}
            _mapper.Map(eventDto, eventt);
            //eventt.Description = eventDto.Description;
            await _eventsRepository.Put(eventt);

            return Ok(_mapper.Map<EventDto>(eventt));

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RestUserRoles.Admin)]
        public async Task<ActionResult<EventDto>> Delete(int id)
        {
            // var eventt = _mapper.Map<Event>(eventDto);
            var eventt = await _eventsRepository.Get(id);
            if (eventt == null) return NotFound($"Event with id '{id}' not found!");

            await _eventsRepository.Delete(eventt);

            return NoContent();
        }
    }
}
