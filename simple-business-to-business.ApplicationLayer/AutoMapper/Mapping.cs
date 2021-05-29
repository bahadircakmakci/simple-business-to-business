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
            CreateMap<AppUsers, RegisterDTO>().ReverseMap();
            CreateMap<AppUsers, LoginDTO>().ReverseMap();
            CreateMap<AppUsers, SearchUserDTO>().ReverseMap();
            CreateMap<AppUsers, EditProfileDTO>().ReverseMap();
            CreateMap<AppUsers, ListUserDTO>().ReverseMap();
            CreateMap<Companies, CompaniesDto>().ReverseMap();
            CreateMap<Products, ProductDto>().ReverseMap();
            CreateMap<Brands, BrandDTO>().ReverseMap();
            CreateMap<MainCategories, CategoryDTO>().ReverseMap();
            CreateMap<SubCategories, CategoryDTO>().ReverseMap();
            CreateMap<ProductPictures, ProductPictureDTO>().ReverseMap();
            CreateMap<Basket, BasketDTO>().ReverseMap();
            CreateMap<Orders, OrderDTO>().ReverseMap();
            CreateMap<OrderDetails, OrderDetailDTO>().ReverseMap();
        }
    }
}
