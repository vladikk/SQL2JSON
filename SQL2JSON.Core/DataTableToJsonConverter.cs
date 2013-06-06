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
            return Convert(source, x => x);
        }

        public string Convert(DataTable source, Func<object, object> transformer)
        {
            var objects = DataTableToObjectsConverter.Convert(source);
            var transformedObjects = objects.Select(x => transformer(x)).ToArray();
            return serializer.Serialize(transformedObjects);
        }
    }
}