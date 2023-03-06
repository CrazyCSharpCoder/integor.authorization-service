using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspErrorHandling.DefaultImplementations
{
	using Converters;

    public class StandardStringErrorConverter : ConverterBase, IStringErrorConverter
	{
		public IErrorConvertationResult Convert(string message)
		{
			return Single(message);
		}
	}
}
