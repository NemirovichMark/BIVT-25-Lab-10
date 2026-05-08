namespace Lab10;

public interface ISerializer<T> where T:Lab9.Blue.Blue
{
  T Deserealize();
  void Serialize(T obj);
}
