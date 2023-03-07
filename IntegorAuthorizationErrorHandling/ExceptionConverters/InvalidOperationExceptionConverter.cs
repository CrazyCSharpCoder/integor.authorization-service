using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

using AspErrorHandling;
using AspErrorHandling.Converters;
using AspErrorHandling.ExceptionsHandling;

namespace PrettyUserAuthorizationErrorHandling.ExceptionConverters
{
	public class InvalidOperationExceptionConverter : LazyInjectableExceptionConverter, IExceptionErrorConverter<InvalidOperationException>
	{
		public InvalidOperationExceptionConverter(IServiceProvider services)
			: base(services, typeof(IExceptionErrorConverter<SocketException>))
		{
		}

		public IErrorConvertationResult? Convert(InvalidOperationException exception)
		{
			Exception baseException = exception.GetBaseException();
			IErrorConvertationResult? result = AutoConvert(baseException);

			return result;
		}
	}
}
