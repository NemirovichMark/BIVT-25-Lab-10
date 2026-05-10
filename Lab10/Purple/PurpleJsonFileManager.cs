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

        public PurpleJsonFileManager(string name, string folder, string fileName, string extension = "txt") : base(name, folder, fileName, extension)
        {
        }

        public override void EditFile(string content)
        {
            T temp = Deserialize();
            temp.ChangeText(content);
            Serialize(temp);
        }
        public override void ChangeFileExtension(string fileExtention)
        {
            ChangeFileFormat("json");
            
        }

        public override void Serialize(T purple)
        { 
            JObject jsonObject = JObject.FromObject(purple);
            jsonObject["type"] = purple.GetType().AssemblyQualifiedName;
            CreateFile();
            using (TextWriter writer =  new StreamWriter(FullPath))
            {
                writer.Write(jsonObject.ToString());
            }
            
        }
        public override T Deserialize()
        {
            if (!File.Exists(FullPath)) return null;
            string jsonObjectString = File.ReadAllText(FullPath);
            JObject jsonObject = JObject.Parse(jsonObjectString);
            string jsonObjectType = jsonObject["type"].ToString();
            Type? objectType = Type.GetType(jsonObjectType);
            jsonObject.Remove("type");
            var purple = jsonObject.ToObject(objectType);
            return purple as T;
        }
    }
}
