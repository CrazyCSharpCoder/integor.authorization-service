using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrettyUserAuthorizationShared.Services.Security.Password
{
    public interface IPasswordValidationService
    {
		Task<bool> IsPasswordValidAsync(int userId, string passwordHash);
	}
}
