using BusinessObject.Models;
using BusinessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Mapping
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, OrderItemDTO>()
                .ForMember(des => des.Email,
                           opt => opt.MapFrom(src => src.Member.Email))
                .ReverseMap();
            CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, ProductResponseDTO>().ReverseMap();
            CreateMap<Member, MemberDTO>().ReverseMap();
            CreateMap<Member, MemberResponseDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
        }

    }
}
