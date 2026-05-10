namespace Lab10.Interfaces
{
    public interface ISerializer<T> where T : global::Lab9.Purple.Purple
    {
        T Deserialize();
        void Serialize(T obj);
    }
}