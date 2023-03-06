using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspErrorHandling.ConvertationResults
{
	public class MultipleErrorConvertationResult : IErrorConvertationResult
	{
		private IEnumerable<IResponseError> _errors;

		public MultipleErrorConvertationResult(IEnumerable<IResponseError> errors)
		{
			_errors = errors;
		}

		public IEnumerable<IResponseError> GetErrors()
			=> _errors;
	}
}
