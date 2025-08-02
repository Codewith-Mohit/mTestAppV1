using AutoMapper;
using mTestAppV1.Dto;
using mTestAppV1.Entities;

namespace mTestAppV1.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
        }
    }
}
