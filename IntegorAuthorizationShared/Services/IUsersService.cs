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
		Task<UserAccountDto> AddAsync(AddUserAccountDto account);

		Task<UserAccountDto?> GetByIdAsync(int id);
		Task<UserAccountDto?> GetByEmailAsync(string email);
	}
}
