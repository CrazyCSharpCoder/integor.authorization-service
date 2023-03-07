using AutoMapper;

using PrettyUserAuthorizationShared.Dto.Users;

namespace PrettyUserAuthorization.Mapper.Profiles.AspDtoMap
{
	using Dto.Authentication;

	public class AuthenticationDtoMapperProfile : Profile
	{
		public AuthenticationDtoMapperProfile()
		{
			CreateMap<RegisterUserDto, AddUserAccountDto>();
		}
	}
}
