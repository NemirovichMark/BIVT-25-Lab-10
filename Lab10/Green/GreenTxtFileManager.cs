using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Green
{
    public class GreenTxtFileManager : GreenFileManager
    {
        public GreenTxtFileManager(string name) : base(name)
        {
        }

        public GreenTxtFileManager(string name, string folderPath, string fileName, string fileExtension = "json") : base(name, folderPath, fileName, fileExtension)
        {
        }
        public override void EditFile(string newText)
        {
            var obj = Deserialize<Lab9.Green.Green>();
            obj.ChangeText(newText);
            Serialize(obj);
        }

        public override void ChangeFileExtension(string newFileExtension)
        {
            ChangeFileFormat("txt");
        }

        public override void Serialize<T>(T obj)
        {
            ChangeFileFormat("txt");

            var path = FullPath;
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            if (!string.IsNullOrEmpty(FolderPath) && !Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }

            var sb = new StringBuilder();
            sb.AppendLine("Type=" + obj.GetType().Name);
            sb.AppendLine("Input=" + obj.Input);

            if (obj is Lab9.Green.Task3 obj2)
            {
                var stroka = obj2.Stroka;
                if (stroka != null)
                {
                    sb.AppendLine("Stroka=" + stroka);
                }
            }

            File.WriteAllText(path, sb.ToString());
        }

        public override T Deserialize<T>()
        {
            var path = FullPath;
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                return null!;
            }

            var text = File.ReadAllText(path);
            if (string.IsNullOrEmpty(text))
            {
                return null!;
            }

            var dict = new Dictionary<string, string>();
            string text1 = File.ReadAllText(FullPath);

            string[] str = text1.Split('\n');
            for (int i = 0; i < str.Length; i++)
            {
                string[] slova = str[i].Split('=');
                if (slova.Length == 2)
                {
                    dict[slova[0].Trim()] = slova[1].Trim();
                }
            }

            string type = dict["Type"];
            string input = dict["Input"];

            Lab9.Green.Green result;

            if (type == "Task1")
            {
                result = new Lab9.Green.Task1(input);
            }
            else if (type == "Task2")
            {
                result = new Lab9.Green.Task2(input);
            }
            else if (type == "Task3")
            {
                string stroka = dict.ContainsKey("Stroka") ? dict["Stroka"] : "";
                result = new Lab9.Green.Task3(input, stroka);
            }
            else if (type == "Task4")
            {
                result = new Lab9.Green.Task4(input);
            }
            else
            {
                return null!;
            }

            result.Review();

            return (T)(object)result;
        }
    }
}
