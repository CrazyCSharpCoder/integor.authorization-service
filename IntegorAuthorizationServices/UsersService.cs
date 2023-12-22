using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;

using IntegorLogicShared.Types.IntegorServices.Authorization;
using IntegorLogicShared.IntegorServices.Authorization;

using IntegorAuthorizationModel;

using IntegorAuthorizationShared.Dto.Users;
using IntegorAuthorizationShared.Services;

namespace IntegorAuthorizationServices
{
	using DatabaseContext;
	using Internal.DatabaseContextExtensions;

	public class UsersService : IUsersService
	{
		private IntegorAuthorizationDbContext _context;
		private IMapper _mapper;

		private UserRolesEnumConverter _rolesHelper;

		public UsersService(
			IntegorAuthorizationDbContext context,
			IMapper mapper,
			UserRolesEnumConverter rolesHelper)
		{
			_context = context;
			_mapper = mapper;

			_rolesHelper = rolesHelper;
		}

		public async Task<UserAccountDto> AddAsync(AddUserAccountDto dto)
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
			UserAccountDto resultDto = _mapper.MapUserToPublicDto(createdAccount, role);

			return resultDto;
		}

		public async Task<UserAccountDto?> GetByIdAsync(int id)
		{
			UserAccount? account = await _context.GetUserByIdAsync(id);

			if (account == null)
				return null;

			UserRole role = (await _context.GetRoleAsync(account.RoleId))!;
			UserAccountDto resultDto = _mapper.MapUserToPublicDto(account, role);

			return resultDto;
		}

		public async Task<UserAccountDto?> GetByEmailAsync(string email)
		{
			UserAccount? account = await _context.GetUserByEmailAsync(email);
			
			if (account == null)
				return null;

			UserRole role = (await _context.GetRoleAsync(account.RoleId))!;
			UserAccountDto resultDto = _mapper.MapUserToPublicDto(account, role);

			return resultDto;
		}
	}
}
