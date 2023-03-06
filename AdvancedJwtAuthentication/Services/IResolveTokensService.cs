using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Claims;

namespace AdvancedJwtAuthentication.Services
{
    public interface IResolveTokensService
    {
        Task<string> GenerateAccessTokenAsync(IEnumerable<Claim> claims);
        Task<IEnumerable<Claim>> ReadAccessTokenAsync(string token);

		Task<string> GenerateRefreshTokenAsync(IEnumerable<Claim> claims);
		Task<IEnumerable<Claim>> ReadRefreshTokenAsync(string token);
	}
}
