using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AspErrorHandling;
using AspErrorHandling.Converters;
using AspErrorHandling.ExceptionsHandling;

using Npgsql;

namespace IntegorAuthorizationErrorHandling.ExceptionConverters.DataAccess
{
	public class ObjectDisposedExceptionConverter : LazyInjectableExceptionConverter, IExceptionErrorConverter<ObjectDisposedException>
	{
		public ObjectDisposedExceptionConverter(IServiceProvider services)
			: base(services, typeof(IExceptionErrorConverter<PostgresException>))
		{
		}

		public IErrorConvertationResult? Convert(ObjectDisposedException exception)
		{
			PostgresException? pgException = exception.GetBaseException() as PostgresException;

			if (pgException != null)
				return AutoConvert(pgException);

			return null;
		}
	}
}
