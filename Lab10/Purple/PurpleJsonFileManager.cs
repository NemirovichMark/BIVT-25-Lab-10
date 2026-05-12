using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Lab10.Purple
{
    public class PurpleJsonFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
    {
        public PurpleJsonFileManager(string name) : base(name)
        {
        }

        public PurpleJsonFileManager(string name, string folderName, string fileName, string fileExtension = "") : base(name, folderName, fileName, fileExtension)
        {
        }

        public override T Deserialize()
        {
            string content = File.ReadAllText(FullPath);

            JObject json = JObject.Parse(content);

            string typeName = (string)json["Type"];

            Type type = Type.GetType(typeName + ", Lab9");
            json.Remove("Type");

            var result = (T)json.ToObject(type);
            result.Review();

            return result;
        }

        public override void Serialize(T obj)
        {
            JObject jobj = JObject.FromObject(obj);
            
            jobj["Type"] = obj.GetType().FullName;

            File.WriteAllText(FullPath, jobj.ToString());
        }

        public override void EditFile(string input)
        {
            T content = Deserialize();
            content.ChangeText(input);
            Serialize(content);
        }

        public override void ChangeFileExtension(string extension)
        {
            if (extension == "json") ChangeFileFormat("json");
        }
    }
}
