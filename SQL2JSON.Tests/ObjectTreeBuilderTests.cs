using System.Collections.Generic;
using NUnit.Framework;
using SQL2JSON.Core;

namespace SQL2JSON.Tests
{
    [TestFixture]
    public class ObjectTreeBuilderTests
    {
        [Test]
        public void Test()
        {
            var objDict = new Dictionary<string, object>();
            objDict["key"] = "abcd";
            objDict["value::id"] = 1;
            objDict["value::name"] = "john johnson";

            var treeBuilder = new ObjectTreeBuilder();
            var splittedObject = treeBuilder.Split(objDict, "::");
            
            Assert.AreEqual("abcd", splittedObject["key"]);
            Assert.AreEqual(1, ((IDictionary<string, object>)splittedObject["value"])["id"]);
            Assert.AreEqual("john johnson", ((IDictionary<string, object>)splittedObject["value"])["name"]);
        }
    }
}