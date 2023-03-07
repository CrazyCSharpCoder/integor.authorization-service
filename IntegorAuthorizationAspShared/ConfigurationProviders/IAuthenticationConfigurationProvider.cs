using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.IdentityModel.Tokens;

namespace IntegorAuthorizationAspShared.ConfigurationProviders
{
    public interface IAuthenticationConfigurationProvider
    {
        SecurityKey GetIssuerSigningKey();
        string GetAlgoritghm();

        string GetIssuer();
        string GetAudience();

        TimeSpan GetExpirationTime(string tokenName);
        string GetCookieName(string tokenName);
    }
}
