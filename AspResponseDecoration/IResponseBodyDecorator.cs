using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspResponseDecoration
{
	public interface IResponseBodyDecorator
	{
		ResponseBodyDecorationResult Decorate(object? bodyObject);
	}
}
