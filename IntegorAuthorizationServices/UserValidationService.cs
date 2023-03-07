using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IntegorAuthorizationShared.Services;

using Microsoft.EntityFrameworkCore;

namespace IntegorAuthorizationServices
{
	using DatabaseContext;

	public class UserValidationService : IUserValidationService
	{
		private IntegorAuthorizationDbContext _context;

		public UserValidationService(IntegorAuthorizationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> EMailExistsAsync(string email)
		{
			return await _context.Users.AnyAsync(user => user.Email == email);
		}
	}
}
