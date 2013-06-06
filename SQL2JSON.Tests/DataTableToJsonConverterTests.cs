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
        private dynamic[] arrayOfObjects;
        private string json;
        private DataTableToDynamicsConverter dataTableToObjectsConverter;
        private IJSONSerializer serializer;

        [SetUp]
        public void SetUp()
        {
            dataTable = A.Fake<DataTable>();
            arrayOfObjects = new dynamic[] { "object1", "object2" };
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
            var transformedObjects = new dynamic[] { "super-object-1", "super-object-2" };
            var transformer = A.Fake<Func<object, object>>();

            A.CallTo(() => transformer.Invoke("object1")).Returns("super-object-1");
            A.CallTo(() => transformer.Invoke("object2")).Returns("super-object-2");
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