namespace Lab10
{
    public interface ISerializer<T>
    {
        void Serialize(T obj);
        T? Deserialize();
    }
}
