using Lab10.Purple;

namespace Lab10
{
    public class Program
    {
        public static void Main()
        {
            var simpleSerializator = new PurpleTxtFileManager("Example1");
            //simpleSerializator.Serialize(new Lab9.Purple.Purple());

            simpleSerializator = new PurpleTxtFileManager("Example2", "txt");
            //simpleSerializator.Serialize(new Lab9.Purple.Purple());
        }
    }
}