using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PrettyUserAuthorizationModel;

namespace PrettyUserAuthorizationShared.Services.Unsafe
{
	public interface IEditUserRolesService
	{
		Task<UserRole> AddAsync(UserRole role);
	}
}
