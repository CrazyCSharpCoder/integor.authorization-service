using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IntegorPublicDto.Authorization.Users;

namespace IntegorAuthorizationShared.Services
{
	using Dto.Users;

    public interface IAuthenticationAbstractionService
	{
		Task LoginAsync(UserAccountInfoDto user);
		Task LogoutAsync();

		Task<UserAccountDto> GetAuthenticatedUserAsync();
	}
}
