using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;

using IntegorAuthorizationAspShared;

using AspErrorHandling;

using static AspErrorHandling.Helpers.ConvertResultShortcuts;

namespace IntegorAuthorization.Middleware
{
	public class StatusCodesHandlingMiddleware : IMiddleware
	{
		private IHttpContextProcessedMarker _processedMarker;
		private IResponseErrorObjectCompiler _errorsCompiler;

		public StatusCodesHandlingMiddleware(
			IHttpContextProcessedMarker processedMarker,
			IResponseErrorObjectCompiler errorsCompiler)
		{
			_processedMarker = processedMarker;
			_errorsCompiler = errorsCompiler;
		}

		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			await next.Invoke(context);

			if (_processedMarker.IsProcessed())
				return;

			HttpResponse response = context.Response;

			object body = GenerateResponseBody(response.StatusCode);
			await response.WriteAsJsonAsync(body);
		}

		private object GenerateResponseBody(int statusCode)
		{
			string codeDescpription = ReasonPhrases.GetReasonPhrase(statusCode);
			IErrorConvertationResult error = Single(codeDescpription);

			return _errorsCompiler.CompileResponse(error);
		}
	}
}
