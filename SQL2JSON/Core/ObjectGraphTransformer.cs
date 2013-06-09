using System.Collections.Generic;

namespace SQL2JSON.Core
{
    public class ObjectGraphTransformer : ITransformer
    {
        protected readonly string delimeter;
        protected readonly ObjectGraphBuilder graphBuilder;

        public ObjectGraphTransformer(string delimeter)
        {
            this.delimeter = delimeter;
            this.graphBuilder = new ObjectGraphBuilder();
        }

        public IDictionary<string, object> Transform(IDictionary<string, object> obj)
        {
            return graphBuilder.Split(obj, delimeter);
        }
    }
}