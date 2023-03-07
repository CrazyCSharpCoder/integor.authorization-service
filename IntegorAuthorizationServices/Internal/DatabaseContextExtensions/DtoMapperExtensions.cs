using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using PrettyUserAuthorizationModel;

using PrettyUserAuthorizationShared.Dto.Roles;
using PrettyUserAuthorizationShared.Dto.Users;

namespace PrettyUserAuthorizationServices.Internal.DatabaseContextExtensions
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
