using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspErrorHandling.ExtensibleError.Mixins
{
    using static ExtensibleErrorMixinsDefaults;

	using Primitives;

    public class MessageErrorMixin : ResponseErrorMixin<string>
	{
		public MessageErrorMixin(string message) : base(MessageMixinKey, message)
		{
		}
	}
}
