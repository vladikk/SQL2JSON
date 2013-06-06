using System.Data;
using System.Data.SqlClient;
using SQL2JSON.Core;

namespace SQL2JSON.Infrastructure
{
    public class ADONetDataAccess : IDataAccess
    {
        protected readonly string connectionString;

        public ADONetDataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DataTable Query(string sql)
        {
            var result = new DataTable();
            var conn = new SqlConnection(connectionString);
            var cmd = new SqlCommand(sql, conn);
            conn.Open();
            var da = new SqlDataAdapter(cmd);
            da.Fill(result);
            conn.Close();
            da.Dispose();

            return result;
        }
    }
}
