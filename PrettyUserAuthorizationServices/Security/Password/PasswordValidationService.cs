using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using PrettyUserAuthorizationModel;
using PrettyUserAuthorizationShared.Services.Security.Password;

namespace PrettyUserAuthorizationServices.Security.Password
{
	using DatabaseContext;

	public class PasswordValidationService : IPasswordValidationService
	{
		private PrettyUserAuthorizationDbContext _context;

		public PasswordValidationService(PrettyUserAuthorizationDbContext context)
		{
			_context = context;
		}
		
		public async Task<bool> IsPasswordValidAsync(int userId, string passwordHash)
		{
			UserAccount? user = await _context.Users.FirstOrDefaultAsync(user => user.Id == userId);
			return user!.PasswordHash == passwordHash;
		}
	}
}
