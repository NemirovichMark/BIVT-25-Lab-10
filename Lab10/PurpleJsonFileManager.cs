using Lab9.Purple;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;


namespace Lab10.Purple
{
    public class PurpleJsonFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
    {
        public PurpleJsonFileManager(string name) : base(name)
        { }

        public PurpleJsonFileManager(string name, string folderPath, string fileName, string fileExtension = "")
            : base(name, folderPath, fileName, fileExtension)
        { }
        public override void ChangeFileExtension(string ext)
        {
            base.ChangeFileFormat("json");
        }
        public override void EditFile(string newtext)
        {
            if (string.IsNullOrEmpty(FullPath) || !File.Exists(FullPath)) return;
            
            T obj = Deserialize();
            if (obj == null) return;
            obj.ChangeText(newtext);
            Serialize(obj);

        }
        public override T Deserialize()
        {
            if (FullPath == null || !File.Exists(FullPath)) return null;

            string content = File.ReadAllText(FullPath);
            var jo = JObject.Parse(content);
            string typeName = jo["Type"].ToString();

            string input = jo["Input"].ToString();
            if (typeName is null || input is null) return null;

            T obj = typeName switch
            {
                "Task1" => (T)(object)new Task1(input),
                "Task2" => (T)(object)new Task2(input),
                "Task3" => (T)(object)new Task3(input),
                "Task4" => (T)(object)new Task4(input,
                    jo["Codes"]?.ToObject<(string, char)[]>() ?? Array.Empty<(string, char)>())
            };
            obj.Review();
            return obj;
        }
        public override void Serialize(T obj)
        {
            if (obj == null) return;
            JObject jobj = JObject.FromObject(obj);

            jobj.Add("Type", obj.GetType().Name);
            File.WriteAllText(FullPath, jobj.ToString());
        }

    }
}


