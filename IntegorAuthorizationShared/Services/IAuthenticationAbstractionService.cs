using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegorAuthorizationShared.Services
{
    using Dto.Users;

    public interface IAuthenticationAbstractionService
	{
		Task LoginAsync(UserAccountDto user);
		Task LogoutAsync();

		Task<bool> RefreshRequiredAsync();
		Task<UserAccountDto> GetAuthenticatedUserAsync();
	}
}
