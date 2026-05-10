namespace Lab10
{
    public interface ISerializer<T> where T : Lab10.Blue.Blue
    {
        void Serialize(T obj);
        T Deserialize();
    }
}
