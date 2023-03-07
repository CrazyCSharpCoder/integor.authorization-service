using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrettyUserAuthorizationShared.Services.Security.Password
{
    public interface IPasswordSaltService
    {
        string GenerateSalt();
		string AppendSalt(string password, string salt);
    }
}
