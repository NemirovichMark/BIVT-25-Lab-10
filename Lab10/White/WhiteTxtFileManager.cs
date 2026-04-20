using System.Diagnostics.Metrics;
using Lab9.Purple;
namespace Lab10.White;

public class WhiteTxtFileManager : IWhiteSerializer
{
    public string Name {get; private set;}
    public string Extension {get; private set;}
    public WhiteTxtFileManager (string name, string smth = "txt")
    {
        Name = name;
        Extension = smth;
    }
    public void Serialize(Purple obj)
    {
        var folder = Directory.GetCurrentDirectory(); // текущая папка запуска глубоко в проекте (Lab10/bin/Debug/net8.0)
        folder = Directory.GetParent(folder).Parent.Parent.FullName; // поднимаемся на две папки вверх (Lab10)
        folder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // получить путь к папке Deckstop

        var filePath = Path.Combine(folder, Name);
        filePath += "." + Extension;
        if (!File.Exists(filePath)) // создаем
        {
            File.Create(filePath).Close();
        }
        if (File.Exists(filePath)) // удаляем
        {
            File.Delete(filePath);
        }

        File.WriteAllText(filePath, obj.Input); // записать все как есть
        File.AppendAllText(filePath, obj.Input); // записать все как есть в конец
        // File.WriteAllLines  <- работает c []string
        Dictionary<string, string> dict = new Dictionary<string, string>();
        dict.Add("Type", obj.GetType().Name);
        dict.Add("Input", obj.Input);
        var d  = dict.ToArray();
        string[] lines = new string[dict.Count];
        for (int i=0; i<lines.Length; i++)
        {
            lines[i] = d[i].Key + " : " +d[i].Value;
        }
        File.WriteAllLines(filePath, lines);
        var str = File.ReadAllLines(filePath); // читает все одной строкой
        lines = File.ReadAllLines(filePath);

        var pair = lines[0].Split(":", 2, StringSplitOptions.RemoveEmptyEntries);
        var input = lines[1].Split(":", 2, StringSplitOptions.RemoveEmptyEntries);

        Lab9.Purple.Purple desObj;
        if (pair[0] == "Type")
        {
            switch (pair[1])
            {
                case "Task1" : desObj = new Lab9.Purple.Task1("maksimka"); break;
                case "Task2" : desObj = new Lab9.Purple.Task1("maksimka"); break;
                case "Task3" : break;
                
            }
        }
    }
}

