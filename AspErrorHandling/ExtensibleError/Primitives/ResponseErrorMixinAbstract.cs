using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspErrorHandling.ExtensibleError.Primitives
{
    public abstract class ResponseErrorMixinAbstract
    {
        public string Key { get; }

        public ResponseErrorMixinAbstract(string key)
        {
            Key = key;
        }

        public abstract object GetValue();
    }
}
