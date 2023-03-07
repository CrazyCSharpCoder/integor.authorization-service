using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IntegorAuthorizationModel;

namespace IntegorAuthorizationShared.Services
{
	public interface IUserRolesService
	{
		Task<IEnumerable<UserRole>> GetAllAsync();
		Task<UserRole> GetRoleOfUserAsync(int userId);
		Task<UserRole> SetRoleAsync(int userId, int roleId);
	}
}
