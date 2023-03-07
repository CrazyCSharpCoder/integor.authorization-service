using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using PrettyUserAuthorizationAspShared.ConfigurationProviders;

using AdvancedJwtAuthentication.Services;

namespace PrettyUserAuthorizationAspServices.Authentication
{
	using static AuthenticationConstants;

	public class CookieTokensAccessor : IHttpContextTokensAccessor
	{
		private IAuthenticationConfigurationProvider _config;
		private HttpContext _http;

		private string _accessCookieName;
		private string _refreshCookieName;

		public CookieTokensAccessor(
			IAuthenticationConfigurationProvider config,
			IHttpContextAccessor httpAccessor)
		{
			_config = config;
			_http = httpAccessor.HttpContext;

			_accessCookieName = _config.GetCookieName(AccessTokenName);
			_refreshCookieName = _config.GetCookieName(RefreshTokenName);
		}

		public string? GetAccessToken()
		{
			_http.Request.Cookies.TryGetValue(_accessCookieName, out string token);

			return token;
		}

		public string? GetRefreshToken()
		{
			_http.Request.Cookies.TryGetValue(_refreshCookieName, out string token);

			return token;
		}

		public void PutAccessToken(string token)
		{
			AttachCookieToken(_accessCookieName, token);
		}

		public void PutRefreshToken(string token)
		{
			AttachCookieToken(_refreshCookieName, token);
		}

		public void DeleteAccessToken()
		{
			DeleteCookieToken(_accessCookieName);
		}

		public void DeleteRefreshToken()
		{
			DeleteCookieToken(_refreshCookieName);
		}

		private void AttachCookieToken(string cookieName, string token)
		{
			CookieOptions options = new CookieOptions()
			{
				HttpOnly = true
				// TODO make secure
			};

			_http.Response.Cookies.Append(cookieName, token, options);
		}

		private void DeleteCookieToken(string cookieName)
		{
			_http.Response.Cookies.Delete(cookieName);
		}
	}
}
