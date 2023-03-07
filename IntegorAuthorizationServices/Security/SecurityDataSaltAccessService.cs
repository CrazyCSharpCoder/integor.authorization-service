using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IntegorAuthorizationModel;
using IntegorAuthorizationShared.Services.Security;

using Microsoft.EntityFrameworkCore;

namespace IntegorAuthorizationServices.Security
{
	using DatabaseContext;

	public class SecurityDataSaltAccessService : ISecurityDataAccessService
	{
		private IntegorAuthorizationDbContext _context;

		public SecurityDataSaltAccessService(IntegorAuthorizationDbContext context)
		{
			_context = context;
		}

		public async Task<SecurityData?> GetSecurityDataAsync(int userId)
		{
			return await _context.SecurityData.FirstOrDefaultAsync(data => data.UserId == userId);
		}
	}
}
