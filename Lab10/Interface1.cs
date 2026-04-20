namespace Lab10;

public interface IWhiteSerializer
{
    void Serialize(Lab9.Purple.Purple obj);
}
public interface IGreenSerializer
{
    void Serialize<T>(T obj) where T : Lab9.Purple.Purple;
}
public interface IBluePurpleSerializer<T> where T : Lab9.Purple.Purple
{
    void Serialize(T obj);
}