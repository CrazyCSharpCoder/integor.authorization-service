using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspErrorHandling.ExceptionsHandling
{
	using Types;
	using Helpers;

    public abstract class CachedInjectableExceptionConverter : InjectableExceptionConverter
    {
        private object[] _converters;

        public CachedInjectableExceptionConverter(params object[] converters)
        {
            _converters = converters;
        }

		protected sealed override IEnumerable<ExceptionConverterToExceptionType> LookExceptionConvertersHierarchy(Type exceptionType)
		{
			return ExceptionConverterResolver.LookExceptionConvertersHierarchy(_converters, exceptionType);
		}
	}
}
