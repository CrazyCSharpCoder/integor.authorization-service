using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using IntegorAuthorizationModel;

using IntegorAuthorizationShared.Dto.Roles;
using IntegorAuthorizationShared.Dto.Users;

namespace IntegorAuthorizationServices.Internal.DatabaseContextExtensions
{
	internal static class DtoMapperExtensions
	{
		public static UserAccountPublicDto MapUserToPublicDto(
			this IMapper mapper, UserAccount user, UserRole role)
		{
			UserAccountPublicDto publicUser = mapper.Map<UserAccountPublicDto>(user);
			publicUser.Role = mapper.Map<UserRoleShortDto>(role);

			return publicUser;
		}
	}
}
