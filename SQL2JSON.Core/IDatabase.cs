using System.Data;

namespace SQL2JSON.Core
{
    public interface IDatabase
    {
        DataTable Query(string sql);
    }
}