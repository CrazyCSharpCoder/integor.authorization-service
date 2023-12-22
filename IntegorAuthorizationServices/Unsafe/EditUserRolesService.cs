using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IntegorAuthorizationModel;

using IntegorAuthorizationShared.Dto;
using IntegorAuthorizationShared.Services.Unsafe;

namespace IntegorAuthorizationServices.Unsafe
{
	using DatabaseContext;

	public class EditUserRolesService : IEditUserRolesService
	{
		private IntegorAuthorizationDbContext _context;

		public EditUserRolesService(IntegorAuthorizationDbContext context)
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
