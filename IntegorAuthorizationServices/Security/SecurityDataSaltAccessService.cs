using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PrettyUserAuthorizationModel;
using PrettyUserAuthorizationShared.Services.Security;

using Microsoft.EntityFrameworkCore;

namespace PrettyUserAuthorizationServices.Security
{
	using DatabaseContext;

	public class SecurityDataSaltAccessService : ISecurityDataAccessService
	{
		private PrettyUserAuthorizationDbContext _context;

		public SecurityDataSaltAccessService(PrettyUserAuthorizationDbContext context)
		{
			_context = context;
		}

		public async Task<SecurityData?> GetSecurityDataAsync(int userId)
		{
			return await _context.SecurityData.FirstOrDefaultAsync(data => data.UserId == userId);
		}
	}
}
