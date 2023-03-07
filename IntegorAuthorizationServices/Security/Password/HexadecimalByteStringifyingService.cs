using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IntegorAuthorizationShared.Services.Security.Password;

namespace IntegorAuthorizationServices.Security.Password
{
	public class HexadecimalByteStringifyingService : IByteStringifyingService
	{
		public string BytesToString(byte[] bytes)
		{
			IEnumerable<string> strBytes = bytes.Select(b => b.ToString("X2"));
			return string.Join("", strBytes);
		}
	}
}
