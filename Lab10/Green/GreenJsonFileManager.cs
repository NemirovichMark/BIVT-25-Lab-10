using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Reflection;

namespace Lab10.Green
{
    public class GreenJsonFileManager : GreenFileManager
    {
        public GreenJsonFileManager(string name) : base(name) { }

        public GreenJsonFileManager(string name, string folder, string fileName, string extension = "json")
            : base(name, folder, fileName, extension) { }

        public override void ChangeFileExtension(string newExtension)
        {
            ChangeFileFormat("json");
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

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(obj, obj.GetType(), options);

            if (json.StartsWith("{"))
                json = json.Insert(1, "\n  \"TypeName\": \"" + obj.GetType().FullName + "\",");

            File.WriteAllText(FullPath, json);
        }

        public override T Deserialize<T>()
        {
            if (!File.Exists(FullPath)) return null;

            string json = File.ReadAllText(FullPath);

            try
            {
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                string typeName = root.TryGetProperty("TypeName", out var typeEl) ? typeEl.GetString() : null;

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
                foreach (var prop in root.EnumerateObject())
                {
                    if (prop.Value.ValueKind == JsonValueKind.String)
                        values[prop.Name] = prop.Value.GetString();
                    else
                        values[prop.Name] = prop.Value.GetRawText();
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
                            try
                            {
                                if (parameters[i].ParameterType == typeof(string))
                                    args[i] = values[matchedKey];
                                else
                                    args[i] = JsonSerializer.Deserialize(values[matchedKey], parameters[i].ParameterType);
                            }
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
            catch
            {
                return null;
            }
        }
    }
}
