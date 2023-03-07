using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrettyUserAuthorizationShared.Services
{
    using Dto.Users;

    public interface IAuthenticationAbstractionService
	{
		Task LoginAsync(UserAccountPublicDto user);
		Task LogoutAsync();

		Task<bool> RefreshRequiredAsync();
		Task<UserAccountPublicDto> GetAuthenticatedUserAsync();
	}
}
