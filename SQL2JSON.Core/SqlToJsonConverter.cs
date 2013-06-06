namespace SQL2JSON.Core
{
    public class SqlToJsonConverter
    {
        protected readonly IDataAccess dataAccess;
        public DataTableToJsonConverter DataTableToJsonConverter { get; set; }

        public SqlToJsonConverter(IDataAccess dataAccess, IJSONSerializer serializer)
        {
            this.dataAccess = dataAccess;
            this.DataTableToJsonConverter = new DataTableToJsonConverter(serializer);
        }

        public string ConvertQuery(string sql)
        {
            var dataTable = dataAccess.Query(sql);
            return DataTableToJsonConverter.Convert(dataTable);
        }
    }
}
