using System.Reflection;
using Newtonsoft.Json.Linq;

namespace Lab10.Purple
{
    public class PurpleJsonFileManager<T> : PurpleFileManager<T> where T : global::Lab9.Purple.Purple
    {
        public PurpleJsonFileManager(string name) : base(name) { }

        public PurpleJsonFileManager(string name, string folder_path, string file_name, string file_extension = "")
            : base(name, folder_path, file_name, file_extension) { }

        public override void EditFile(string text)
        {
            T obj = this.Deserialize();
            if (obj == null)
                return;
            obj.ChangeText(text);
            this.Serialize(obj);
        }

        public override void ChangeFileExtension(string new_extension)
        {
            this.ChangeFileFormat("json");
        }

        public override void Serialize(T obj)
        {
            if (obj == null || string.IsNullOrEmpty(this.FullPath))
                return;

            JObject json = JObject.FromObject(obj);
            json["Type"] = obj.GetType().AssemblyQualifiedName;

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

                object? obj;
                if (type == typeof(global::Lab9.Purple.Task4))
                    obj = Activator.CreateInstance(type, input, null);
                else
                    obj = Activator.CreateInstance(type, input);

                if (obj == null)
                    return null!;

                JToken? output_token = json["Output"];
                if (output_token != null)
                {
                    FieldInfo? output_field = type.GetField("_output", BindingFlags.NonPublic | BindingFlags.Instance);
                    if (output_field != null)
                    {
                        if (output_field.FieldType == typeof(string))
                            output_field.SetValue(obj, output_token.ToObject<string>());
                        else if (output_field.FieldType == typeof(string[]))
                            output_field.SetValue(obj, output_token.ToObject<string[]>());
                    }
                }

                return (T)obj;
            }
            catch
            {
                return null!;
            }
        }
    }
}