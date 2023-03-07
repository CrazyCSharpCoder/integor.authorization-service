using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IntegorAuthorizationModel;

namespace IntegorAuthorizationShared.Services.Unsafe
{
	public interface IEditUserRolesService
	{
		Task<UserRole> AddAsync(UserRole role);
	}
}
