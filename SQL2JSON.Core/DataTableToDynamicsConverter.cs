using System.Collections.Generic;
using System.Data;
using System.Dynamic;

namespace SQL2JSON.Core
{
    public class DataTableToDynamicsConverter
    {
        public virtual dynamic[] Convert(DataTable source)
        {
            var result = new dynamic[source.Rows.Count];

            for (int i = 0; i < source.Rows.Count; i++)
            {
                var row = source.Rows[i];
                var obj = ConvertRowToObject(row, i);
                result[i] = obj;
            }

            return result;
        }

        private dynamic ConvertRowToObject(DataRow source, int i)
        {
            var obj = new ExpandoObject();
            var dict = (IDictionary<string, object>) obj;

            for (int j = 0; j < source.Table.Columns.Count; j++)
            {
                var column = source.Table.Columns[j];
                dict[column.ColumnName] = source[column.ColumnName];
            }
            return obj;
        }
    }
}
