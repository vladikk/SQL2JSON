using System;
using System.Data;
using NUnit.Framework;
using SQL2JSON.Core;

namespace SQL2JSON.Tests
{
    [TestFixture]
    public class DataTableToDynamicsConverterTests
    {
        [Test]
        public void Convert_TableWithTwoRows_ReturnsArrayOfTwoDynamicObjects()
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof (int));
            dataTable.Columns.Add("Title", typeof(string));
            dataTable.Columns.Add("Value", typeof(decimal));
            dataTable.Columns.Add("Active", typeof(bool));
            dataTable.Columns.Add("CreatedOn", typeof(DateTime));

            var row = dataTable.NewRow();
            row["Id"] = 1;
            row["Title"] = "Hello, world";
            row["Value"] = 7.1;
            row["Active"] = true;
            row["CreatedOn"] = new DateTime(2013, 6, 6, 17, 15, 1, 2);
            dataTable.Rows.Add(row);

            row = dataTable.NewRow();
            row["Id"] = 2;
            row["Title"] = "Hello, world!";
            row["Value"] = 17.7;
            row["Active"] = true;
            row["CreatedOn"] = new DateTime(2012, 7, 7, 7, 15, 1, 2);
            dataTable.Rows.Add(row);

            var converter = new DataTableToDynamicsConverter();

            var result = converter.Convert(dataTable);

            Assert.AreEqual(2, result.Length);
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual("Hello, world", result[0].Title);
            Assert.AreEqual(7.1, result[0].Value);
            Assert.AreEqual(true, result[0].Active);
            Assert.AreEqual(new DateTime(2013, 6, 6, 17, 15, 1, 2), result[0].CreatedOn);

            Assert.AreEqual(2, result[1].Id);
            Assert.AreEqual("Hello, world!", result[1].Title);
            Assert.AreEqual(17.7, result[1].Value);
            Assert.AreEqual(true, result[1].Active);
            Assert.AreEqual(new DateTime(2012, 7, 7, 7, 15, 1, 2), result[1].CreatedOn);
        }
    }
}
