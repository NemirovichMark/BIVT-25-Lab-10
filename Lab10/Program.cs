using Lab10.White;
using Lab9.Purple;

namespace Lab10
{
    public class Program
    {
        public static void Main()
        {
            var obj = new Lab9.Purple.Task1($"maksim fediarin");
            var simpleSerializator = new WhiteTxtFileManager("Example1");
            simpleSerializator.Serialize(obj);
            // simpleSerializator = new WhiteTxtFileManager("Example2","txt");
            // simpleSerializator.Serialize(new Lab9.Purple.Purple);
        }
    }
}