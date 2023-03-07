using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegorAuthorizationShared.Dto.Users
{
	using Roles;

    public class UserAccountPublicDto
    {
        public int Id { get; set; }

        public string EMail { get; set; } = null!;

        public DateTime PasswordUpdatedDate { get; set; }

        public bool IsActive { get; set; }
        public DateTime? IsActiveUpdatedDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public UserRoleShortDto Role { get; set; } = null!;
        public DateTime RoleUpdatedDate { get; set; }
    }
}
