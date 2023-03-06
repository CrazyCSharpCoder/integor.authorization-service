using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspErrorHandling.Converters;

namespace AspErrorHandling.ExceptionsHandling
{
	using Types;
	using Helpers;

    public abstract class InjectableExceptionConverter : ConverterBase
    {
		protected abstract IEnumerable<ExceptionConverterToExceptionType> LookExceptionConvertersHierarchy(Type exceptionType);

		protected IErrorConvertationResult? Convert(object converter, Exception exception)
			=> ExceptionConverterResolver.Convert(converter, exception);

		protected IErrorConvertationResult? AutoConvert(Exception exception)
		{
			IEnumerable<ExceptionConverterToExceptionType> hierarchy = LookExceptionConvertersHierarchy(exception.GetType());

			foreach (ExceptionConverterToExceptionType converterToExc in hierarchy)
			{
				IErrorConvertationResult? errors = ExceptionConverterResolver.Convert(converterToExc.Converter, exception);

				if (errors != null)
					return errors;
			}

			return null;
		}
	}
}
