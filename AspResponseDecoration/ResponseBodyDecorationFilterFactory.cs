using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspResponseDecoration
{
	public class ResponseBodyDecorationFilterFactory : Attribute, IFilterFactory
	{
		public bool IsReusable => false;

		private IEnumerable<Type> _decoratorTypes;

		public ResponseBodyDecorationFilterFactory(params Type[] decoratorTypes)
		{
			_decoratorTypes = decoratorTypes;
		}

		public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
		{
			IEnumerable<IResponseBodyDecorator> decorators = _decoratorTypes.Select(
				decType => (IResponseBodyDecorator)serviceProvider.GetRequiredService(decType));

			Type filterType = typeof(ResponseBodyDecorationFilter);
			return (Activator.CreateInstance(filterType, decorators) as IFilterMetadata)!;
		}

		private class ResponseBodyDecorationFilter : IActionFilter
		{
			private IEnumerable<IResponseBodyDecorator> _decorators;

			public ResponseBodyDecorationFilter(IEnumerable<IResponseBodyDecorator> decorators)
			{
				_decorators = decorators;
			}

			public void OnActionExecuting(ActionExecutingContext context)
			{
			}

			public void OnActionExecuted(ActionExecutedContext context)
			{
				if (context.Result == null)
					return;

				ObjectResult? result = context.Result as ObjectResult;

				if (result == null)
					return;

				result.Value = Decorate(_decorators, result.Value);
			}

			private object? Decorate(IEnumerable<IResponseBodyDecorator> decorators, object? body)
			{
				foreach (IResponseBodyDecorator decorator in decorators)
				{
					ResponseBodyDecorationResult decorationResult = decorator.Decorate(body);

					if (decorationResult.Success)
						return decorationResult.NewBody;
				}

				return body;
			} 
		}
	}
}
