using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Lab10.Green
{
    public class GreenTxtFileManager : GreenFileManager
    {
        public GreenTxtFileManager(string name) : base(name) { }

        public GreenTxtFileManager(string name, string folder, string fileName, string extension = "txt")
            : base(name, folder, fileName, extension) { }

        public override void ChangeFileExtension(string newExtension)
        {
            ChangeFileFormat("txt");
        }

        public override void EditFile(string input)
        {
            if (!File.Exists(FullPath)) return;
            var task = Deserialize<Lab9.Green.Green>();
            if (task == null) return;
            task.ChangeText(input);
            Serialize(task);
        }

        public override void Serialize<T>(T obj)
        {
            if (obj == null) return;

            string text = "TypeName:" + obj.GetType().FullName + "\n";

            var props = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                var val = prop.GetValue(obj);
                if (val == null) continue;

                if (val is string[] arr)
                    text += prop.Name + ":[" + string.Join("|||", arr) + "]\n";
                else
                    text += prop.Name + ":" + val.ToString().Replace("\n", "\\n").Replace("\r", "\\r") + "\n";
            }

            File.WriteAllText(FullPath, text);
        }

        public override T Deserialize<T>()
        {
            if (!File.Exists(FullPath)) return null;

            string[] lines = File.ReadAllLines(FullPath);

            string typeName = null;
            foreach (var line in lines)
            {
                if (line.StartsWith("TypeName:"))
                {
                    typeName = line.Substring(9).Trim();
                    break;
                }
            }

            Type actualType = typeof(T);
            if (!string.IsNullOrEmpty(typeName))
            {
                foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
                {
                    var found = asm.GetType(typeName);
                    if (found != null) { actualType = found; break; }
                }
            }

            var values = new System.Collections.Generic.Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var line in lines)
            {
                int colon = line.IndexOf(':');
                if (colon <= 0) continue;
                string key = line.Substring(0, colon).Trim();
                string value = line.Substring(colon + 1).Trim()
                    .Replace("\\n", "\n").Replace("\\r", "\r");
                values[key] = value;
            }

            object result = null;
            var constructors = actualType.GetConstructors().OrderByDescending(c => c.GetParameters().Length);

            foreach (var ctor in constructors)
            {
                var parameters = ctor.GetParameters();
                var args = new object[parameters.Length];
                bool success = true;

                for (int i = 0; i < parameters.Length; i++)
                {
                    string paramName = parameters[i].Name.ToLower();

                    string matchedKey = null;
                    foreach (var key in values.Keys)
                    {
                        string k = key.ToLower();
                        if (k == paramName || k.Contains(paramName) || paramName.Contains(k))
                        {
                            matchedKey = key;
                            break;
                        }
                    }

                    if (matchedKey == null && (paramName == "text" || paramName == "str"))
                    {
                        foreach (var key in values.Keys)
                            if (key.ToLower().Contains("input")) { matchedKey = key; break; }
                    }

                    if (matchedKey != null)
                    {
                        try { args[i] = Convert.ChangeType(values[matchedKey], parameters[i].ParameterType); }
                        catch { success = false; break; }
                    }
                    else
                    {
                        args[i] = null;
                    }
                }

                if (success)
                {
                    try { result = ctor.Invoke(args); break; }
                    catch { }
                }
            }

            if (result == null)
                result = Activator.CreateInstance(actualType);

            return (T)result;
        }
    }
}
