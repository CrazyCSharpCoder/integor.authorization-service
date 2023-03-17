using AutoMapper;

using IntegorAuthorizationShared.Dto.Users;
using IntegorAuthorizationShared.Dto.Roles;

using IntegorAuthorizationModel;

namespace IntegorAuthorization.Mapper.Profiles.Dto
{
	public class AuthenticationDtoMapperProfile : Profile
	{
		public AuthenticationDtoMapperProfile()
		{
			CreateMap<UserAccountDto, PublicDto.Users.UserAccountInfoDto>();

			CreateMap<UserRoleShortDto, PublicDto.Roles.UserRoleShortDto>();
			CreateMap<UserRole, PublicDto.Roles.UserRoleFullDto>();
			
			CreateMap<PublicDto.Users.Input.RegisterUserDto, AddUserAccountDto>();
		}
	}
}
