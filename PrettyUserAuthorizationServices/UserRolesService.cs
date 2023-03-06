﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PrettyUserAuthorizationModel;
using PrettyUserAuthorizationShared.Services;

using Microsoft.EntityFrameworkCore;

namespace PrettyUserAuthorizationServices
{
	using DatabaseContext;
	using Internal.DatabaseContextExtensions;

	public class UserRolesService : IUserRolesService
	{
		private PrettyUserAuthorizationDbContext _context;

		public UserRolesService(PrettyUserAuthorizationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<UserRole>> GetAllAsync()
		{
			return await _context.UserRoles.AsNoTracking().ToArrayAsync();
		}

		public async Task<UserRole> GetRoleOfUserAsync(int userId)
		{
			UserAccount user = await _context.GetUserByIdWithValidationAsync(userId);
			return (await _context.GetRoleAsync(user.RoleId))!;
		}

		public async Task<UserRole> SetRoleAsync(int userId, int roleId)
		{
			UserAccount user = await _context.GetUserByIdWithValidationAsync(userId);
			user.RoleId = roleId;

			_context.Update(user);
			await _context.SaveChangesAsync();

			return (await _context.GetRoleAsync(user.RoleId))!;
		}
	}
}
