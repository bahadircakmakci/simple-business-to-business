using AutoMapper;
using simple_business_to_business.ApplicationLayer.Modes.DTOs;
using simple_business_to_business.DomainLayer.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace simple_business_to_business.ApplicationLayer.AutoMapper
{
    public class Mapping: Profile
    {
        public Mapping()
        {
            CreateMap<AppUsers, RegisterDTO>().ForMember(x => x.Name, opt => opt.MapFrom(a => a.FullName)).ReverseMap();
            CreateMap<AppUsers, LoginDTO>().ReverseMap();
        }
    }
}
