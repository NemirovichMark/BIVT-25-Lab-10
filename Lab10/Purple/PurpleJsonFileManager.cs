using System;
using System.Linq;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lab10.Purple
{
    public class PurpleJsonFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
    {
        public PurpleJsonFileManager(string name) : base(name) { }
        public PurpleJsonFileManager(string name, string folder, string fileName, string ext = "txt") : base(name, folder, fileName, ext) { }

        public override void EditFile(string content)
        {
            var obj = Deserialize();
            if (obj != null)
            {
                obj.ChangeText(content);
                Serialize(obj);
            }
        }

        public override void ChangeFileExtension(string newExtension)
        {
            ChangeFileFormat("json");
        }

        public override void Serialize(T obj)
        {
            var dto = new
            {
                Type = obj.GetType().Name,
                Input = obj.Input,
                Codes = (obj is Lab9.Purple.Task4 t4) ? t4.Codes.Select(c => new { pair = c.Item1, code = c.Item2.ToString() }).ToArray() : null
            };

            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(dto, options);
            Directory.CreateDirectory(Path.GetDirectoryName(FullPath) ?? string.Empty);
            File.WriteAllText(FullPath, json);
        }

        public override T? Deserialize()
        {
            if (!File.Exists(FullPath)) return null!;
            var text = File.ReadAllText(FullPath);
            using (var doc = JsonDocument.Parse(text))
            {
                var root = doc.RootElement;
                var type = root.GetProperty("Type").GetString();
                var input = root.GetProperty("Input").GetString() ?? string.Empty;

                if (type == nameof(Lab9.Purple.Task1))
                {
                    var o = (T)(object)new Lab9.Purple.Task1(input);
                    o.Review();
                    return o;
                }
                if (type == nameof(Lab9.Purple.Task2))
                {
                    var o = (T)(object)new Lab9.Purple.Task2(input);
                    o.Review();
                    return o;
                }
                if (type == nameof(Lab9.Purple.Task3))
                {
                    var o = (T)(object)new Lab9.Purple.Task3(input);
                    o.Review();
                    return o;
                }
                if (type == nameof(Lab9.Purple.Task4))
                {
                    var codesList = new (string, char)[0];
                    if (root.TryGetProperty("Codes", out var codesElem) && codesElem.ValueKind == JsonValueKind.Array)
                    {
                        var list = new System.Collections.Generic.List<(string, char)>();
                        foreach (var e in codesElem.EnumerateArray())
                        {
                            var pair = e.GetProperty("pair").GetString() ?? string.Empty;
                            var codeStr = e.GetProperty("code").GetString();
                            var ch = string.IsNullOrEmpty(codeStr) ? '\0' : codeStr[0];
                            list.Add((pair, ch));
                        }
                        codesList = list.ToArray();
                    }
                    {
                        var o = (T)(object)new Lab9.Purple.Task4(input, codesList);
                        o.Review();
                        return o;
                    }
                }
                return null!;
            }
        }
    }
}
