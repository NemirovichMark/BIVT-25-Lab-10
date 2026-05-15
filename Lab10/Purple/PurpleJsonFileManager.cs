using Lab9.Purple;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Purple
{
    public class PurpleJsonFileManager<T> : PurpleFileManager<T>
        where T : Lab9.Purple.Purple
    {
        public PurpleJsonFileManager(string name) : base(name) { }

        public PurpleJsonFileManager(string name, string folderName, string fileName, string fileExtension = "txt") : base(name, folderName, fileName, fileExtension) { }

        public override void EditFile(string content)
        {
            T obj = Deserialize();
            if(obj != null)
            {
                obj.ChangeText(content);
                Serialize(obj);
            }
        }
        public override void ChangeFileExtension(string extension)
        {
            ChangeFileFormat("json");
        }
        public override void Serialize(T obj)
        {
            if (obj != null && !string.IsNullOrWhiteSpace(FullPath))
            {
                JObject json = JObject.FromObject(obj);
                json.Add("Type", obj.GetType().Name);
                File.WriteAllText(FullPath, json.ToString());
            }   
        }
        public override T Deserialize()
        {
            if (File.Exists(FullPath))
            {
                string content = File.ReadAllText(FullPath);
                JObject obj = JObject.Parse(content);
                string jsonObjectType = obj["Type"].ToString();
                Type? type = Type.GetType(jsonObjectType);
                obj.Remove("Type");
                var purple = obj.ToObject(type);
                return purple as T;
            }
            else
                return null;
        }
    }
}
