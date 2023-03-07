using AutoMapper;

using IntegorAuthorizationShared.Dto.Users;

namespace IntegorAuthorization.Mapper.Profiles.AspDtoMap
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
