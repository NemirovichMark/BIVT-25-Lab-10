using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Lab10.Green
{
    public class GreenTxtFileManager : GreenFileManager
    {
        public GreenTxtFileManager(string name) : base(name) { }

        public GreenTxtFileManager(string name, string folderPath, string fileName, string fileExtension = "txt") : base(name, folderPath, fileName, fileExtension)
        {

        }

        public override void EditFile(string content)
        {
            var obj = Deserialize<Lab9.Green.Green>();

            if (obj != null)
            {
                obj.ChangeText(content);
            }

            if (obj != null)
                Serialize(obj);
        }

        public override void ChangeFileExtension(string newExtension)
        {
            ChangeFileFormat("txt");
        }

        public override void Serialize<T>(T obj)
        {
            if (obj == null)
                return;

            CreateFile();

            var lines = new List<string>();

            Type type = obj.GetType();

            lines.Add("Type=" + type.AssemblyQualifiedName);

            foreach (var prop in type.GetProperties())
            {
                lines.Add(prop.Name + "=" + prop.GetValue(obj));
            }

            File.WriteAllLines(FullPath, lines);
        }


        public override T Deserialize<T>()
        {
            if (!File.Exists(FullPath))
                return null;

            var lines = File.ReadAllLines(FullPath);

            var data = new Dictionary<string, string>();

            foreach (var line in lines)
            {
                string[] parts = line.Split('=', 2);

                if (parts.Length == 2)
                {
                    data[parts[0]] = parts[1];
                }
            }

            Type type = Type.GetType(data["Type"]);

            var constructors = type.GetConstructors();

            var ctor = constructors[0];

            for (int i = 1; i < constructors.Length; i++)
            {
                if (constructors[i].GetParameters().Length > ctor.GetParameters().Length)
                {
                    ctor = constructors[i];
                }
            }

            var parameters = ctor.GetParameters();

            object[] args = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                var p = parameters[i];

                string name = p.Name.ToLower();

                string key = null;

                foreach (var k in data.Keys)
                {
                    string s = k.ToLower().Replace("_", "");

                    if (s == name ||
                        s.Contains(name) ||
                        name.Contains(s))
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

                string value = data[key];

                args[i] = Convert.ChangeType(value, p.ParameterType);
            }

            T obj = (T)ctor.Invoke(args);

            if (obj != null)
            {
                obj.Review();
            }

            return obj;
        }

    }
}