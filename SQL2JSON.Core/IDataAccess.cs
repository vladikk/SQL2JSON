using System.Data;

namespace SQL2JSON.Core
{
    public interface IDataAccess
    {
        DataTable Query(string sql);
    }
}