using Lab9.Purple;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Purple
{
    public class PurpleJsonFileManager<T> : PurpleFileManager<T>
        where T : Lab9.Purple.Purple
    {
        public PurpleJsonFileManager(string name) : base(name) { }

        public PurpleJsonFileManager(string name, string folderName, string fileName, string fileExtension = "txt") : base(name, folderName, fileName, fileExtension) { }

        public override void EditFile(string content)
        {
            T obj = Deserialize();
            if(obj != null)
            {
                obj.ChangeText(content);
                Serialize(obj);
            }
        }
        public override void ChangeFileExtension(string extension)
        {
            base.ChangeFileExtension("json");
        }
        public override void Serialize(T obj)
        {
            if (obj != null && !string.IsNullOrWhiteSpace(FullPath))
            {
                JObject json = JObject.FromObject(obj);
                json["Type"] = obj.GetType().Name;
                File.WriteAllText(FullPath, json.ToString());
            }
        }
        public override T Deserialize()
        {
            if (!string.IsNullOrEmpty(FullPath) && File.Exists(FullPath))
            {
                string content = File.ReadAllText(FullPath);
                JObject obj = JObject.Parse(content);
                string jsonObjectType = obj["Type"]?.ToString() ?? "";
                string input = obj["Input"]?.ToString() ?? "";

                Lab9.Purple.Purple result = null;

                switch (jsonObjectType)
                {
                    case "Task1": result = new Task1(input); break;
                    case "Task2": result = new Task2(input); break;
                    case "Task3": result = new Task3(input); break;
                    case "Task4":
                        var codesArray = obj["Codes"] as JArray;
                        (string, char)[] codes = null;
                        if (codesArray != null)
                        {
                            codes = new (string, char)[codesArray.Count];
                            for (int i = 0; i < codesArray.Count; i++)
                            {
                                var item = codesArray[i];
                                string item1 = item["Item1"]?.ToString() ?? "";
                                string item2Str = item["Item2"]?.ToString() ?? " ";
                                char item2 = item2Str.Length > 0 ? item2Str[0] : ' ';
                                codes[i] = (item1, item2);
                            }
                        }
                        result = new Task4(input, codes);
                        break;
                }

                if (result == null)
                {
                    Type type = typeof(Lab9.Purple.Purple).Assembly.GetType($"Lab9.Purple.{jsonObjectType}");
                    if (type != null)
                    {
                        obj.Remove("Type");
                        try { result = obj.ToObject(type) as Lab9.Purple.Purple; } catch { }
                    }
                }

                if (result != null)
                {
                    result.Review();
                    return result as T;
                }
            }
            return null;
        }
    }
}