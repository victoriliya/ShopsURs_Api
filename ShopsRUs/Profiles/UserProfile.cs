using AutoMapper;
using ShopsRUs.Dtos;
using ShopsRUs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsRUs.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {

            CreateMap<User, UserReadDto>()
                .ForMember(x => x.CustomerType, cd => cd.MapFrom(map => map.UserType.UserTypeName.ToString())).ReverseMap();
            CreateMap<User, UserToAddDto>().ReverseMap();
            
        }
    }
}
