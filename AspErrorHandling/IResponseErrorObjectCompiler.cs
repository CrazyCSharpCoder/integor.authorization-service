using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspErrorHandling
{
	public interface IResponseErrorObjectCompiler
	{
		object CompileResponse(params IErrorConvertationResult[] errors);
	}
}
