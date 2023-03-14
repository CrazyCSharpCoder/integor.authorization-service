using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using IntegorErrorsHandling;
using IntegorResponseDecoration;

namespace IntegorAuthorizationResponseDecoration.Decorators
{
	public class ErrorResponseBodyDecorator : IResponseBodyDecorator
	{
		private IResponseErrorObjectCompiler _errorCompiler;

		public ErrorResponseBodyDecorator(IResponseErrorObjectCompiler errorCompiler)
		{
			_errorCompiler = errorCompiler;
		}

		public ResponseBodyDecorationResult Decorate(object? bodyObject)
		{
			if (bodyObject == null)
				return new ResponseBodyDecorationResult(false);

			IEnumerable<IErrorConvertationResult> errors;

			if (bodyObject is IEnumerable<IErrorConvertationResult> bodyErrors)
				errors = bodyErrors;
			else if (bodyObject is IErrorConvertationResult bodyError)
				errors = new IErrorConvertationResult[] { bodyError };
			else
				return new ResponseBodyDecorationResult(false);

			object errorsBody = _errorCompiler.CompileResponse(errors.ToArray());

			return new ResponseBodyDecorationResult(errorsBody);
		}
	}
}
