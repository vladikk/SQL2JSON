using SQL2JSON.Core;
using SQL2JSON.Infrastructure;

namespace SQL2JSON
{
    public class Program
    {
        static void Main(string[] args)
        {
            var connectionString = args[0];
            var query = args[1];
            var outputFile = args[2];

            var converter = new SqlToJsonConverter(new ADONetDataAccess(connectionString), new JsonDotNetSerializer());
            
            var json = converter.ConvertQuery(query);
            System.IO.File.WriteAllText(outputFile, json);
        }
    }
}
