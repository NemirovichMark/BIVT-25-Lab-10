using System;
using System.Collections.Generic;
using System.IO;
using Lab9.Purple;

namespace Lab10.Purple
{
    public class PurpleTxtFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
    {
        public PurpleTxtFileManager(string name) : base(name)
        {
            ChangeFileFormat("txt");
        }

        public PurpleTxtFileManager(string name, string folderName, string fileName, string fileExtension = "txt")
            : base(name, folderName, fileName, fileExtension)
        {
            ChangeFileFormat("txt");
        }

        public override void ChangeFileExtension(string text)
        {
            ChangeFileFormat("txt");
        }

        public override void EditFile(string text)
        {
            T task = Deserialize();
            if (task == null) return;

            task.ChangeText(text);
            Serialize(task);
        }

        public override void Serialize(T obj)
        {
            if (obj == null) return;

            List<string> lines = new List<string>();
            // Используем двоеточие, как того ожидает тест
            lines.Add($"Type: {obj.GetType().Name}");
            lines.Add($"Input: {obj.Input ?? ""}");

            if (obj is Task4 task4)
            {
                if (task4.Codes != null)
                {
                    foreach (var item in task4.Codes)
                    {
                        lines.Add($"Code: {item.Item1}|{item.Item2}");
                    }
                }
            }
            else if (obj is Task3 task3)
            {
                if (task3.Codes != null)
                {
                    foreach (var item in task3.Codes)
                    {
                        lines.Add($"Code: {item.Item1}|{item.Item2}");
                    }
                }
            }

            File.WriteAllLines(FullPath, lines);
        }

        public override T Deserialize()
        {
            if (!File.Exists(FullPath)) return null;

            string type = "";
            string input = "";
            List<(string, char)> codesList = new List<(string, char)>();

            try
            {
                string[] lines = File.ReadAllLines(FullPath);

                foreach (string line in lines)
                {
                    // Теперь ищем двоеточие как разделитель
                    int separatorIndex = line.IndexOf(':');
                    if (separatorIndex == -1) continue;

                    string key = line.Substring(0, separatorIndex).Trim();
                    string value = line.Substring(separatorIndex + 1).Trim();

                    if (key == "Type") type = value;
                    else if (key == "Input") input = value;
                    else if (key == "Code")
                    {
                        string[] parts = value.Split('|');
                        if (parts.Length == 2 && !string.IsNullOrEmpty(parts[1]))
                        {
                            codesList.Add((parts[0], parts[1][0]));
                        }
                    }
                }

                if (string.IsNullOrEmpty(type)) return null;

                Lab9.Purple.Purple task;
                var codesArray = codesList.ToArray();

                switch (type)
                {
                    case "Task2": task = new Task2(input); break;
                    case "Task3": task = new Task3(input); break;
                    case "Task4": task = new Task4(input, codesArray); break;
                    default: task = new Task1(input); break;
                }

                task.Review();
                return (T)task;
            }
            catch
            {
                return null;
            }
        }
    }
}