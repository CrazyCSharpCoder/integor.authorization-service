using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.IdentityModel.Tokens;

using ExtensibleRefreshJwtAuthentication;
using IntegorAspHelpers.MicroservicesInteraction.Authorization;

using IntegorAuthorizationAspShared.ConfigurationProviders;

namespace IntegorAuthorizationAspServices.Internal
{ 
	internal class TokenResolvingHelper
	{
		private IAuthenticationConfigurationProvider _config;
		private IClaimTypesNamer _claimTypes;

		public TokenResolvingHelper(
			IAuthenticationConfigurationProvider configuration,
			IClaimTypesNamer claimTypes)
        {
			_config = configuration;
			_claimTypes = claimTypes;
		}

        public TokenValidationParameters CreateTokenValidationParameters()
		{
			SigningCredentials credentials = CreateSigningCredentials(_config);

			return new TokenValidationParameters()
			{
				ValidIssuer = _config.GetIssuer(),
				ValidAudience = _config.GetAudience(),

				IssuerSigningKey = credentials.Key,
				ValidAlgorithms = new string[] { credentials.Algorithm },
				ClockSkew = TimeSpan.Zero
			};
		}

		public SecurityTokenDescriptor CreateSecurityTokenDescriptor(
			IEnumerable<Claim> claims, string tokenConfigName)
		{
			return new SecurityTokenDescriptor()
			{
				Issuer = _config.GetIssuer(),
				Audience = _config.GetAudience(),
				Subject = new ClaimsIdentity(claims, null, _claimTypes.UsernameClaimType, _claimTypes.UserRoleClaimType),

				SigningCredentials = CreateSigningCredentials(_config),

				Expires = DateTime.UtcNow + _config.GetExpirationTime(tokenConfigName)
			};
		}

		public string GenerateToken(SecurityTokenDescriptor descriptor)
		{
			JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
			SecurityToken token = handler.CreateToken(descriptor);

			return handler.WriteToken(token);
		}

		public async Task<IEnumerable<Claim>> ReadTokenAsync(
			string token, TokenValidationParameters parameters)
		{
			JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

			TokenValidationResult validationResult;

			validationResult = await handler.ValidateTokenAsync(token, parameters);

			if (!validationResult.IsValid)
			{
				throw validationResult.Exception;
			}

			JwtSecurityToken? jwt = (validationResult.SecurityToken as JwtSecurityToken)!;

			return jwt.Claims;
		}

		private static SigningCredentials CreateSigningCredentials(IAuthenticationConfigurationProvider configuration)
			=> new SigningCredentials(configuration.GetIssuerSigningKey(), configuration.GetAlgoritghm());
	}
}
