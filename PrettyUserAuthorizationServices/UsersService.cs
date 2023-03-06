using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using PrettyUserAuthorizationModel;

using PrettyUserAuthorizationShared.Types;
using PrettyUserAuthorizationShared.Dto.Users;
using PrettyUserAuthorizationShared.Services;
using PrettyUserAuthorizationShared.Helpers;

using Microsoft.EntityFrameworkCore.Storage;

namespace PrettyUserAuthorizationServices
{
	using DatabaseContext;
	using Internal.DatabaseContextExtensions;

	public class UsersService : IUsersService
	{
		private PrettyUserAuthorizationDbContext _context;
		private IMapper _mapper;

		private UserRolesConverter _rolesHelper;

		public UsersService(
			PrettyUserAuthorizationDbContext context,
			IMapper mapper,
			UserRolesConverter rolesHelper)
		{
			_context = context;
			_mapper = mapper;

			_rolesHelper = rolesHelper;
		}

		public async Task<UserAccountPublicDto> AddAsync(AddUserAccountDto dto)
		{
			UserAccount createdAccount = _mapper.Map<UserAccount>(dto);
			createdAccount.RoleId = _rolesHelper.RolesEnumToRoleId(UserRoles.User);

			SecurityData securityData = new SecurityData() { PasswordSalt = dto.PasswordSalt };

			using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync())
			{
				try
				{
					await _context.Users.AddAsync(createdAccount);
					await _context.SaveChangesAsync();

					securityData.UserId = createdAccount.Id;

					await _context.SecurityData.AddAsync(securityData);
					await _context.SaveChangesAsync();

					await transaction.CommitAsync();
				}
				catch
				{
					await transaction.RollbackAsync();
					throw;
				}
			}

			UserRole role = (await _context.GetRoleAsync(createdAccount.RoleId))!;
			UserAccountPublicDto resultDto = _mapper.MapUserToPublicDto(createdAccount, role);

			return resultDto;
		}

		public async Task<UserAccountPublicDto?> GetByIdAsync(int id)
		{
			UserAccount? account = await _context.GetUserByIdAsync(id);

			if (account == null)
				return null;

			UserRole role = (await _context.GetRoleAsync(account.RoleId))!;
			UserAccountPublicDto resultDto = _mapper.MapUserToPublicDto(account, role);

			return resultDto;
		}

		public async Task<UserAccountPublicDto?> GetByEmailAsync(string email)
		{
			UserAccount? account = await _context.GetUserByEmailAsync(email);
			
			if (account == null)
				return null;

			UserRole role = (await _context.GetRoleAsync(account.RoleId))!;
			UserAccountPublicDto resultDto = _mapper.MapUserToPublicDto(account, role);

			return resultDto;
		}
	}
}
