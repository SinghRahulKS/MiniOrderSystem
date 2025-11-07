using AutoMapper;
using ProductService.Model.Request;
using ProductService.Model.Response;
using ProductService.Repository.Entity;

namespace ProductService.Model.Mapper
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<CategoryCreateRequest, Category>().ReverseMap();
            CreateMap<Category, CategoryResponse>().ReverseMap();
            CreateMap<Product, ProductResponse>().ReverseMap();
            CreateMap<CreateProductRequest, Product>().ReverseMap();
        }
    }
}
