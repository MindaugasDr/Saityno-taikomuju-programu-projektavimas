using AutoMapper;
using BidToBuy.Auth;
using BidToBuy.Auth.Model;
using BidToBuy.Data.Dtos.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using WebApplication1.Data.Dtos.Users;
using WebApplication1.Data.Entities;

namespace BidToBuy.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api")]
    public class AuthController:ControllerBase
    {
        private readonly UserManager<RestUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenManager _tokenManager;
        public AuthController(UserManager<RestUser> userManager, IMapper mapper, ITokenManager tokenManager) {
            _userManager = userManager;
            _mapper = mapper;
            _tokenManager = tokenManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto) {

            var user = await _userManager.FindByNameAsync(registerUserDto.UserName);
            
            if (user != null) return BadRequest("Request invalid!"); 
            var newUser = new RestUser() {
                PhoneNumber = registerUserDto.PhoneNumber,
                UserName = registerUserDto.UserName 
            };
            var createUserResult = await _userManager.CreateAsync(newUser,registerUserDto.Password);
            if (!createUserResult.Succeeded) {
                return BadRequest("Could not create an user!");
            }
            await _userManager.AddToRoleAsync(newUser,RestUserRoles.RegularUser);
            return CreatedAtAction(nameof(Register), _mapper.Map<RestUserDto>(newUser));
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto) {
            var user = await _userManager.FindByNameAsync(loginUserDto.UserName);
            if (user == null) 
                return BadRequest("Username or password is incorrect");
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);
            if (!isPasswordValid)
                return BadRequest("Username or password is incorrect");

            var accessToken = await _tokenManager.CreateAccessTokenAsync(user);
            return Ok(new SuccesfulLoginResponseDto(accessToken));
        }
        
    }
}
