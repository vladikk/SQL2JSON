namespace SQL2JSON.Core
{
    public interface IJSONSerializer
    {
        string Serialize(object obj);
    }
}