using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspErrorHandling.Exceptions
{
	public class InvalidConverterException : ApplicationException
	{
		public InvalidConverterException(string message) : base(message)
		{
		}

		public InvalidConverterException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
