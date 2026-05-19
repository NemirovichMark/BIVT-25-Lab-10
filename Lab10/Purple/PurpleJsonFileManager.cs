using System.Reflection;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Lab10.Purple
{
    public class PurpleJsonFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
    {
        public PurpleJsonFileManager(string name) : base(name) { }

        public PurpleJsonFileManager(string name, string folder_path, string file_name, string file_extension = "")
            : base(name, folder_path, file_name, file_extension) { }

        public override void EditFile(string text)
        {
            T obj = this.Deserialize();
            if (obj == null) return;
            obj.ChangeText(text);
            this.Serialize(obj);
        }

        public override void ChangeFileExtension(string new_extension)
        {
            this.ChangeFileFormat("json");
        }

        public override void Serialize(T obj)
        {
            if (obj == null || string.IsNullOrEmpty(this.FullPath)) return;

            JObject json = JObject.FromObject(obj);
            json["Type"] = obj.GetType().AssemblyQualifiedName;

            if (obj is Lab9.Purple.Task4 task4)
            {
                JArray codes_array = [];
                foreach ((string pair, char code) in task4.Codes)
                {
                    codes_array.Add(new JObject
                    {
                        ["pair"] = pair,
                        ["code"] = code.ToString()
                    });
                }
                json["Codes"] = codes_array;
            }

            if (!string.IsNullOrEmpty(this.FolderPath))
                Directory.CreateDirectory(this.FolderPath);

            File.WriteAllText(this.FullPath, json.ToString());
        }

        public override T Deserialize()
        {
            if (string.IsNullOrEmpty(this.FullPath) || !File.Exists(this.FullPath))
                return null!;

            try
            {
                string json_text = File.ReadAllText(this.FullPath);
                JObject json = JObject.Parse(json_text);

                string? type_name = json["Type"]?.ToString();
                if (string.IsNullOrWhiteSpace(type_name))
                    return null!;

                Type? type = Type.GetType(type_name);
                if (type == null || !typeof(T).IsAssignableFrom(type))
                    return null!;

                string input = json["Input"]?.ToString() ?? string.Empty;

                object? obj = null;

                if (type == typeof(Lab9.Purple.Task4))
                {
                    List<(string, char)> codes_list = [];
                    if (json["Codes"] is JArray codes_array)
                    {
                        foreach (JObject item in codes_array)
                        {
                            string pair = item["pair"]?.ToString() ?? "";
                            string code_str = item["code"]?.ToString() ?? "";
                            char code = code_str.Length > 0 ? code_str[0] : '\0';
                            codes_list.Add((pair, code));
                        }
                    }
                    obj = Activator.CreateInstance(type, input, codes_list.ToArray());
                }
                else
                {
                    obj = Activator.CreateInstance(type, input);
                }

                if (obj == null)
                    return null!;

                ((Lab9.Purple.Purple)obj).Review();

                return (T)obj;
            }
            catch
            {
                return null!;
            }
        }
    }
}