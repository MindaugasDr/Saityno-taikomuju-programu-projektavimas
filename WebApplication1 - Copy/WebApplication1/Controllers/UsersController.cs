using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data.Repositories;
using WebApplication1.Data.Dtos.Users;
using WebApplication1.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using BidToBuy.Auth.Model;
using BidToBuy.Data.Dtos.Auth;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        public UsersController(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = RestUserRoles.RegularUser)]
        //[Authorize(Roles = RestUserRoles.Admin)]
        public async Task<IEnumerable<RestUserDto>> GetAll()
        {
            return (await _usersRepository.GetAll()).Select(o => new RestUserDto(o.UserName, o.Id, o.PhoneNumber));
        }
        [HttpGet("{id}")]
        [Authorize(Roles = RestUserRoles.RegularUser)]
        public async Task<ActionResult<RestUserDto>> Get(string id)
        {
            var user = await _usersRepository.Get(id);
            if (user == null) return NotFound($"User with id '{id}' not found!");
            return Ok(_mapper.Map<RestUserDto>(user));
        }
        //[HttpPost]


        //[HttpPut("{id}")]
        //public async Task<ActionResult<UserDto>> Put(int id, EditUserDto userDto)
        //{
        //    // var item = _mapper.Map<Item>(itemDto);
        //    var user = await _usersRepository.Get(id);
        //    if (user == null) return NotFound($"User with id '{id}' not found!");

        //    _mapper.Map(userDto, user);
        //    //item.Description = itemDto.Description;
        //    await _usersRepository.Put(user);

        //    return Ok(_mapper.Map<UserDto>(user));
        //}

        [HttpDelete("{id}")]
        [Authorize(Roles = RestUserRoles.Admin)]
        public async Task<ActionResult<UserDto>> Delete(string id)
        {
            // var item = _mapper.Map<Item>(itemDto);
            var user = await _usersRepository.Get(id);
            if (user == null) return NotFound($"User with id '{id}' not found!");

            await _usersRepository.Delete(id);

            return NoContent();

        }

    }
}
