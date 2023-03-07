using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using IntegorAuthorizationModel;

namespace IntegorAuthorizationServices.Internal.DatabaseContextExtensions
{
	using DatabaseContext;

	public static class RolesDbContextExtensions
	{
		public static async Task<UserRole?> GetRoleAsync(
			this IntegorAuthorizationDbContext context, int id, bool asNoTracking = true)
		{
			return await context.UserRoles
				.ApplyAsNoTracking(asNoTracking)
				.FirstOrDefaultAsync(role => role.Id == id);
		}
	}
}
