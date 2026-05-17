using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Purple
{
    public class PurpleTxtFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
    {
        public PurpleTxtFileManager(string name) : base(name) { }
        public PurpleTxtFileManager(string name, string folderPath, string fileName, string fileExtension = "txt")
            : base(name, folderPath, fileName, fileExtension) { }

        public override void EditFile(string text)
        {
            T obj = Deserialize();
            if (obj == null) return;
            obj.ChangeText(text);
            Serialize(obj);
        }
        public override void ChangeFileExtension(string extension)
        {
            base.ChangeFileExtension("txt");
        }
        public override void Serialize(T obj)
        {
            if (obj == null) return;

            var dict = new Dictionary<string, string>();
            dict["Type"] = obj.GetType().Name;
            dict["Input"] = obj.Input;

            if (obj is Lab9.Purple.Task1 task1)
            {
                dict["Output"] = task1.Output;
            }
            else if (obj is Lab9.Purple.Task2 task2)
            {
                dict["Output"] = string.Join(" ", task2.Output);
            }
            else if (obj is Lab9.Purple.Task3 task3)
            {
                dict["Output"] = task3.Output;
            }
            else if (obj is Lab9.Purple.Task4 task4)
            {
                dict["Output"] = task4.Output;

                if (task4.Codes != null)
                {
                    dict["Codes"] = string.Join(";", task4.Codes.Select(x => $"{x.Item1},{x.Item2}"));
                }
            }

            var lines = dict.Select(x => $"{x.Key}:{x.Value}");
            File.WriteAllLines(FullPath, lines);
        }
        public override T Deserialize()
        {
            if (!File.Exists(FullPath)) return null;

            var lines = File.ReadAllLines(FullPath);
            if (lines.Length == 0) return null;

            var dict = new Dictionary<string, string>();

            foreach (var line in lines)
            {
                var parts = line.Split(':', 2);
                if (parts.Length == 2)
                {
                    dict[parts[0]] = parts[1];
                }
            }

            if (!dict.ContainsKey("Type") || !dict.ContainsKey("Input")) return null;

            string type = dict["Type"]; string input = dict["Input"];
            T obj = null;

            if (type == "Task1")
            {
                obj = new Lab9.Purple.Task1(input) as T;
            }
            else if (type == "Task2")
            {
                obj = new Lab9.Purple.Task2(input) as T;
            }
            else if (type == "Task3")
            {
                obj = new Lab9.Purple.Task3(input) as T;
            }
            else if (type == "Task4")
            {
                (string, char)[] codes = null;

                if (dict.ContainsKey("Codes"))
                {
                    codes = dict["Codes"]
                        .Split(';', StringSplitOptions.RemoveEmptyEntries)
                        .Select(x =>
                        {
                            var parts = x.Split(',');
                            if (parts.Length != 2) return (null, '\0');

                            return (parts[0], parts[1][0]);
                        })
                        .ToArray();
                }
                obj = new Lab9.Purple.Task4(input, codes) as T;
            }

            if (obj == null) return null;
            obj.Review();

            return obj;
        }
    }
}
