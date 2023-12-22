using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegorAuthorizationShared.Services.Security.Password
{
    public interface IPasswordEncryptionService
    {
        string Encrypt(string password);
    }
}
