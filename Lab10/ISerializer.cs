namespace Lab10;

public interface ISerializer<T> where T : Lab9.Blue.Blue
{
    T Deserialize();
    void Serialize(T obj);
}