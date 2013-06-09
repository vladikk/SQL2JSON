namespace SQL2JSON.Core
{
    public class SqlToJsonConverter
    {
        protected readonly IDataAccess dataAccess;
        protected readonly ITransformer transformer;
        public DataTableToJsonConverter DataTableToJsonConverter { get; set; }

        public SqlToJsonConverter(IDataAccess dataAccess, IJSONSerializer serializer, ITransformer transformer=null)
        {
            this.dataAccess = dataAccess;
            this.transformer = transformer;
            this.DataTableToJsonConverter = new DataTableToJsonConverter(serializer);
        }

        public string ConvertQuery(string sql)
        {
            var dataTable = dataAccess.ExecuteQuery(sql);
            return DataTableToJsonConverter.Convert(dataTable, transformer);
        }
    }
}
