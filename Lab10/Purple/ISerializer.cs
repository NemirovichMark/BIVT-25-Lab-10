namespace Lab10;

public interface ISerializer<T> where T : Lab9.Purple.Purple
{
    public T Deserialize();
    public void Serialize(T obj);
}