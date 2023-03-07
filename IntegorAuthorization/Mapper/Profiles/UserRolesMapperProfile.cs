using AutoMapper;

using PrettyUserAuthorizationModel;
using PrettyUserAuthorizationShared.Dto.Roles;

namespace PrettyUserAuthorization.Mapper.Profiles
{
	public class UserRolesMapperProfile : Profile
	{
		public UserRolesMapperProfile()
		{
			CreateMap<UserRole, UserRoleShortDto>();
		}		
	}
}
