using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;

namespace AspErrorHandling.ExtensibleError.Primitives
{
    public class ExtensibleResponseError : IResponseError
    {
        private ResponseErrorMixinAbstract[] _mixins;

        public ExtensibleResponseError(IEnumerable<ResponseErrorMixinAbstract> mixins)
        {
            _mixins = mixins.OrderBy(mixin => mixin.Key).ToArray();
        }

        public object ToResponseObject()
        {
            IDictionary<string, object> result = new ExpandoObject()!;

            foreach (ResponseErrorMixinAbstract mixin in _mixins)
                result.Add(mixin.Key, mixin.GetValue());

            return result;
        }
    }
}
