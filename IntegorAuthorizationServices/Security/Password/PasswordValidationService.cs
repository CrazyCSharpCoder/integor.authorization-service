using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using IntegorAuthorizationModel;
using IntegorAuthorizationShared.Services.Security.Password;

namespace IntegorAuthorizationServices.Security.Password
{
	using DatabaseContext;

	public class PasswordValidationService : IPasswordValidationService
	{
		private IntegorAuthorizationDbContext _context;

		public PasswordValidationService(IntegorAuthorizationDbContext context)
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
