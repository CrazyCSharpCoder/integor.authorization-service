using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using PrettyUserAuthorizationModel;

using PrettyUserAuthorizationShared.Types;
using PrettyUserAuthorizationShared.Services;
using PrettyUserAuthorizationShared.Services.Unsafe;
using PrettyUserAuthorizationShared.Helpers;

namespace PrettyUserAuthorization
{
	public class ApplicationInitializer
	{
		private Dictionary<UserRoles, UserRole> _rolesData = new Dictionary<UserRoles, UserRole>()
		{
			// TODO set descriptions to cover context of particular application concept
			{
				UserRoles.Admin, new UserRole()
				{
					Title = "Admin",
					Description = "Advanced functional is available, and also can manage moderators."
				}
			},
			{
				UserRoles.Moderator, new UserRole()
				{
					Title = "Moderator",
					Description = "Advanced functional is available."
				}
			},
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
		private UserRolesConverter _helper;

		public ApplicationInitializer(
			IUserRolesService rolesService,
			IEditUserRolesService editRolesService,
			IMapper mapper,
			UserRolesConverter helper)
		{
			_rolesService = rolesService;
			_editRolesService = editRolesService;

			_mapper = mapper;
			_helper = helper;
		}

		public async Task EnsureRolesCreatedAsync()
		{
			IEnumerable<UserRole> roles = await _rolesService.GetAllAsync();

			foreach (UserRoles role in Enum.GetValues<UserRoles>())
			{
				if (!roles.Any(r => r.Id == _helper.RolesEnumToRoleId(role)))
					await CreateRoleAsync(role);
			} 
		}

		private async Task CreateRoleAsync(UserRoles role)
		{
			UserRole roleEntry = _mapper.Map<UserRole>(_rolesData[role]);
			roleEntry.Id = _helper.RolesEnumToRoleId(role);

			await _editRolesService.AddAsync(roleEntry);
		}
	}
}
