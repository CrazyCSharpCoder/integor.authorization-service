using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedJwtAuthentication.Services
{
	public interface IHttpContextTokensAccessor
	{
		string? GetAccessToken();
		string? GetRefreshToken();

		void PutAccessToken(string token);
		void PutRefreshToken(string token);

		void DeleteAccessToken();
		void DeleteRefreshToken();
	}
}
