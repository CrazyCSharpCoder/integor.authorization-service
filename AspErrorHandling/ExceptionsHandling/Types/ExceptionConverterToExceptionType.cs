using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspErrorHandling.ExceptionsHandling.Types
{
	public class ExceptionConverterToExceptionType
	{
		public object Converter { get; }
		public Type ExceptionType { get; }

		internal ExceptionConverterToExceptionType(object converter, Type exceptionType)
		{
			Converter = converter;
			ExceptionType = exceptionType;
		}
	}
}
