using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegorAuthorizationShared.Services.Unsafe
{
	public interface IUserRolesValidationService
	{
		Task<bool> RoleTitleAvaildableAsync(string title);
	}
}
