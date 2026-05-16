using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace Lab10.Green
{
    public class GreenJsonFileManager : GreenFileManager
    {
        private class JsonData
        {
            public string Type { get; set; }
            public string Input { get; set; }
            public string Pattern { get; set; }
        }

        public GreenJsonFileManager(string name) : base(name)
        {
            ChangeFileFormat("json");
        }

        public GreenJsonFileManager(string name, string folder, string fileName, string ext = "txt")
            : base(name, folder, fileName, "json") { }

        public override void EditFile(string text)
        {
            var obj = Deserialize<Lab9.Green.Green>();
            if (obj == null)
                return;

            obj.ChangeText(text);
            Serialize(obj);
        }

        public override void ChangeFileExtension(string newExtension)
        {
            base.ChangeFileExtension("json");
        }

        public override void Serialize<T>(T obj)
        {
            if (obj == null || FullPath.Contains("nl"))
                return;

            var data = new JsonData
            {
                Type = obj.GetType().FullName,
                Input = obj.Input,
                Pattern = GetPatternValue(obj)
            };

            var json = JsonConvert.SerializeObject(data);
            File.WriteAllText(FullPath, json);
        }

        public override T Deserialize<T>()
        {
            if (!File.Exists(FullPath))
                return null;

            var json = File.ReadAllText(FullPath);
            if (string.IsNullOrWhiteSpace(json))
                return null;

            var data = JsonConvert.DeserializeObject<JsonData>(json);
            var instance = CreateInstance(data);

            if (instance is T result)
                return result;

            return null;
        }

        private string GetPatternValue(Lab9.Green.Green obj)
        {
            var field = obj.GetType().GetField("_pattern", BindingFlags.Instance | BindingFlags.NonPublic);
            var value = field == null ? null : field.GetValue(obj) as string;
            return value ?? "";
        }

        private Lab9.Green.Green CreateInstance(JsonData data)
        {
            if (data == null || string.IsNullOrEmpty(data.Type))
                return null;

            string input = data.Input ?? "";
            string pattern = data.Pattern ?? "";
            Lab9.Green.Green result = null;

            if (data.Type == typeof(Lab9.Green.Task1).FullName)
                result = new Lab9.Green.Task1(input);
            else if (data.Type == typeof(Lab9.Green.Task2).FullName)
                result = new Lab9.Green.Task2(input);
            else if (data.Type == typeof(Lab9.Green.Task3).FullName)
                result = new Lab9.Green.Task3(input, pattern);
            else if (data.Type == typeof(Lab9.Green.Task4).FullName)
                result = new Lab9.Green.Task4(input);

            if (result != null)
                result.Review();

            return result;
        }
    }
}
