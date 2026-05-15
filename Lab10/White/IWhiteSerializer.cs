using Lab10.White;
using Lab10;

namespace Lab10.White
{
    public interface IWhiteSerializer 
    {
        Lab10.White.White Deserialize();
        void Serialize(Lab10.White.White obj);
    }
}
