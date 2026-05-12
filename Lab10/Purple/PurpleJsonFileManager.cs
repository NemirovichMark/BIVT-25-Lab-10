using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Purple
{
    public class PurpleJsonFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
    {
        public PurpleJsonFileManager(string name) : base(name)
        {
        }
        public PurpleJsonFileManager(string name, string folder, string fileName, string ext = "txt") : base(name, folder, fileName, ext)
        {
        }

        public override void EditFile(string text)
        {
            T obj = Deserialize();
            obj.ChangeText(text);
            Serialize(obj);
        }

        public override void ChangeFileExtension(string fileExtension = "json")
        {
            ChangeFileFormat(fileExtension);
        }

        public override void Serialize(T obj)
        {
           
            var jsonObj = JObject.FromObject(obj);
            jsonObj.Add("Type", obj.GetType().Name);

            string json = jsonObj.ToString();
            //if (!Directory.Exists(FullPath)) { Directory.CreateDirectory(FullPath); }
            File.WriteAllText(FullPath, json);
        }
        public override T Deserialize()
        {
            if (!File.Exists(FullPath)) return null;

            string content = File.ReadAllText(FullPath);
            if (string.IsNullOrWhiteSpace(content)) return null;

            JObject jObj = JObject.Parse(content);
            string typeJObject = jObj["Type"].ToString();
            Type typeObject = Type.GetType(($"Lab9.Purple.{typeJObject}, Lab9"));

            jObj.Remove("Type");
            var obj = jObj.ToObject(typeObject);
            
            return (T)obj;
        }

        
    }
}
