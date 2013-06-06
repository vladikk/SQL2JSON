﻿using System;
using System.Collections.Generic;
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
            var transformedObjects = objects.Select(transformer.Transform).ToArray();
            return serializer.Serialize(transformedObjects);
        }

        public class NoTransformation : ITransformer
        {
            public IDictionary<string, object> Transform(IDictionary<string, object> obj)
            {
                return obj;
            }
        }
    }
}