using Lab9.Purple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using Lab10.Purple;

namespace Lab10.Purple
{
    public class PurpleTxtFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
    {
        public PurpleTxtFileManager(string name) : base(name) { }
        public PurpleTxtFileManager(string name, string folderPath, string filename, string fileExtension = "") : base(name, folderPath, filename, fileExtension) { }
        public override void EditFile(string a)
        {
            T obj = Deserialize();
            if (obj != null)
            { obj.ChangeText(a); Serialize(obj); }
        }
        public override void ChangeFileExtension(string e)
        {
            ChangeFileExtension("txt");
        }
        public override void Serialize(T obj)
        {
            string txt = $"Input:{obj.Input}\nType:{obj.GetType().FullName}";
            File.WriteAllText(FullPath, txt);
        }
        public override T Deserialize()
        {
            if (string.IsNullOrEmpty(FullPath) || !File.Exists(FullPath)) return null;

            var pairs = new Dictionary<string, string>();
            using (StreamReader reader = new StreamReader(FullPath))
            {
                string l;
                while ((l = reader.ReadLine()) != null)
                {
                    int ind = l.IndexOf(':');
                    if (ind >= 0)
                        pairs[l.Substring(0, ind)] = l.Substring(ind + 1);
                }
            }
            if (!pairs.ContainsKey("Type") || !pairs.ContainsKey("Input")) return null;
            string typeName = pairs["Type"];
            string input = pairs["Input"];
            (string, char)[] codes = null;
            if (pairs.ContainsKey("Count"))
            {
                int count = int.Parse(pairs["Count"]);
                codes = new (string, char)[count];
                for (int i = 0; i < count; i++)
                {
                    string value = pairs[$"Code{i}"];
                    codes[i] = (value.Split("|")[0], value[value.Length - 1]);
                }
            }
            Lab9.Purple.Purple obj;
            if (typeName == "Task1") obj = new Task1(input);
            else if (typeName == "Task2") obj = new Task2(input);
            else if (typeName == "Task3") obj = new Task3(input);
            else if (typeName == "Task4") obj = new Task4(input, codes);
            else return null;
            obj.Review();
            return (T)obj;
        }
    }
}
