using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApplication1.Data.Dtos.Items;
using WebApplication1.Data.Dtos.Images;
using WebApplication1.Data.Entities;
using WebApplication1.Data.Dtos.Events;
using WebApplication1.Data.Dtos.Users;
using WebApplication1.Data.Dtos.Ratings;
using BidToBuy.Data.Dtos.Auth;

namespace WebApplication1.Data
{
    public class RestProfile:Profile
    {
        public RestProfile() 
        {
            CreateMap<Item, ItemDto>();
            CreateMap<CreateItemDto,Item>();
            CreateMap<EditItemDto, Item>();

            CreateMap<Image, ImageDto>();
            CreateMap<CreateImageDto, Image>();
            CreateMap<EditImageDto, Image>();

            CreateMap<Event, EventDto>();
            CreateMap<CreateEventDto, Event>();
            CreateMap<EditEventDto, Event>();

            //CreateMap<User, UserDto>();
            //CreateMap<CreateUserDto, User>();
            //CreateMap<EditUserDto, User>();

            CreateMap<Rating, RatingDto>();
            CreateMap<CreateRatingDto, Rating>();
            CreateMap<EditRatingDto, Rating>();

            CreateMap<RestUser, RestUserDto>();
        }
    }
}
