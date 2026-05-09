using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Lab9.Purple;

namespace Lab10.Purple
{
    public class PurpleTxtFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
    {
        public PurpleTxtFileManager(string name) : base(name)
        {
        }

        public PurpleTxtFileManager(string name, string folderName, string fileName, string fileExtension = "txt") : base(name, folderName, fileName, fileExtension)
        {
        }

        public override T Deserialize()
        {
            if (FullPath == null || !File.Exists(FullPath)) return null;

            var pairs = new Dictionary<string, string>();
            foreach (string line in File.ReadAllLines(FullPath))
            {
                int colIndex = line.IndexOf(':');
                if (colIndex >= 0)
                    pairs[line.Substring(0, colIndex)] = line.Substring(colIndex + 1);
            }

            string typeName = pairs["Type"];
            string input = pairs["Input"];

            (string, char)[] codes = null;
            if (pairs.ContainsKey("CodesCount"))
            {
                int count = int.Parse(pairs["CodesCount"]);
                codes = new (string, char)[count];
                for (int i = 0; i < count; i++)
                {
                    string value = pairs["Code" + i];
                    codes[i] = (value.Substring(0, value.Length - 2), value[value.Length - 1]);
                }
            }

            Lab9.Purple.Purple result;
            if (typeName == "Task1")
            {
                result = new Task1(input);
            }
            else if (typeName == "Task2")
            {
                result = new Task2(input);
            }
            else if (typeName == "Task3")
            {
                result = new Task3(input);
            }
            else if (typeName == "Task4")
            {
                result = new Task4(input, codes);
            }
            else
            {
                result = null;
            }

            if (result == null) return null;
            result.Review();
            return (T)result;
        }


        public override void Serialize(T obj)
        {
            if (obj == null || FullPath == null) return;

            var pairs = new Dictionary<string, string>();
            pairs["Type"] = obj.GetType().Name;
            pairs["Input"] = obj.Input;

            if (obj is Task3 task3)
            {
                pairs["CodesCount"] = task3.Codes.Length.ToString();
                for (int i = 0; i < task3.Codes.Length; i++)
                    pairs["Code" + i] = task3.Codes[i].Item1 + " " + task3.Codes[i].Item2;
            }
            else if (obj is Task4 task4)
            {
                pairs["CodesCount"] = task4.Codes.Length.ToString();
                for (int i = 0; i < task4.Codes.Length; i++)
                    pairs["Code" + i] = task4.Codes[i].Item1 + " " + task4.Codes[i].Item2;
            }

            string[] lines = new string[pairs.Count];
            int index = 0;
            foreach (var pair in pairs)
                lines[index++] = pair.Key + ":" + pair.Value;

            File.WriteAllLines(FullPath, lines);
        }

        public override void EditFile(string input)
        {
            T content = Deserialize();
            content.ChangeText(input);
            Serialize(content);
        }

        public override void ChangeFileExtension(string extension)
        {
            if (extension == "txt") ChangeFileFormat("txt");
        }
    }
}
