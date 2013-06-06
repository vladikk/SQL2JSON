using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using NUnit.Framework;
using SQL2JSON.Infrastructure;

namespace SQL2JSON.Tests
{
    [TestFixture]
    public class JsonDotNetSerializerTests
    {
        [Test]
        public void Serialize_DynamicObjectWithVariousDataTypes_ReturnsValidJSON()
        {
            dynamic data = new ExpandoObject();
            data.Id = 1;
            data.Title = "Hello, world";
            data.Value = 7.17;
            data.CreatedOn = new DateTime(2013, 6, 6, 17, 15, 1, 2);

            var result = new JsonDotNetSerializer().Serialize(data);

            Assert.AreEqual("{\"Id\":1,\"Title\":\"Hello, world\",\"Value\":7.17,\"CreatedOn\":\"2013-06-06T17:15:01.002\"}", result);
        }
    }
}