using AutoMapper;

using IntegorAuthorizationModel;
using IntegorAuthorizationShared.Dto.Roles;

namespace IntegorAuthorization.Mapper.Profiles
{
	public class UserRolesMapperProfile : Profile
	{
		public UserRolesMapperProfile()
		{
			CreateMap<UserRole, UserRoleShortDto>();
		}		
	}
}
