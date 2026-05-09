namespace Lab10.Blue
{
    public interface ISerializer<T> where T : Lab9.Blue.Blue
    {
        void Serialize(T obj);
        T Deserialize();
    }
}
