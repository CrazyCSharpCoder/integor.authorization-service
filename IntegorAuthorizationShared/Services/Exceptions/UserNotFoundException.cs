using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IntegorAuthorizationShared.Services.Exceptions
{
	public class UserNotFoundException : ApplicationException
	{
		public UserNotFoundException(string message) : base(message)
		{
		}

		public UserNotFoundException(string message, Exception? innerException) : base(message, innerException)
		{
		}

		UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
