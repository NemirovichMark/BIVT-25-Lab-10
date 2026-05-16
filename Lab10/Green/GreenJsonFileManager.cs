using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lab10.Green
{
    public class GreenJsonFileManager: GreenFileManager
    {
        public GreenJsonFileManager(string name) : base(name)
        {
        }

        public GreenJsonFileManager(string name, string folderPath, string fileName, string fileExtension = "json"): base(name, folderPath, fileName, fileExtension)
        {
        }

        public override void EditFile(string newText)
        {
            var obj = Deserialize<Lab9.Green.Green>();
            obj.ChangeText(newText);
            Serialize(obj);
        }

        public override void ChangeFileExtension(string newExtension)
        {
            ChangeFileFormat("json");
        }

        public override void Serialize<T>(T obj)
        {
            if (!string.IsNullOrEmpty(FolderPath) && !Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }

            ChangeFileFormat("json");

            string strokaValue = null;
            if (obj is Lab9.Green.Task3 task3)
            {
                strokaValue = task3.Stroka;
            }

            var temp = new
            {
                TypeName = obj.GetType().Name, 
                Input = obj.Input,
                Pattern = strokaValue
            };
            string json = JsonConvert.SerializeObject(temp, Formatting.Indented);
            File.WriteAllText(FullPath, json);
        }
        public override T Deserialize<T>()
        {
            if (string.IsNullOrEmpty(FullPath) || !File.Exists(FullPath))
            {
                return null;
            }
            string dis = File.ReadAllText(FullPath);

            dynamic data = JsonConvert.DeserializeObject(dis);

            string typeName = data.TypeName;
            string input = data.Input;

            Lab9.Green.Green obj;

            if (typeName == "Task1")
            {
                obj = new Lab9.Green.Task1(input);
            }
            else if (typeName == "Task2")
            {
                obj = new Lab9.Green.Task2(input);
            }
            else if (typeName == "Task3")
            {
                string pattern = data.Pattern;
                obj = new Lab9.Green.Task3(input, pattern);
            }
            else if (typeName == "Task4")
            {
                obj = new Lab9.Green.Task4(input);
            }
            else
            {
                return null;
            }

            obj.Review();
            return (T)(object)obj;
        }
    }
}
