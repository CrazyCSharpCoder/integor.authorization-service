using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegorAuthorizationShared.Services
{
	public interface IUserValidationService
	{
		Task<bool> EMailExistsAsync(string email);
	}
}
