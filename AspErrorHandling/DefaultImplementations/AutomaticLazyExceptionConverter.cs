using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspErrorHandling.DefaultImplementations
{
	using Converters;
	using ExceptionsHandling;

	public class AutomaticLazyExceptionConverter<TException> : LazyInjectableExceptionConverter, IExceptionErrorConverter<TException>
		where TException : Exception
	{
		public AutomaticLazyExceptionConverter(IServiceProvider services, params Type[] exceptionConverterTypes) : base(services, exceptionConverterTypes)
		{
		}

		public IErrorConvertationResult? Convert(TException exception) => AutoConvert(exception);
	}
}
