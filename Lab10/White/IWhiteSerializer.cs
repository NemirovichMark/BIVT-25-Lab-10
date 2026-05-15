using Lab10.White;
using Lab10;

namespace Lab10.White
{
    public interface IWhiteSerializer
    {
        void Serialize(Lab9.White.White obj); 
        Lab9.White.White Deserialize();       
    }
}
