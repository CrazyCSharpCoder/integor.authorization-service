using AutoMapper;

using PrettyUserAuthorizationModel;
using PrettyUserAuthorizationShared.Dto.Users;

namespace PrettyUserAuthorization.Mapper.Profiles
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
