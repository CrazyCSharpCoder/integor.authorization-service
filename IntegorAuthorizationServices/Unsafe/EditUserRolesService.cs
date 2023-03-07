using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PrettyUserAuthorizationModel;

using PrettyUserAuthorizationShared.Dto;
using PrettyUserAuthorizationShared.Services.Unsafe;

namespace PrettyUserAuthorizationServices.Unsafe
{
	using DatabaseContext;

	public class EditUserRolesService : IEditUserRolesService
	{
		private PrettyUserAuthorizationDbContext _context;

		public EditUserRolesService(PrettyUserAuthorizationDbContext context)
		{
			_context = context;
		}

		public async Task<UserRole> AddAsync(UserRole role)
		{
			await _context.AddAsync(role);
			await _context.SaveChangesAsync();

			return role;
		}
	}
}
