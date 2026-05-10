using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Blue
{
    public class BlueTxtFileManager<T>: BlueFileManager<T> where T: Lab9.Blue.Blue
    {
        public BlueTxtFileManager(string name) : base(name)
        {

        }
        public BlueTxtFileManager(string name, string folderPath, string fileName, string fileExtension = "txt") : base(name, folderPath, fileName, fileExtension)
        {

        }
        public override void EditFile(string newContent)
        {
            var obj = Deserialize();
            obj.ChangeText(newContent);
            Serialize(obj);
        }
        public override void ChangeFileExtension(string newExtention)
        {
            ChangeFileFormat("txt");
        }
        public override void Serialize(T obj)
        {
            if (obj == null) return;
            if (string.IsNullOrWhiteSpace(FullPath)) return;

            Directory.CreateDirectory(Path.GetDirectoryName(FullPath)!);

            using (var writer = new StreamWriter(FullPath))
            {
                if (obj is Lab9.Blue.Task2 task2)
                {
                    writer.WriteLine($"Combination: {task2.Combo}");
                }
                writer.WriteLine($"Type: {obj.GetType().AssemblyQualifiedName}");
                writer.WriteLine($"Input: {obj.Input}");
            }
        }
        public override T Deserialize()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            using (StreamReader reader = new StreamReader(FullPath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains(":"))
                    {
                        var parts = line.Split(new[] { ':' }, 2);
                        dict[parts[0].Trim()] = parts[1].Trim();
                    }
                }
            }

            string txt_input = null;
            string txt_comb = null;
            string type_name = null;
            if (dict.ContainsKey("Type"))
            {
                type_name = dict["Type"];
                txt_input = dict["Input"];
                if (dict.ContainsKey("Combination")) txt_comb = dict["Combination"];
            }

            if (txt_input == null) return null;
            if (type_name == null) return null;

            Lab9.Blue.Blue obj;
            if (type_name.Contains("Task1"))
            {
                obj = new Lab9.Blue.Task1(txt_input);
            }
            else if (type_name.Contains("Task2"))
            {
                obj = new Lab9.Blue.Task2(txt_input, txt_comb);
            }
            else if (type_name.Contains("Task3"))
            {
                obj = new Lab9.Blue.Task3(txt_input);
            }
            else if (type_name.Contains("Task4"))
            {
                obj = new Lab9.Blue.Task4(txt_input);
            }
            else return null;

            obj.Review();
            return (T)obj;
        }
    }
}
