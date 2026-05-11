using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Blue
{
    public class BlueTxtFileManager<T> : BlueFileManager<T> where T : Lab9.Blue.Blue
    {
        public BlueTxtFileManager(string name) : base(name) { _fileExtension = "txt"; }
        public BlueTxtFileManager(string name, string fileName, string folderPath, string fileExtension = "") : base(name, fileName, folderPath, fileExtension) { _fileExtension = "txt"; }

        public override void EditFile(string text)
        {
            var obj = Deserialize();
            obj.ChangeText(text);
            Serialize(obj);
        }

        public override void ChangeFileExtension(string text)
        {
            ChangeFileFormat("txt");
        }

        public override void Serialize(T elem)
        {
            if (elem == null) return;
            Directory.CreateDirectory(FolderPath);

            using (var SS = new StreamWriter(FullPath))
            {
                SS.WriteLine($"Type:{elem.GetType().FullName.Split('.')[2]}");
                SS.WriteLine($"Input:{elem.Input}");
                if (elem is Lab9.Blue.Task2 t2) SS.WriteLine($"Substr:{t2.Substr}");
            }
        }
        public override T Deserialize()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            using (var SS = new StreamReader(FullPath))
            {
                string line;
                while((line = SS.ReadLine()) != null)
                {
                    if (line.Contains(":"))
                    {
                        var parts = line.Split(':', 2);
                        dict[parts[0].Trim()] = parts[1].Trim();
                    }
                }
            }
            string type = "";
            string input = "";
            if (dict.ContainsKey("Type"))
                type = dict["Type"];
            else throw new Exception("No class type founded");
            if (dict.ContainsKey("Input"))
                input = dict["Input"];
            else throw new Exception("No input founded");
            T ans = null;
            if (type == "Task1")
                ans = new Lab9.Blue.Task1(input) as T;
            if (type == "Task2") {
                if (dict.ContainsKey("Substr"))
                    ans = new Lab9.Blue.Task2(input, dict["Substr"]) as T;
                else throw new Exception("No substring founded");
            }
            if (type == "Task3")
                ans = new Lab9.Blue.Task3(input) as T;
            if (type == "Task4")
                ans = new Lab9.Blue.Task4(input) as T;
            if (ans == null) throw new Exception("Wrong type string");
            ans.Review();
            return ans;
        }
    }
}
