using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

using IntegorAuthorizationShared.Services.Security.Password;

namespace IntegorAuthorizationServices.Security.Password
{
    public class PasswordEncryptionService : IPasswordEncryptionService
    {
		private IByteStringifyingService _byteStringifier;

		public PasswordEncryptionService(IByteStringifyingService byteStringifier)
		{
			_byteStringifier = byteStringifier;
		}

        public string Encrypt(string password)
        {
			byte[] pwdBytes = Encoding.ASCII.GetBytes(password);

			HashAlgorithm algorithm = SHA256.Create();
			byte[] hash = algorithm.ComputeHash(pwdBytes);

			return _byteStringifier.BytesToString(hash);
        }
    }
}
