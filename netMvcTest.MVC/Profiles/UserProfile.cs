using netMvcTest.Dto;
using netMvcTest.Dto.User;
using netMvcTest.Model.Entity;

namespace netMvcTest.MVC.Profiles
{
    public class UserProfile : AutoMapper.Profile
    {

        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<RegisterUserDto, User>();
            CreateMap<User, RegisterUserDto>();
        }
    }
}