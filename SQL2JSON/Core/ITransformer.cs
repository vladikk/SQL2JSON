using System.Collections.Generic;

namespace SQL2JSON.Core
{
    public interface ITransformer
    {
        IDictionary<string, object> Transform(IDictionary<string, object> obj);
    }
}