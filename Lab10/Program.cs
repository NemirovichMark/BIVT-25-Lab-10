using System.Security.Cryptography.X509Certificates;
using System.Text;
using Lab9.Purple;

namespace Lab10
{
    public class Program
    {
        public static void Main()
        {
            // СОЗДАНИЕ ДИРЕКТОРИЙ

            string binPath = Environment.CurrentDirectory; // <- получение папки bin
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string firstDirectory = "FirstDirectory";
            string secondDirectory = "secondDirectory";

            string fullPath = Path.Combine(desktopPath,firstDirectory,secondDirectory);
            Directory.CreateDirectory(fullPath);

            // ЗАПИСЬ/(автоматическое)СОЗДАНИЕ ФАЙЛОВ

            // если указать только имя, сохранит туда где program.cs
            // !!! ЕСЛИ СОХРАНЯЕТЕ В ПАПКИ ТО ОНИ ДОЛЖНЫ СУЩЕСТВОВАТЬ
            // при таком вызове каждый раз перезаписывает (создается новый файл)
            using (var sw = new StreamWriter("test1.txt")) 
            {
                sw.Write("sw = new StreamWriter");
                sw.WriteLine("Hello, World!");
                sw.WriteLine("Hello, User!");
                sw.Write("My Name is Maksim!");
            }

            // при таком вызове файл НЕ перезаписывается, и дополняется 
            // новыми данными
            using (var sw = new StreamWriter("test2.txt", true))
            {
                sw.Write("sw = new StreamWriter");
                sw.WriteLine("Hello, World!");
                sw.WriteLine("Hello, User!");
                sw.Write("My Name is Maksim!");
            }

            //Заполняем мысивом строк
            string[] lines =    {"public class MaksimLegend {",
                                "public static void PrintHello() {", 
                                "string text = \"Hello, World!\";",
                                "System.Console.WriteLine(text);",
                                "}",
                                "}"};

            using (var sw = new StreamWriter("../Lab9/test3.cs"))
            {
                foreach (string line in lines)
                {
                    sw.WriteLine(line);
                }
            }

            // ЧТЕНИЕ ФАЙЛОВ

            // проверяем существует ли вообще файл 
            if (File.Exists("test1.txt")) 
                System.Console.WriteLine("Да существует");
            else System.Console.WriteLine("Нет не существует");

            // если укажем не верное название/путь -> ошибка
            using (var sr = new StreamReader("test1.txt"))
            {
                string text = sr.ReadToEnd();
                System.Console.WriteLine(text);
                // ReadToEnd() <- 10/10 считывает все с переносами на новую строку
                // очень удобно делать Split(Enviroment.NewLine)
                // !!! ЕСЛИ МЫ УЖЕ СЧИТАЛИ ФАЙЛ ТО ПРИ ПОВТОРНОМ СЧИТЫВАНИИ БУДЕТ ПУСТАЯ СТРОКА 
                // Также например:
                // a = ReadLine()
                // b = ReadToEnd();
                // в a <- будет лежать первая строка
                // в b <- все до конца начиная со второй
            }
            
            // построчное считывание файла
            using (var sr = new StreamReader("test2.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    System.Console.WriteLine(line);
                }
            }

            // ПЕРЕНОС ФАЙЛОВ В ДРУГУЮ ДИРЕКТОРИЮ
            if (!File.Exists(fullPath + "/test2.txt"))
            {
                // fullPath <- эту переменную создали на строке 18
                File.Move("test2.txt",fullPath+"/test2.txt");
                // File.Move(старое_название_файла, новое_название_файла);
                // <- если такой файл уже сущетсвует то ошибка
            }
            // УДАЛЕНИЕ ФАЙЛА 
            else 
            {
                File.Delete(fullPath+"/test2.txt");
                if (File.Exists(fullPath + "/test1.txt")) File.Delete(fullPath+"/test1.txt");
                File.Move("test2.txt",fullPath+"/test2.txt");
                File.Move("test1.txt",fullPath+"/test1.txt");
            }
            
            // ЕСТЬ ЛИ В ПАПКЕ ФАЙЛЫ/ДИРЕКТОРИИ
            bool hasFiles = Directory.EnumerateFiles(fullPath).Any();
            bool hasDirectory = Directory.EnumerateDirectories(fullPath).Any();
            bool hasFilesOrDirectories = Directory.EnumerateFileSystemEntries(fullPath).Any();
            System.Console.WriteLine(hasFilesOrDirectories);
        }
    }
}
