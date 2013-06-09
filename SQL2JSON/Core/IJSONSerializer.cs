using System.IO;

namespace SQL2JSON.Core
{
    public interface IJSONSerializer
    {
        string Serialize(object obj);
        void Serialize(object obj, StreamWriter writer);
    }
}