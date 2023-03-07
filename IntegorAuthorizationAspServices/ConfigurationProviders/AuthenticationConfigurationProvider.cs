using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using Microsoft.IdentityModel.Tokens;

using IntegorAuthorizationAspShared.ConfigurationProviders;

namespace IntegorAuthorizationAspServices.ConfigurationProviders
{
	public class AuthenticationConfigurationProvider : IAuthenticationConfigurationProvider
    {
        private IConfiguration _configuration;

        public AuthenticationConfigurationProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SecurityKey GetIssuerSigningKey()
        {
            string strKey = _configuration["Authentication:CookieJwt:SecretKey"];
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(strKey));
        }

        public string GetAlgoritghm()
        {
            return _configuration["Authentication:CookieJwt:Algorithm"];
        }

        public string GetIssuer()
        {
            return _configuration["Authentication:CookieJwt:Issuer"];
        }

        public string GetAudience()
        {
            return _configuration["Authentication:CookieJwt:Issuer"];
        }

        public TimeSpan GetExpirationTime(string tokenName)
        {
            return TimeSpan.Parse(_configuration[$"Authentication:CookieJwt:{tokenName}:Exp"]);
        }

        public string GetCookieName(string tokenName)
        {
            return _configuration[$"Authentication:CookieJwt:{tokenName}:CookieName"];
        }
    }
}
