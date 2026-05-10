using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Green
{
    public class GreenTxtFileManager : GreenFileManager
    {
        public GreenTxtFileManager(string name) : base(name)
        {
        }

        public GreenTxtFileManager(string name, string folderPath, string fileName, string fileExtension = "json") : base(name, folderPath, fileName, fileExtension)
        {
        }
        public override void EditFile(string newText)
        {
            var obj = Deserialize<Lab9.Green.Green>();
            obj.ChangeText(newText);
            Serialize(obj);
        }

        public override void ChangeFileExtension(string newFileExtension)
        {
            ChangeFileFormat("txt");
        }

        public override void Serialize<T>(T obj)
        {
            ChangeFileFormat("txt");
            string content = "Type;;;" + obj.GetType().AssemblyQualifiedName + Environment.NewLine;
            Type currentType = obj.GetType();
            while (currentType != null && currentType != typeof(object))
            {
                var fields = currentType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                for (int i = 0; i < fields.Length; i++)
                {
                    var field = fields[i];
                    var value = field.GetValue(obj);
                    if (value is string valStr)
                    {
                        string cleanValue = valStr.Replace("\n", "[-n-]");
                        content += field.Name + ";;;" + cleanValue + Environment.NewLine;
                    }
                }
                currentType = currentType.BaseType;
            }
            File.WriteAllText(FullPath, content);
        }

        public override T Deserialize<T>()
        {
            string[] lines = File.ReadAllLines(FullPath);
            string typeStr = lines[0].Split(";;;")[1];
            Type type1 = Type.GetType(typeStr);

            var data = new Dictionary<string, string>();
            for (int i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split(";;;");
                data[parts[0]] = parts[1].Replace("[-n-]", "\n");
            }

            var constructor = type1.GetConstructors()[0];
            var parameters = constructor.GetParameters();
            object[] args = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                string pName = parameters[i].Name;

                if (data.TryGetValue("_" + pName, out string val))
                {
                    args[i] = val;
                }

                else if (data.TryGetValue("_input", out string inputVal))
                {
                    args[i] = inputVal;
                }
            }

            T res = (T)constructor.Invoke(args);

            if (data.TryGetValue("_input", out string finalInput))
            {
                res.ChangeText(finalInput);
            }

            return res;
        }
    }
}
