using AutoMapper;

using IntegorAuthorizationModel;
using IntegorAuthorizationShared.Dto.Users;

namespace IntegorAuthorization.Mapper.Profiles
{
    public class UsersMapperProfile : Profile
    {
        public UsersMapperProfile()
        {
            CreateMap<UserAccount, UserAccountPublicDto>();
			CreateMap<AddUserAccountDto, UserAccount>();
        }
    }
}
