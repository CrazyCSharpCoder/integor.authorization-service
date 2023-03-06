using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspErrorHandling.ConvertationResults
{
	public class SingleErrorConvertationResult : IErrorConvertationResult
	{
		public IResponseError Error { get; }

		public SingleErrorConvertationResult(IResponseError error)
		{
			Error = error;
		}

		public IEnumerable<IResponseError> GetErrors()
		{
			yield return Error;
		}
	}
}
