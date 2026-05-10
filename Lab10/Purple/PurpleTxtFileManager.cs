using System.Reflection;
using System.Text;

namespace Lab10.Purple
{
    public class PurpleTxtFileManager<T> : PurpleFileManager<T> where T : global::Lab9.Purple.Purple
    {
        public PurpleTxtFileManager(string name) : base(name) { }

        public PurpleTxtFileManager(string name, string folder_path, string file_name, string file_extension = "txt")
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
            this.ChangeFileFormat("txt");
        }

        public override void Serialize(T obj)
        {
            if (obj == null || string.IsNullOrEmpty(this.FullPath))
                return;

            if (!string.IsNullOrEmpty(this.FolderPath))
                Directory.CreateDirectory(this.FolderPath);

            string output = Convert.ToBase64String(Encoding.UTF8.GetBytes(obj.ToString()));

            File.WriteAllText(this.FullPath,
                $"Type: {obj.GetType().AssemblyQualifiedName}\n" +
                $"Input: {obj.Input}\n" +
                $"Output: {output}");
        }

        public override T Deserialize()
        {
            if (string.IsNullOrEmpty(this.FullPath) || !File.Exists(this.FullPath))
                return null!;

            string[] lines = File.ReadAllLines(this.FullPath);

            string type_name = "", input = "", output_code = "";
            foreach (string line in lines)
            {
                if (line.StartsWith("Type: "))
                    type_name = line.Substring("Type: ".Length);
                else if (line.StartsWith("Input: "))
                    input = line.Substring("Input: ".Length);
                else if (line.StartsWith("Output: "))
                    output_code = line.Substring("Output: ".Length);
            }

            if (string.IsNullOrEmpty(type_name))
                return null!;

            Type? type = Type.GetType(type_name);
            if (type == null || !typeof(T).IsAssignableFrom(type))
                return null!;

            object? obj;
            if (type == typeof(global::Lab9.Purple.Task4))
                obj = Activator.CreateInstance(type, input, null);
            else
                obj = Activator.CreateInstance(type, input);

            if (obj == null)
                return null!;

            if (!string.IsNullOrEmpty(output_code))
            {
                string output = Encoding.UTF8.GetString(Convert.FromBase64String(output_code));
                FieldInfo? field = type.GetField("_output", BindingFlags.NonPublic | BindingFlags.Instance);
                if (field != null)
                {
                    if (field.FieldType == typeof(string))
                        field.SetValue(obj, output);
                    else if (field.FieldType == typeof(string[]))
                        field.SetValue(obj, output.Split('\n'));
                }
            }

            return (T)obj;
        }
    }
}