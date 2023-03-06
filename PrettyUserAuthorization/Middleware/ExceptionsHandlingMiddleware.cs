using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;

using AspErrorHandling;
using static AspErrorHandling.Helpers.ConvertResultShortcuts;

namespace PrettyUserAuthorization.Middleware
{
	public class ExceptionsHandlingMiddleware : IMiddleware
	{
		private IResponseErrorObjectCompiler _errorsCompiler;

		public ExceptionsHandlingMiddleware(IResponseErrorObjectCompiler errorsCompiler)
		{
			_errorsCompiler = errorsCompiler;
		}

		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{
				await next.Invoke(context);
			}
			catch
			{
				await SendErrorAsync(context.Response);
			}
		}

		private async Task SendErrorAsync(HttpResponse response)
		{
			response.Clear();

			response.StatusCode = StatusCodes.Status500InternalServerError;

			object body = GenerateErrorBody();
			await response.WriteAsJsonAsync(body);
		}

		private object GenerateErrorBody()
		{
			string errorMessage = ReasonPhrases.GetReasonPhrase(
				StatusCodes.Status500InternalServerError);
			IErrorConvertationResult error = Single(errorMessage);

			return _errorsCompiler.CompileResponse(error);
		}
	}
}
