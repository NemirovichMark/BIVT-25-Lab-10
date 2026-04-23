namespace Lab10
{
    public class Program
    {
        public static void Main()
        {
            string folderpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //рабочий стол
            string filename = "example.txt";
            string fullpath = Path.Combine(folderpath, filename);
            Console.WriteLine(fullpath);
            File.Create(fullpath).Close();
            File.WriteAllText(fullpath, "hi sus");
            File.AppendAllText(fullpath, "blabla");
            string content = File.ReadAllText(fullpath);
            string[] lines = File.ReadAllLines(fullpath);
            Console.WriteLine(content);
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
            string folderpath2 = Path.Combine(folderpath, "exampleFolder");
            string filepath = Path.Combine(folderpath2);
            if (!Directory.Exists(folderpath2)) Directory.CreateDirectory(folderpath2);
            string ext = "txt";
            if (!File.Exists(filepath)) File.Create(filepath).Close();
            else File.WriteAllText(filepath, ""); 
            //знаем только полный путь
            string folderpath3 = Path.GetDirectoryName(filepath);
            Console.WriteLine(folderpath3);
            string fileName2 = Path.GetFileNameWithoutExtension(filepath);
        }
    }
}
