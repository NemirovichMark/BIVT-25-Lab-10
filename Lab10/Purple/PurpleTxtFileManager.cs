using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace Lab10.Purple
{
    public class PurpleTxtFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
    {
        public PurpleTxtFileManager(string name) : base(name) { }

        public PurpleTxtFileManager(string name, string folder_path, string file_name, string file_extension = "txt")
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
            this.ChangeFileFormat("txt");
        }

        public override void Serialize(T obj)
        {
            if (obj == null || string.IsNullOrEmpty(this.FullPath)) return;

            if (!string.IsNullOrEmpty(this.FolderPath))
                Directory.CreateDirectory(this.FolderPath);

            string output_base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(obj.ToString() ?? ""));

            string codes_json = "";
            if (obj is Lab9.Purple.Task4 task4)
            {
                var codes_list = task4.Codes.Select(pair_code => new { pair = pair_code.Item1, code = pair_code.Item2.ToString() }).ToList();
                codes_json = JsonConvert.SerializeObject(codes_list);
            }

            File.WriteAllText(this.FullPath,
                $"Type: {obj.GetType().AssemblyQualifiedName}\n" +
                $"Input: {obj.Input ?? ""}\n" +
                $"Output: {output_base64}\n" +
                $"Codes: {codes_json}");
        }

        public override T Deserialize()
        {
            if (string.IsNullOrEmpty(this.FullPath) || !File.Exists(this.FullPath))
                return null!;

            string[] lines = File.ReadAllLines(this.FullPath);

            string type_name = "";
            string input = "";
            string output_code = "";
            string codes_json = "";

            foreach (string line in lines)
            {
                if (line.StartsWith("Type: "))
                    type_name = line.Substring("Type: ".Length);
                else if (line.StartsWith("Input: "))
                    input = line.Substring("Input: ".Length);
                else if (line.StartsWith("Output: "))
                    output_code = line.Substring("Output: ".Length);
                else if (line.StartsWith("Codes: "))
                    codes_json = line.Substring("Codes: ".Length);
            }

            if (string.IsNullOrEmpty(type_name))
                return null!;

            Type? type = Type.GetType(type_name);
            if (type == null || !typeof(T).IsAssignableFrom(type))
                return null!;

            object? obj = null;

            if (type == typeof(Lab9.Purple.Task4))
            {
                List<(string, char)> codes_list = new List<(string, char)>();
                if (!string.IsNullOrEmpty(codes_json))
                {
                    var deserialized = JsonConvert.DeserializeObject<List<dynamic>>(codes_json);
                    if (deserialized != null)
                    {
                        foreach (var item in deserialized)
                        {
                            string pair = item.pair;
                            string code_str = item.code;
                            char code = code_str.Length > 0 ? code_str[0] : '\0';
                            codes_list.Add((pair, code));
                        }
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
    }
}