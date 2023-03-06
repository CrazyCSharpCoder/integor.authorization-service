using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using PrettyUserAuthorizationModel;

namespace PrettyUserAuthorizationServices.Internal.DatabaseContextExtensions
{
	using DatabaseContext;

	public static class RolesDbContextExtensions
	{
		public static async Task<UserRole?> GetRoleAsync(
			this PrettyUserAuthorizationDbContext context, int id, bool asNoTracking = true)
		{
			return await context.UserRoles
				.ApplyAsNoTracking(asNoTracking)
				.FirstOrDefaultAsync(role => role.Id == id);
		}
	}
}
