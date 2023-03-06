using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PrettyUserAuthorizationShared.Services;

using Microsoft.EntityFrameworkCore;

namespace PrettyUserAuthorizationServices
{
	using DatabaseContext;

	public class UserValidationService : IUserValidationService
	{
		private PrettyUserAuthorizationDbContext _context;

		public UserValidationService(PrettyUserAuthorizationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> EMailExistsAsync(string email)
		{
			return await _context.Users.AnyAsync(user => user.Email == email);
		}
	}
}
