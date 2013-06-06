using System;
using System.Data;
using System.Linq;

namespace SQL2JSON.Core
{
    public class DataTableToJsonConverter
    {
        protected readonly IJSONSerializer serializer;

        public DataTableToJsonConverter(IJSONSerializer serializer)
        {
            this.serializer = serializer;
            this.DataTableToObjectsConverter = new DataTableToDynamicsConverter();
        }

        public DataTableToDynamicsConverter DataTableToObjectsConverter { get; set; }

        public string Convert(DataTable source)
        {
            return Convert(source, new NoTransformation());
        }

        public string Convert(DataTable source, ITransformer transformer)
        {
            var objects = DataTableToObjectsConverter.Convert(source);
            var transformedObjects = objects.Select(x => transformer.Transform(x)).ToArray();
            return serializer.Serialize(transformedObjects);
        }

        public class NoTransformation : ITransformer
        {
            public object Transform(object obj)
            {
                return obj;
            }
        }
    }
}