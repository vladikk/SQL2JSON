using System.Collections.Generic;
using System.Data;

namespace SQL2JSON.Core
{
    public class DataTableToDynamicsConverter
    {
        public virtual Dictionary<string, object>[] Convert(DataTable source)
        {
            var result = new Dictionary<string, object>[source.Rows.Count];

            for (int i = 0; i < source.Rows.Count; i++)
            {
                var row = source.Rows[i];
                var obj = ConvertRowToDictionary(row, i);
                result[i] = obj;
            }

            return result;
        }

        private Dictionary<string, object> ConvertRowToDictionary(DataRow source, int i)
        {
            var dict = new Dictionary<string, object>();

            for (int j = 0; j < source.Table.Columns.Count; j++)
            {
                var column = source.Table.Columns[j];
                dict[column.ColumnName] = source[column.ColumnName];
            }

            return dict;
        }
    }
}
