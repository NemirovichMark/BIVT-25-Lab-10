using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab10.Green
{
    public class GreenJsonFileManager : GreenFileManager
    {
        public GreenJsonFileManager(string name) : base(name)
        {
        }

        public GreenJsonFileManager(string name, string folderpath, string filename, string fileextension = "txt") : base(name, folderpath, filename, fileextension)
        {
        }

        public override void EditFile(string text)
        {
            if (text == "" || text == null || !File.Exists(FullPath)) return;

            Lab9.Green.Green obj = Deserialize<Lab9.Green.Green>();

            obj.ChangeText(text);
            Serialize(obj);
        }

        public override void ChangeFileExtension(string extension)
        {
            if (extension == null || extension == "" || extension != "json") return;
            ChangeFileFormat(extension);
        }

        public override T Deserialize<T>()
        {
            if (!File.Exists(FullPath)) return null;

            string jsonText = File.ReadAllText(FullPath);

            Dictionary<string, string> json = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonText);

            string nameType = json["Type"];
            string input = json["Input"];

            Lab9.Green.Green obj = null;

            switch (nameType)
            {
                case "Task1":
                    obj = new Lab9.Green.Task1(input);
                    break;
                case "Task2":
                    obj = new Lab9.Green.Task2(input);
                    break;
                case "Task3":
                    string pattern = json["Pattern"];
                    obj = new Lab9.Green.Task3(input, pattern);
                    break;
                case "Task4":
                    obj = new Lab9.Green.Task4(input);
                    break;
            }

            if (obj != null) obj.Review();

            return (T)obj;
        }

        public override void Serialize<T>(T obj)
        {
            if (obj == null) return;

            if (!Directory.Exists(FolderPath)) Directory.CreateDirectory(FolderPath);

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Type"] = obj.GetType().Name;
            dic["Input"] = obj.Input;
            if (obj is Lab9.Green.Task3 task3)
            {
                dic["Pattern"] = task3.Pattern;
            }

            string json = JsonSerializer.Serialize(dic);
            File.WriteAllText(FullPath, json);

        }
    }
}
