using Lab9.Purple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Purple
{
    public class PurpleTxtFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
    {
        public PurpleTxtFileManager(string name) : base(name)
        {
        }
        public PurpleTxtFileManager(string name, string folder, string fileName, string ext = "txt") : base(name, folder, fileName, ext)
        {
        }

        public override void EditFile(string text)
        {
            var obj = Deserialize();
            obj.ChangeText(text);
            Serialize(obj);
        }

        public override void ChangeFileExtension(string fileExtension)
        {
            ChangeFileExtension("txt");
        }

        public override void Serialize(T obj)
        {
            
            string type = obj.GetType().Name;
            string s = $"Input:{obj.Input}{Environment.NewLine}Type:{type}{Environment.NewLine}";
            if (obj is Lab9.Purple.Task4 task4)
            {
                var cortege = task4.Codes;
                s += "Codes: ";

                for (int i = 0; i < cortege.Length; i++)
                    s += cortege[i].Item1 + "*" + cortege[i].Item2 + ";";
            }
            File.WriteAllText(FullPath, s);
            
        }
        public override T Deserialize()
        {
            if (!File.Exists(FullPath)) return null;

            string text = File.ReadAllText(FullPath);
            if (string.IsNullOrWhiteSpace(text)) return null;

            Dictionary<string, string> dict = new Dictionary<string, string>();

            var lines = File.ReadAllLines(FullPath);
            foreach (var line in lines)
            {
                if (line.Contains(":"))
                {
                    var parts = line.Split(":", 2);
                    dict[parts[0].Trim()] = parts[1].Trim();
                }
            }

            string type = dict["Type"];
            string input = dict["Input"];

            switch (type)
            {
                case "Task1":
                    var obj1 = new Lab9.Purple.Task1(input);

                    return obj1 as T;
                case "Task2":
                    var obj2 = new Lab9.Purple.Task2(input);
                    return obj2 as T;
                case "Task3":
                    var obj3 = new Lab9.Purple.Task3(input);
                    return obj3 as T;
                case "Task4":
                    {
                        string codes = dict["Codes"];
                        var arr = codes.Split(";").Where(x => x != "").ToArray();
                        (string, char)[] array = new (string, char)[arr.Length];
                        for (int i = 0; i < arr.Length; i++)
                        {
                            var curArr = arr[i].Split("*");
                            array[i] = (curArr[0].Trim(), char.Parse(curArr[1].Trim()));
                        }

                        var obj4 = new Lab9.Purple.Task4(input, array);
                        return obj4 as T;
                    }

            }
            return null;

        }

        
    }
}
