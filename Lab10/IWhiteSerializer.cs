using Lab9.White;
using Lab10.White;

namespace Lab10.White
{
    public interface IWhiteSerializer
    {
        void Serialize(Lab9.White.White obj);
        Lab9.White.White Deserialize();
    }
}
