using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Lab10.Blue
{
    public class BlueJsonFileManager<T>: BlueFileManager<T> where T: Lab9.Blue.Blue
    {
        public BlueJsonFileManager(string name) : base(name)
        {

        }
        public BlueJsonFileManager(string name, string folderPath, string fileName, string fileExtension = "") : base(name, folderPath, fileName, fileExtension)
        {

        }
        public override void EditFile(string newContent)
        {
            var obj = Deserialize();
            if (obj == null) return;
            obj.ChangeText(newContent);
            Serialize(obj);
        }
        public override void ChangeFileExtension(string newExtention)
        {
            ChangeFileFormat("json");
        }
        public override void Serialize(T obj)
        {
            if (obj == null) return;

            JObject jobj = JObject.FromObject(obj);
            jobj["Type"] = obj.GetType().AssemblyQualifiedName;
            Directory.CreateDirectory(FolderPath);
            File.WriteAllText(FullPath, jobj.ToString());
        }

        public override T Deserialize()
        {
            if (!File.Exists(FullPath)) return null;

            string json = File.ReadAllText(FullPath);
            JObject jobj = JObject.Parse(json);

            string type = jobj["Type"].ToString();
            if (string.IsNullOrWhiteSpace(type)) return null;

            Type t = Type.GetType(type);
            if (t == null || !typeof(T).IsAssignableFrom(t)) return null; 

            jobj.Remove("Type");
            var obj = jobj.ToObject(t);

            if (obj is Lab9.Blue.Blue blue)
                blue.Review();

            return obj as T;
        }
    }
}
