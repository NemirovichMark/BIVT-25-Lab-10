namespace Lab10
{
    public class Program
    {
        public static void Main()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),"aboba", "abobus.txt");
            
            Console.WriteLine(path);
            Console.WriteLine(Directory.GetParent(path));
        }
    }
}