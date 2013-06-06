using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using FakeItEasy;
using FakeItEasy.Configuration;
using NUnit.Framework;
using SQL2JSON.Core;

namespace SQL2JSON.Tests
{
    [TestFixture]
    public class DataTableToJsonConverterTests
    {
        private DataTable dataTable;
        private Dictionary<string, object>[] arrayOfObjects;
        private string json;
        private DataTableToDynamicsConverter dataTableToObjectsConverter;
        private IJSONSerializer serializer;

        [SetUp]
        public void SetUp()
        {
            dataTable = A.Fake<DataTable>();
            arrayOfObjects = new [] {
                new Dictionary<string, object> { { "Id", 1 } }, 
                new Dictionary<string, object> { { "Id", 2 } }, 
            };
            json = "json";
            dataTableToObjectsConverter = A.Fake<DataTableToDynamicsConverter>();
            serializer = A.Fake<IJSONSerializer>();
            A.CallTo(() => dataTableToObjectsConverter.Convert(dataTable)).Returns(arrayOfObjects);
        }

        [Test]
        public void Convert_DataTable_ReturnsJsonString()
        {
            FakeCallToSerializer(arrayOfObjects).Returns(json);

            var result = MakeConverter().Convert(dataTable);

            Assert.AreEqual(json, result);
            FakeCallToSerializer(arrayOfObjects).MustHaveHappened();
            A.CallTo(() => dataTableToObjectsConverter.Convert(dataTable)).MustHaveHappened();
        }

        [Test]
        public void Convert_DataTableWithCustomConverter_ReturnsJsonString()
        {
            var transformedObjects = new [] {
                new Dictionary<string, object> { { "TransformedId", 1 } }, 
                new Dictionary<string, object> { { "TransformedId", 2 } }, 
            };
            var transformer = A.Fake<ITransformer>();

            A.CallTo(() => transformer.Transform(arrayOfObjects[0])).Returns(transformedObjects[0]);
            A.CallTo(() => transformer.Transform(arrayOfObjects[1])).Returns(transformedObjects[1]);
            FakeCallToSerializer(transformedObjects).Returns(json);

            var result = MakeConverter().Convert(dataTable, transformer);

            Assert.AreEqual(json, result);
            FakeCallToSerializer(transformedObjects).MustHaveHappened();
            A.CallTo(() => dataTableToObjectsConverter.Convert(dataTable)).MustHaveHappened();
        }

        private DataTableToJsonConverter MakeConverter()
        {
            var converter = new DataTableToJsonConverter(serializer);
            converter.DataTableToObjectsConverter = dataTableToObjectsConverter;
            return converter;
        }

        private IReturnValueConfiguration<string> FakeCallToSerializer(IEnumerable<dynamic> expectedArgument)
        {
            return A.CallTo(() => serializer.Serialize(arrayOfObjects)).WhenArgumentsMatch(x => expectedArgument.SequenceEqual((IEnumerable<dynamic>)x[0]));
        }
    }
}