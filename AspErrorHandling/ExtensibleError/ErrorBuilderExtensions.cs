using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspErrorHandling.ExtensibleError
{
	using Mixins;

	public static class ErrorBuilderExtensions
	{
		public static ErrorsBuilder AddKey(this ErrorsBuilder builder, string value)
			=> builder.AddMixin(new KeyErrorMixin(value));

		public static ErrorsBuilder AddMessage(this ErrorsBuilder builder, string message)
			=> builder.AddMixin(new MessageErrorMixin(message));

		public static ErrorsBuilder AddMessages(this ErrorsBuilder builder, IEnumerable<string> messages)
			=> builder.AddMixin(new MessagesErrorMixin(messages));
	}
}
