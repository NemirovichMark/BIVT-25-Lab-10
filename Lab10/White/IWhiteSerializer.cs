using Lab10.White;
using Lab10;

namespace Lab10.White
{
    public interface IWhiteSerializer 
    {
        Lab9.White.White Deserialize();
        void Serialize(Lab9.White.White obj);
    }
}
