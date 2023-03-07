using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrettyUserAuthorizationShared.Dto.Users
{
    public class AddUserAccountDto
    {
        public string EMail { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;
		public string PasswordSalt { get; set; } = null!;
    }
}
