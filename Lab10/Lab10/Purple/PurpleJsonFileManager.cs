using Lab9.Purple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using Lab10.Purple;

namespace Lab10.Purple
{
    public  class PurpleJsonFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
    {
        public PurpleJsonFileManager(string name) : base(name) { }
        public PurpleJsonFileManager(string name, string folderPath, string filename, string fileExtension = ".json") : base(name, folderPath, filename, fileExtension) { }
        public override void EditFile(string a)
        {
            T obj=Deserialize();
            if (obj != null)
            { obj.ChangeText(a); Serialize(obj); }
        }
        public override void ChangeFileExtension(string e)
        {
            ChangeFileExtension("json");
        }
        public override void Serialize(T obj)
        {
            if (obj == null) return;
            JObject jobj = JObject.FromObject(obj);

            jobj.Add("Type", obj.GetType().Name);
            File.WriteAllText(FullPath, jobj.ToString());
        }
        public override T Deserialize()
        {
            if (FullPath == null || !File.Exists(FullPath)) return null;
            string a = File.ReadAllText(FullPath);
            var json = JObject.Parse(a);
            string typeName = json["Type"].ToString();

            string input = json["Input"].ToString();
            if (typeName is null || input is null) return null;

            T obj = typeName switch
            {
                "Task1" => (T)(object)new Task1(input),
                "Task2" => (T)(object)new Task2(input),
                "Task3" => (T)(object)new Task3(input),
                "Task4" => (T)(object)new Task4(input,
                    json["Codes"]?.ToObject<(string, char)[]>() ?? Array.Empty<(string, char)>())
            };
            obj.Review();
            return obj;
        }
    }
}
