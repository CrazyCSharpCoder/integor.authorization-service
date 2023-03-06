using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspErrorHandling.ExtensibleError.Primitives
{
    public class ResponseErrorMixin<TValue> : ResponseErrorMixinAbstract
        where TValue : notnull
    {
        public TValue Value { get; }

        public ResponseErrorMixin(string key, TValue value) : base(key)
        {
            Value = value;
        }

        public sealed override object GetValue()
        {
            return Value;
        }
    }
}
