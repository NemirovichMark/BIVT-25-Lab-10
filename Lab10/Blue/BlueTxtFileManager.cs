using Lab9.Blue;
using System.Collections.Generic;
using System.IO;

namespace Lab10.Blue
{
    public class BlueTxtFileManager<T> : BlueFileManager<T> where T : Lab9.Blue.Blue
    {
        public BlueTxtFileManager(string name) : base(name) { }

        public BlueTxtFileManager(string name, string folderPath, string fileName, string fileExtension = "txt") :
            base(name, folderPath, fileName, fileExtension)
        { }

        public override void EditFile(string text)
        {
            var obj = Deserialize();
            if (obj == null) return;
            obj.ChangeText(text);
            Serialize(obj);
        }

        public override void ChangeFileExtension(string extension)
        {
            ChangeFileFormat("txt");
        }

        public override void Serialize(T obj)
        {
            if (obj == null || string.IsNullOrEmpty(FullPath)) return;

            if (!string.IsNullOrEmpty(FolderPath)) Directory.CreateDirectory(FolderPath);

            using (StreamWriter sw = new StreamWriter(FullPath))
            {
                sw.WriteLine($"Type: {obj.GetType().AssemblyQualifiedName}");
                sw.WriteLine($"Input: {obj.Input}");
                if (obj is Task2 t2)
                {
                    sw.WriteLine($"Sequence: {t2.Sequence}");
                }
            }
        }

        public override T Deserialize()
        {
            if (!File.Exists(FullPath)) return null;

            string[] lines = File.ReadAllLines(FullPath);

            if (lines.Length < 2) return null;

            var dict = new Dictionary<string, string>();
            foreach (var line in lines)
            {
                int colonIndex = line.IndexOf(':');
                if (colonIndex != -1)
                {
                    string key = line.Substring(0, colonIndex).Trim();
                    string value = line.Substring(colonIndex + 1).Trim();
                    dict[key] = value;
                }
            }

            if (!dict.ContainsKey("Type") || !dict.ContainsKey("Input")) return null;

            string typeName = dict["Type"];
            string input = dict["Input"];

            Lab9.Blue.Blue res = null;

            if (typeName.Contains("Task1"))
            {
                res = new Lab9.Blue.Task1(input);
            }
            else if (typeName.Contains("Task2"))
            {
                string seq = dict.ContainsKey("Sequence") ? dict["Sequence"] : "";
                res = new Lab9.Blue.Task2(input, seq);
            }
            else if (typeName.Contains("Task3"))
            {
                res = new Lab9.Blue.Task3(input);
            }
            else if (typeName.Contains("Task4"))
            {
                res = new Lab9.Blue.Task4(input);
            }

            if (res != null)
            {
                res.Review(); 
                return res as T; 
            }

            return null;
        }
    }
}