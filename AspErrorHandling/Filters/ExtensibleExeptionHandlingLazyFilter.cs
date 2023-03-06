using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspErrorHandling.Filters
{
    using DefaultImplementations;

    public class ExtensibleExeptionHandlingLazyFilter : Attribute, IExceptionFilter
    {
        private IResponseErrorObjectCompiler _errorsComplier;

        private AutomaticLazyExceptionConverter<Exception> _filterConvertationMechanism;

        public ExtensibleExeptionHandlingLazyFilter(IServiceProvider services, IResponseErrorObjectCompiler errorsComplier,

            params Type[] exceptionConverterTypes)
        {
            _errorsComplier = errorsComplier;

            _filterConvertationMechanism = new AutomaticLazyExceptionConverter<Exception>(services, exceptionConverterTypes);
        }

        public void OnException(ExceptionContext context)
        {
			IErrorConvertationResult? errors;

			errors = _filterConvertationMechanism.Convert(context.Exception);

            if (errors == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                return;
            }

            object responseObject = _errorsComplier.CompileResponse(errors);

            context.Result = new ObjectResult(responseObject)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
