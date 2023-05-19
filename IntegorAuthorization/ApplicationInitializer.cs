using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using IntegorAuthorizationModel;
using IntegorLogicShared.Types.IntegorServices.Authorization;

using IntegorAuthorizationShared.Services;
using IntegorAuthorizationShared.Services.Unsafe;

using IntegorLogicShared.IntegorServices.Authorization;

namespace IntegorAuthorization
{
	public class ApplicationInitializer
	{
		private Dictionary<UserRoles, UserRole> _rolesData = new Dictionary<UserRoles, UserRole>()
		{
			//{
			//	UserRoles.Admin, new UserRole()
			//	{
			//		Title = "Admin",
			//		Description = "Advanced functional is available, and also can manage moderators."
			//	}
			//},
			//{
			//	UserRoles.Moderator, new UserRole()
			//	{
			//		Title = "Moderator",
			//		Description = "Advanced functional is available."
			//	}
			//},

			// TODO set descriptions to cover context of particular application concept
			{
				UserRoles.User, new UserRole()
				{
					Title = "User",
					Description = "Basic functional is available."
				}
			}
		};

		private IUserRolesService _rolesService;
		private IEditUserRolesService _editRolesService;

		private IMapper _mapper;
		private UserRolesEnumConverter _rolesHelper;

		public ApplicationInitializer(
			IUserRolesService rolesService,
			IEditUserRolesService editRolesService,
			IMapper mapper,
			UserRolesEnumConverter rolesHelper)
		{
			_rolesService = rolesService;
			_editRolesService = editRolesService;

			_mapper = mapper;
			_rolesHelper = rolesHelper;
		}

		public async Task EnsureRolesCreatedAsync()
		{
			IEnumerable<UserRole> roles = await _rolesService.GetAllAsync();

			foreach (UserRoles role in Enum.GetValues<UserRoles>())
			{
				if (!roles.Any(r => r.Id == _rolesHelper.RolesEnumToRoleId(role)))
					await CreateRoleAsync(role);
			} 
		}

		private async Task CreateRoleAsync(UserRoles role)
		{
			UserRole roleEntry = _mapper.Map<UserRole>(_rolesData[role]);
			roleEntry.Id = _rolesHelper.RolesEnumToRoleId(role);

			await _editRolesService.AddAsync(roleEntry);
		}
	}
}
