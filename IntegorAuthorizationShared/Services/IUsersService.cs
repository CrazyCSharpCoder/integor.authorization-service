using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegorAuthorizationShared.Services
{
    using Dto.Users;

    public interface IUsersService
	{
		Task<UserAccountPublicDto> AddAsync(AddUserAccountDto account);

		Task<UserAccountPublicDto?> GetByIdAsync(int id);
		Task<UserAccountPublicDto?> GetByEmailAsync(string email);
	}
}
