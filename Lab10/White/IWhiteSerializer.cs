namespace Lab10
{
    public interface IWhiteSerializer
    {
        Lab9.White.White Deserialize();
        void Serialize(Lab9.White.White obj);
    }
}
