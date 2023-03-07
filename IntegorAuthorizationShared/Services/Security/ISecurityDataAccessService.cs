using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IntegorAuthorizationModel;

namespace IntegorAuthorizationShared.Services.Security
{
    public interface ISecurityDataAccessService
    {
		Task<SecurityData?> GetSecurityDataAsync(int userId);
    }
}
