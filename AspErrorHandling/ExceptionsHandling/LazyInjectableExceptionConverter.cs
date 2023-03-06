using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

namespace AspErrorHandling.ExceptionsHandling
{
	using Types;
    using Helpers;

    public abstract class LazyInjectableExceptionConverter : InjectableExceptionConverter
    {
        private IServiceProvider _services;
        private Type[] _converterTypes;

        public LazyInjectableExceptionConverter(IServiceProvider services, params Type[] converterTypes)
        {
            _services = services;
            _converterTypes = converterTypes;
        }

		protected sealed override IEnumerable<ExceptionConverterToExceptionType> LookExceptionConvertersHierarchy(Type exceptionType)
		{
			IEnumerable<Type> hierarchy = ExceptionConverterResolver.LookExceptionConverterTypesHierarchy(_converterTypes, exceptionType);

			return hierarchy.Select(converterType => SelectConverter(converterType));
		}

		private ExceptionConverterToExceptionType SelectConverter(Type converterType)
		{
			object converter = _services.GetRequiredService(converterType);
			Type exceptionType = ExceptionConverterReflector.GetConverterExceptionType(converterType)!;

			return new ExceptionConverterToExceptionType(converter, exceptionType);
		}
	}
}
