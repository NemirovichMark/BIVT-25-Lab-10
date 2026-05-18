using Lab9.Blue;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Lab10.Blue
{
    public class BlueJsonFileManager<T> : BlueFileManager<T> where T : Lab9.Blue.Blue
    {
        public BlueJsonFileManager(string name) : base(name) { }

        public BlueJsonFileManager(string name, string folderPath, string fileName, string fileExtension = "json") :
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
            ChangeFileFormat("json");
        }

        public override void Serialize(T obj)
        {
            if (obj == null || string.IsNullOrEmpty(FullPath)) return;

            JObject jobj = new JObject();
            jobj["Type"] = obj.GetType().AssemblyQualifiedName;
            jobj["Input"] = obj.Input;

            if (obj is Task2 t2)
            {
                jobj["Sequence"] = t2.Sequence;
            }

            if (!string.IsNullOrEmpty(FolderPath)) Directory.CreateDirectory(FolderPath);
            File.WriteAllText(FullPath, jobj.ToString());
        }

        public override T Deserialize()
        {
            if (!File.Exists(FullPath)) return null;

            string content = File.ReadAllText(FullPath).Trim();

            if (string.IsNullOrEmpty(content) || !content.StartsWith("{"))
            {
                return null;
            }

            JObject jobj = JObject.Parse(content);

            if (jobj["Type"] == null || jobj["Input"] == null) return null;

            string typeName = jobj["Type"].ToString();
            string input = jobj["Input"].ToString();

            Lab9.Blue.Blue res = null;
            if (typeName.Contains("Task1")) res = new Task1(input);
            else if (typeName.Contains("Task2"))
            {
                string seq = jobj["Sequence"]?.ToString() ?? "";
                res = new Task2(input, seq);
            }
            else if (typeName.Contains("Task3")) res = new Task3(input);
            else if (typeName.Contains("Task4")) res = new Task4(input);

            if (res != null) res.Review();
            return res as T;
        }
    }
}