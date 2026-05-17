using Lab9.Blue;

namespace Lab10
{
    public interface ISerializer<T> where T : Blue
    {
        void Serialize(T obj);
        T Deserialize();
    }
}
