using Lab10.Blue;
namespace Lab10
{
    public class Program
    {
        public static void Main()
        {
            var manager = new BlueJsonFileManager<Lab9.Blue.Blue>("test", "TestJson", "task", "json");

            var task = new Lab9.Blue.Task2("самолет играет важную роль", "иг");
            task.Review();

            manager.Serialize(task);

            var result = manager.Deserialize();

            Console.WriteLine(File.Exists(manager.FullPath));
            Console.WriteLine(task.ToString());
            Console.WriteLine(result.ToString());
            
            Console.WriteLine(Path.GetFullPath(manager.FullPath));


            string test = "привет, как дела, спишь?";
            string res = test.Substring("привет, ".Length);
            Console.WriteLine(res);
        }
    }
}