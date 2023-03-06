﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

using PrettyUserAuthorizationModelOptions.Security;
using PrettyUserAuthorizationShared.Services.Security.Password;

namespace PrettyUserAuthorizationServices.Security.Password
{
	public class PasswordSaltService : IPasswordSaltService
	{
		private IByteStringifyingService _bytesStringifier;

		public PasswordSaltService(IByteStringifyingService bytesStringifier)
		{
			_bytesStringifier = bytesStringifier;
		}

		public string GenerateSalt()
		{
			byte[] salt = RandomNumberGenerator.GetBytes(SecurityOptions.PasswordSaltLength);
			return _bytesStringifier.BytesToString(salt);
		}

		public string AppendSalt(string password, string salt)
		{
			return password + salt;
		}
	}
}
