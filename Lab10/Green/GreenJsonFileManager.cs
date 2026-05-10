using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab10.Green
{
    public class GreenJsonFileManager: GreenFileManager
    {
        public GreenJsonFileManager(string name) : base(name)
        {
        }

        public GreenJsonFileManager(string name, string folderPath, string fileName, string fileExtension = "json"): base(name, folderPath, fileName, fileExtension)
        {
        }

        // Переопределяем EditFile (Десериализация -> Изменение -> Сериализация)
        public override void EditFile(string newText)
        {
            var obj = Deserialize<Lab9.Green.Green>();
            obj.ChangeText(newText);
            Serialize(obj);
        }
        // Переопределяем смену расширения (только на json)
        public override void ChangeFileExtension(string newExtension)
        {
            ChangeFileFormat("json");
        }

        // Обобщенный метод Serialize
        public override void Serialize<T>(T obj) //// YTYTYTYTYT
        {
            if (obj == null) return;
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(obj, obj.GetType(), options);

            if (json.StartsWith("{"))
            {
                json = json.Insert(1, $"\n  \"TypeName\": \"{obj.GetType().FullName}\",");
            }
            File.WriteAllText(FullPath, json);
        }
        public override T Deserialize<T>()
        {
            if (!File.Exists(FullPath)) return null;
            string json = File.ReadAllText(FullPath);
            using (JsonDocument doc = JsonDocument.Parse(json))
            {
                var root = doc.RootElement;
                Type actualType = typeof(T);

                if (root.TryGetProperty("TypeName", out var typeEl))
                {
                    string typeName = typeEl.GetString();
                    if (!string.IsNullOrEmpty(typeName))
                    {
                        actualType = typeof(Lab9.Green.Green).Assembly.GetType(typeName) ?? typeof(T);
                    }
                }

                var ctors = actualType.GetConstructors();
                if (ctors.Length == 0) return null;

                var ctor = ctors[0]; 
                var pInfos = ctor.GetParameters();
                object[] args = new object[pInfos.Length];

                for (int i = 0; i < pInfos.Length; i++)
                {
                    string pName = pInfos[i].Name.ToLower();

                    foreach (var prop in root.EnumerateObject())
                    {
                        string jsonPropName = prop.Name.ToLower();

                        if (jsonPropName == pName || jsonPropName.Contains(pName))
                        {
                            if (pInfos[i].ParameterType == typeof(string))
                            {
                                args[i] = prop.Value.GetString();
                            }
                            else
                            {
                                args[i] = null;
                            }
                            break;
                        }
                    }
                }

                T obj = (T)ctor.Invoke(args);

                if (obj != null)
                {
                    var reviewMethod = obj.GetType().GetMethod("Review");
                    if (reviewMethod != null)
                    {
                        reviewMethod.Invoke(obj, null);
                    }
                }
                return obj;
            }
        }
    }
}
