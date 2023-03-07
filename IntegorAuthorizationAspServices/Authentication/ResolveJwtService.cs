using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.IdentityModel.Tokens;

using AdvancedJwtAuthentication.Services;

using IntegorAuthorizationAspShared.ConfigurationProviders;

namespace IntegorAuthorizationAspServices.Authentication
{
	using static AuthenticationConstants;

	public class ResolveJwtService : IResolveTokensService
	{
		private class TokensShared
		{
			public string Issuer { get; set; } = null!;
			public string Audience { get; set; } = null!;

			public SigningCredentials Credentials { get; set; } = null!;
		}

		private IAuthenticationConfigurationProvider _config;

		private TokensShared _sharedOptions;

		public ResolveJwtService(
			IAuthenticationConfigurationProvider authConfig)
		{
			_config = authConfig;

			_sharedOptions = new TokensShared()
			{
				Issuer = _config.GetIssuer(),
				Audience = _config.GetAudience(),

				Credentials = new SigningCredentials(
					_config.GetIssuerSigningKey(), _config.GetAlgoritghm())
			};
		}

		public async Task<string> GenerateAccessTokenAsync(IEnumerable<Claim> claims)
		{
			SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
			{
				Issuer = _sharedOptions.Issuer,
				Audience = _sharedOptions.Audience,
				Subject = new ClaimsIdentity(claims),

				SigningCredentials = _sharedOptions.Credentials,

				Expires = DateTime.UtcNow + _config.GetExpirationTime(AccessTokenName)
			};

			return GenerateToken(descriptor);
		}

		public async Task<string> GenerateRefreshTokenAsync(IEnumerable<Claim> claims)
		{
			SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
			{
				Issuer = _sharedOptions.Issuer,
				Audience = _sharedOptions.Audience,
				Subject = new ClaimsIdentity(claims),

				SigningCredentials = _sharedOptions.Credentials,

				Expires = DateTime.UtcNow + _config.GetExpirationTime(RefreshTokenName)
			};

			return GenerateToken(descriptor);
		}

		public async Task<IEnumerable<Claim>> ReadAccessTokenAsync(string token)
		{
			return await ReadTokenAsync(token, BuildTokenValidationParameters());
		}

		public async Task<IEnumerable<Claim>> ReadRefreshTokenAsync(string token)
		{
			return await ReadTokenAsync(token, BuildTokenValidationParameters());
		}

		private string GenerateToken(SecurityTokenDescriptor descriptor)
		{
			JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
			SecurityToken token = handler.CreateToken(descriptor);

			return handler.WriteToken(token);
		}

		private async Task<IEnumerable<Claim>> ReadTokenAsync(
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

		private TokenValidationParameters BuildTokenValidationParameters()
			=> new TokenValidationParameters()
			{
				ValidIssuer = _sharedOptions.Issuer,
				ValidAudience = _sharedOptions.Audience,

				IssuerSigningKey = _sharedOptions.Credentials.Key,
				ValidAlgorithms = new string[] { _sharedOptions.Credentials.Algorithm },
				ClockSkew = TimeSpan.Zero
			};
	}
}
