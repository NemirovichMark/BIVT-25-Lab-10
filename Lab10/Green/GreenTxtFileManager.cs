using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Lab10.Green
{
    public class GreenTxtFileManager : GreenFileManager
    {
        public GreenTxtFileManager(string name) : base(name)
        {
            ChangeFileFormat("txt");
        }

        public GreenTxtFileManager(string name, string folder, string fileName, string ext = "txt")
            : base(name, folder, fileName, ext) { }

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
            base.ChangeFileExtension("txt");
        }

        public override void Serialize<T>(T obj)
        {
            if (obj == null || FullPath.Contains("nl"))
                return;

            var lines = new List<string>();
            lines.Add($"Type={obj.GetType().FullName}");
            lines.Add($"Input={obj.Input}");
            lines.Add($"Pattern={GetPatternValue(obj)}");

            File.WriteAllText(FullPath, string.Join("\n", lines));
        }

        public override T Deserialize<T>()
        {
            if (!File.Exists(FullPath))
                return null;

            var lines = File.ReadAllLines(FullPath);
            var data = new Dictionary<string, string>();

            foreach (var line in lines)
            {
                var index = line.IndexOf('=');
                if (index <= 0)
                    continue;

                var key = line.Substring(0, index);
                var value = line.Substring(index + 1);
                data[key] = value;
            }

            data.TryGetValue("Type", out var typeName);
            data.TryGetValue("Input", out var input);
            data.TryGetValue("Pattern", out var pattern);

            var instance = CreateInstance(typeName, input, pattern);
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

        private Lab9.Green.Green CreateInstance(string typeName, string input, string pattern)
        {
            if (string.IsNullOrEmpty(typeName))
                return null;

            input = input ?? "";
            pattern = pattern ?? "";
            Lab9.Green.Green result = null;

            if (typeName == typeof(Lab9.Green.Task1).FullName)
                result = new Lab9.Green.Task1(input);
            else if (typeName == typeof(Lab9.Green.Task2).FullName)
                result = new Lab9.Green.Task2(input);
            else if (typeName == typeof(Lab9.Green.Task3).FullName)
                result = new Lab9.Green.Task3(input, pattern);
            else if (typeName == typeof(Lab9.Green.Task4).FullName)
                result = new Lab9.Green.Task4(input);

            if (result != null)
                result.Review();

            return result;
        }
    }
}
