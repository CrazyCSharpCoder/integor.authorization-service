using AutoMapper;

using IntegorAuthorizationModel;
using IntegorAuthorizationShared.Dto.Users;

namespace IntegorAuthorization.Mapper.Profiles
{
    public class UsersMapperProfile : Profile
    {
        public UsersMapperProfile()
        {
            CreateMap<UserAccount, UserAccountDto>();
			CreateMap<AddUserAccountDto, UserAccount>();
        }
    }
}
