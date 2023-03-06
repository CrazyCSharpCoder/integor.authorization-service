using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using PrettyUserAuthorizationModel;
using PrettyUserAuthorizationShared.Services.Exceptions;

using Microsoft.EntityFrameworkCore;

namespace PrettyUserAuthorizationServices.Internal.DatabaseContextExtensions
{
	using DatabaseContext;

	internal static class UsersDbContextExtensions
	{
		public static async Task<UserAccount?> GetUserByIdAsync(
			this PrettyUserAuthorizationDbContext context, int id, bool asNoTracking = true)
		{
			return await context.GetUserAccountAsync(user => user.Id == id, asNoTracking);
		}

		public static async Task<UserAccount?> GetUserByEmailAsync(
			this PrettyUserAuthorizationDbContext context, string email, bool asNoTracking = true)
		{
			return await context.GetUserAccountAsync(user => user.Email == email, asNoTracking);
		}

		public static async Task<UserAccount> GetUserByIdWithValidationAsync(
			this PrettyUserAuthorizationDbContext context, int id, bool asNoTracking = true)
		{
			return await context.GetUserAccountWithValidationAsync(user => user.Id == id, asNoTracking);
		}

		public static async Task<UserAccount> GetUserByEmailWithValidationAsync(
			this PrettyUserAuthorizationDbContext context, string email, bool asNoTracking = true)
		{
			return await context.GetUserAccountWithValidationAsync(user => user.Email == email, asNoTracking);
		}

		private static async Task<UserAccount?> GetUserAccountAsync(
			this PrettyUserAuthorizationDbContext context,
			Expression<Func<UserAccount, bool>> predicate, bool asNoTracking)
		{
			return await context.Users
				.ApplyAsNoTracking(asNoTracking)
				.FirstOrDefaultAsync(predicate);
		}

		private static async Task<UserAccount> GetUserAccountWithValidationAsync(
			this PrettyUserAuthorizationDbContext context,
			Expression<Func<UserAccount, bool>> predicate, bool asNoTracking)
		{
			UserAccount? account = await context.Users
				.ApplyAsNoTracking(asNoTracking)
				.FirstOrDefaultAsync(predicate);

			if (account == null)
				throw new UserNotFoundException("User with specified id does not exist");

			return account;
		}
	}
}
