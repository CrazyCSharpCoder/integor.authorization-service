using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;

using ExtensibleRefreshJwtAuthentication;
using ExtensibleRefreshJwtAuthentication.Refresh;

using IntegorAuthorizationAspShared.ConfigurationProviders;

namespace IntegorAuthorizationAspServices.TokenResolvers
{
	using Internal;

	public class JwtRefreshTokenResolver : IRefreshTokenResolver
	{
		private const string _refreshTokenName = "Refresh";

		private TokenResolvingHelper _helper;

		public JwtRefreshTokenResolver(
			IAuthenticationConfigurationProvider configuration,
			IOptions<ClaimTypeNames> claimTypesOptions)
		{
			_helper = new TokenResolvingHelper(configuration, claimTypesOptions);
		}

		public async Task<string> GenerateTokenAsync(IEnumerable<Claim> claims)
		{
			SecurityTokenDescriptor tokenDescriptor =
				_helper.CreateSecurityTokenDescriptor(claims, _refreshTokenName);

			return _helper.GenerateToken(tokenDescriptor);
		}

		public async Task<IEnumerable<Claim>> ReadTokenAsync(string token)
		{
			TokenValidationParameters validationParameters =
				_helper.CreateTokenValidationParameters();

			return await _helper.ReadTokenAsync(token, validationParameters);
		}
	}
}
