namespace SQL2JSON.Core
{
    public interface ITransformer
    {
        object Transform(object obj);
    }
}