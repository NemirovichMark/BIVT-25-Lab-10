using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Purple
{
    public class PurpleJsonFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
    {
        public PurpleJsonFileManager(string name) : base(name) { }
        public PurpleJsonFileManager(string name, string folderPath, string fileName, string fileExtension = "txt")
            : base(name, folderPath, fileName, fileExtension) { }

        public override void EditFile(string text)
        {
            var obj = Deserialize();
            if (obj == null) return;
            obj.ChangeText(text);
            Serialize(obj);
        }
        public override void ChangeFileExtension(string extension)
        {
            base.ChangeFileExtension("json");
        }
        public override void Serialize(T obj)
        {
            if (obj == null) return;

            var jsonObject = new JObject();
            jsonObject["Type"] = obj.GetType().Name;
            jsonObject["Input"] = obj.Input;

            if (obj is Lab9.Purple.Task4 task4 && task4.Codes != null)
            {
                var arr = new JArray();
                foreach (var c in task4.Codes)
                {
                    var o = new JObject();
                    o["pair"] = c.Item1;
                    o["code"] = c.Item2.ToString();
                    arr.Add(o);
                }
                jsonObject["Codes"] = arr;
            }

            File.WriteAllText(FullPath, jsonObject.ToString());
        }
        public override T Deserialize()
        {
            if (!File.Exists(FullPath)) return null;

            string jsonString = File.ReadAllText(FullPath);
            JObject jsonObject = JObject.Parse(jsonString);

            string typeName = jsonObject["Type"]?.ToString();
            if (typeName == null) return null;

            string input = jsonObject["Input"]?.ToString() ?? "";

            T obj = null;
            if (typeName == "Task1") obj = new Lab9.Purple.Task1(input) as T;
            else if (typeName == "Task2") obj = new Lab9.Purple.Task2(input) as T;
            else if (typeName == "Task3") obj = new Lab9.Purple.Task3(input) as T;
            else if (typeName == "Task4")
            {
                (string, char)[] codes = null;

                if (jsonObject["Codes"] is JArray codesArr && codesArr.Count > 0)
                {
                    codes = codesArr
                        .Select(x =>
                        {
                            var pair = x["pair"]?.ToString();
                            var code = x["code"]?.ToString();
                            if (pair == null || code == null || code.Length == 0) return (null, '\0');
                            return (pair, code[0]);
                        })
                        .ToArray();
                }
                obj = new Lab9.Purple.Task4(input, codes) as T;
            }

            if (obj == null) return null;
            obj.Review();
            return obj;
        }
    }
}
