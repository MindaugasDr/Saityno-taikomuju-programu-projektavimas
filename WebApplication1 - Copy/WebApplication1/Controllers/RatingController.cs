using AutoMapper;
using BidToBuy.Auth.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Data.Dtos.Ratings;
using WebApplication1.Data.Entities;
using WebApplication1.Data.Repositories;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/ratings")]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingsRepository _ratingsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        public RatingsController(IRatingsRepository ratingsRepository, IMapper mapper, IUsersRepository usersRepository, IAuthorizationService authorizationService)
        {
            _usersRepository = usersRepository;
            _ratingsRepository = ratingsRepository;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IEnumerable<RatingDto>> GetAll()
        {
            return (await _ratingsRepository.GetAll()).Select(o => new RatingDto(o.User_id, o.Reviewer_id, o.Left_rating));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Rating>> Get(int id)
        {
            var rating = await _ratingsRepository.Get(id);
            if (rating == null) return NotFound($"Rating with id '{id}' not found!");
            return Ok(_mapper.Map<RatingDto>(rating));
        }
        [HttpPost]
        [Authorize(Roles = RestUserRoles.RegularUser)]
        public async Task<ActionResult<RatingDto>> Post(CreateRatingDto ratingDto)
        {
            //var user1 = await _usersRepository.Get(ratingDto.User_id);
            var user2 = await _usersRepository.Get(ratingDto.Reviewer_id);
            var id = User.FindFirstValue(CustomClaims.UserId);
            //ratingDto.User_id = id;
            //if (user1 == null) return NotFound($"User with id '{ratingDto.User_id}' not found!");
            if (user2 == null) return NotFound($"User (reviewer) with id '{ratingDto.Reviewer_id}' not found!");
            if (id == ratingDto.Reviewer_id) return BadRequest("User can't rate himself!");
            var allRatings = await _ratingsRepository.GetAll();
            var tmp = allRatings.ToList().Where(e => e.User_id == id && e.Reviewer_id == ratingDto.Reviewer_id).FirstOrDefault();
            if (tmp != null) {
                return BadRequest("$You have already rated this user!");
            }
            

            var rating = _mapper.Map<Rating>(ratingDto);
            rating.User_id = id;
            await _ratingsRepository.Create(rating);
            return Created($"/api/ratings/{rating.Rating_id}", _mapper.Map<RatingDto>(rating));

        }

        [HttpPut("{id}")]
        [Authorize(Roles = RestUserRoles.RegularUser)]
        public async Task<ActionResult<RatingDto>> Put(int id, EditRatingDto ratingDto)
        {
            // var item = _mapper.Map<Item>(itemDto);
            //var user1 = await _usersRepository.Get(ratingDto.User_id);
            //var user2 = await _usersRepository.Get(ratingDto.Reviewer_id);
            //if (user1 == null) return NotFound($"User with id '{ratingDto.User_id}' not found!");
            //if (user2 == null) return NotFound($"User (reviewer) with id '{ratingDto.Reviewer_id}' not found!");
            var rating = await _ratingsRepository.Get(id);
            if (rating == null) return NotFound($"Rating with id '{id}' not found!");
            var idtmp = (User.FindFirst(CustomClaims.UserId).Value);
            var result = (idtmp == rating.User_id|| User.IsInRole(RestUserRoles.Admin));
            //var authorizationResult = await _authorizationService.AuthorizeAsync(User, rating, PolicyNames.SameUser);
            if (!result)
            {
                return Forbid();
            }

            _mapper.Map(ratingDto, rating);
            //item.Description = itemDto.Description;
            await _ratingsRepository.Put(rating);

            return Ok(_mapper.Map<RatingDto>(rating));

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RestUserRoles.RegularUser)]
        public async Task<ActionResult<RatingDto>> Delete(int id)
        {
            // var item = _mapper.Map<Item>(itemDto);
            var rating = await _ratingsRepository.Get(id);
            if (rating == null) return NotFound($"Rating with id '{id}' not found!");
            //var authorizationResult = await _authorizationService.AuthorizeAsync(User, rating, PolicyNames.SameUser);
            var idtmp = (User.FindFirst(CustomClaims.UserId).Value);

            var result = (idtmp == rating.User_id || User.IsInRole(RestUserRoles.Admin));
            if (!result)
            {
                return Forbid();
            }
            await _ratingsRepository.Delete(rating);

            return NoContent();

        }

    }
}
