using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspErrorHandling.Converters
{
    public interface IErrorConverter<TErrorObject>
    {
        IErrorConvertationResult? Convert(TErrorObject errorObject);
    }
}
