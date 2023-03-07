using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IntegorAuthorizationModel;

namespace IntegorAuthorizationShared.Helpers
{
	using Types;

	public class UserRolesConverter
	{
		private static Dictionary<UserRoles, Func<UserRole>> _match = new Dictionary<UserRoles, Func<UserRole>>()
		{
			{ UserRoles.User, () => new UserRole() { Id = (int)UserRoles.User, Title = "User" } }
		};

		public int RolesEnumToRoleId(UserRoles role)
		{
			return (int)role;
		}

		public UserRoles ModelToRolesEnum(int roleId)
		{
			UserRoles? role = Enum.GetValues<UserRoles>().FirstOrDefault(value => (int)value == roleId);

			if (role == null)
			{
				throw new InvalidOperationException(
					"There is not a enum value corresponding the specified model");
			}

			return role.Value;
		}

		public UserRoles ModelToRolesEnum(UserRole role)
		{
			return ModelToRolesEnum(role.Id);
		}
	}
}
