using System;
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

            Assert.AreEqual("{\r\n  \"Id\": 1,\r\n  \"Title\": \"Hello, world\",\r\n  \"Value\": 7.17,\r\n  \"CreatedOn\": \"2013-06-06T17:15:01.002\"\r\n}", result);
        }
    }
}