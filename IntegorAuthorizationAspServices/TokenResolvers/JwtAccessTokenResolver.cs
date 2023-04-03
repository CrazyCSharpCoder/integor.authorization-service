﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

using Microsoft.IdentityModel.Tokens;

using ExtensibleRefreshJwtAuthentication;
using ExtensibleRefreshJwtAuthentication.Access.Tokens;

using IntegorAuthorizationAspShared.ConfigurationProviders;

namespace IntegorAuthorizationAspServices.TokenResolvers
{
	using Internal;

	public class JwtAccessTokenResolver : IAccessTokenResolver
	{
		private const string _accessTokenName = "Access";

		private TokenResolvingHelper _helper;

		public JwtAccessTokenResolver(
			IAuthenticationConfigurationProvider configuration,
			IClaimTypesNamer claimTypes)
		{
			_helper = new TokenResolvingHelper(configuration, claimTypes);
		}

		public async Task<string> GenerateTokenAsync(IEnumerable<Claim> claims)
		{
			SecurityTokenDescriptor tokenDescriptor =
				_helper.CreateSecurityTokenDescriptor(claims, _accessTokenName);

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
