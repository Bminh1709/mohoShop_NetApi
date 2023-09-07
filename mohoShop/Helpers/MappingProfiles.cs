using AutoMapper;
using mohoShop.Models;
using mohoShop.Dto;

namespace mohoShop.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDtoNoId, Category>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDtoWCatId, Product>();
            CreateMap<ProductDtoUpdate, Product>();
            CreateMap<CustomerDto, Customer>();

        }
    }
}
