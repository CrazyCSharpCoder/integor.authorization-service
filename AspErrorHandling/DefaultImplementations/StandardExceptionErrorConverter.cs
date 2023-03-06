using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspErrorHandling.DefaultImplementations
{
	using Converters;

	public class StandardExceptionErrorConverter :
		ConverterBase, IExceptionErrorConverter<Exception>
	{
		public IErrorConvertationResult Convert(Exception exc)
		{
			return Single("Internal server error");
		}
	}
}
