using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Collections.Generic;

namespace Lab10.Green
{
    public class GreenJsonFileManager : GreenFileManager
    {
        public GreenJsonFileManager(string name) : base(name) { }

        public GreenJsonFileManager(string name, string folderPath, string fileName, string fileExtension = "json") : base(name, folderPath, fileName, fileExtension) { }


        public override void EditFile(string text)
        {
            var obj = Deserialize<Lab9.Green.Green>();

            if (obj != null)
            {
                obj.ChangeText(text);
                Serialize(obj);
            }

        }

        public override void ChangeFileExtension(string extension)
        {
            ChangeFileFormat("json");
        }

        public override void Serialize<T>(T obj)
        {
            if (obj == null) return;

            CreateFile();

            var type = obj.GetType();

            var data = new Dictionary<string, object>();
            data["Type"] = type.AssemblyQualifiedName;

            foreach (var prop in type.GetProperties())
            {
                data[prop.Name] = prop.GetValue(obj);
            }

            File.WriteAllText(FullPath, JsonSerializer.Serialize(data));
        }

        public override T Deserialize<T>()
        {
            if (!File.Exists(FullPath)) return null;

            var data = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(File.ReadAllText(FullPath));

            Type type = Type.GetType(data["Type"].GetString());
            
            var constructors = type.GetConstructors();

            var constructor = constructors[0];

            for (int i = 1; i < constructors.Length; i++)
            {
                if (constructors[i].GetParameters().Length > constructor.GetParameters().Length)
                {
                    constructor = constructors[i];
                }
            }

            var parameters = constructor.GetParameters();

            object[] args = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                var p = parameters[i];

                string name = p.Name.ToLower();

                string key = null;

                foreach (var k in data.Keys)
                {
                    string s = k.ToLower().Replace("_", "");

                    if (s == name || s.Contains(name) || name.Contains(s))
                    {
                        key = k;
                        break;
                    }
                }

                if (key == null && (name == "text" || name == "str"))
                {
                    foreach (var k in data.Keys)
                    {
                        if (k.ToLower().Contains("input"))
                        {
                            key = k;
                            break;
                        }
                    }
                }

                if (key == null)
                {
                    args[i] = null;
                    continue;
                }

                string value = data[key].GetString();

                args[i] = Convert.ChangeType(value, p.ParameterType);
            }

            T obj = (T)constructor.Invoke(args);

            if (obj != null)
            {
                obj.Review();
            }

            return obj;
        }
    }
}