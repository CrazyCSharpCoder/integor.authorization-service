using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrettyUserAuthorizationShared.Services
{
	public interface IUserValidationService
	{
		Task<bool> EMailExistsAsync(string email);
	}
}
