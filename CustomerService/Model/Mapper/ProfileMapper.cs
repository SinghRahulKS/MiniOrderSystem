using AutoMapper;
using UserService.Model.Request;
using UserService.Repository.Entity;

namespace UserService.Model.Mapper
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<User, RegisterRequestModel>().ReverseMap();
            CreateMap<ContactRequest, ContactMessage>().ReverseMap();
        }
    }
}
