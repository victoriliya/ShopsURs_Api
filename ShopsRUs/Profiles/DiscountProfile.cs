using AutoMapper;
using ShopsRUs.Dtos;
using ShopsRUs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsRUs.Profiles
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {

            CreateMap<Discount, DiscountReadDto>().ReverseMap();
            CreateMap<DiscountAddDto, Discount>().ReverseMap();

            CreateMap<DiscountAddDto, UserType>().ReverseMap();
            CreateMap<DiscountReadDto, UserType>().ReverseMap()
                .ForMember(x => x.DiscountRate, cd => cd.MapFrom(map => map.Discount.DiscountRate));
        }
    }
}
