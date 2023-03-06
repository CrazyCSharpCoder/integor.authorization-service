using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspErrorHandling.ExtensibleError
{
	using Primitives;

    public class ErrorsBuilder
	{
		private Dictionary<string, ResponseErrorMixinAbstract> _keysToMixins =
			new Dictionary<string, ResponseErrorMixinAbstract>();

		public ErrorsBuilder AddMixin<TMixinValue>(ResponseErrorMixin<TMixinValue> mixin)
			where TMixinValue : notnull
		{
			if (_keysToMixins.ContainsKey(mixin.Key))
				throw new ArgumentException(
					$"Mixin with key \"{mixin.Key}\" is already present. " +
					$"All mixin keys must be unique");

			_keysToMixins[mixin.Key] = mixin;

			return this;
		}

		public IResponseError Build() => new ExtensibleResponseError(_keysToMixins.Values);
	}
}
