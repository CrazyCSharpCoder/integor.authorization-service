using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PrettyUserAuthorizationModel;

namespace PrettyUserAuthorizationShared.Services.Security
{
    public interface ISecurityDataAccessService
    {
		Task<SecurityData?> GetSecurityDataAsync(int userId);
    }
}
