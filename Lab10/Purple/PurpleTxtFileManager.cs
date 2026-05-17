using System;
using System.IO;
using System.Linq;

namespace Lab10.Purple
{
    public class PurpleTxtFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
    {
        public PurpleTxtFileManager(string name) : base(name) { }
        public PurpleTxtFileManager(string name, string folder, string fileName, string ext = "txt") : base(name, folder, fileName, ext) { }

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
            ChangeFileFormat("txt");
        }

        public override void Serialize(T obj)
        {
            var lines = new System.Collections.Generic.List<string>();
            lines.Add($"Type: {obj.GetType().Name}");
            lines.Add($"Input: {obj.Input}");

            if (obj is Lab9.Purple.Task4 t4)
            {
                var codes = string.Join(";", t4.Codes.Select(c => $"{c.Item1}|{c.Item2}"));
                lines.Add($"Codes: {codes}");
            }

            Directory.CreateDirectory(Path.GetDirectoryName(FullPath) ?? string.Empty);
            File.WriteAllText(FullPath, string.Join(Environment.NewLine, lines));
        }

        public override T? Deserialize()
        {
            if (!File.Exists(FullPath)) return null;
            var lines = File.ReadAllLines(FullPath);
            string type = string.Empty;
            string input = string.Empty;
            (string, char)[] codes = new (string, char)[0];

            foreach (var l in lines)
            {
                var idx = l.IndexOf(':');
                if (idx < 0) continue;
                var key = l.Substring(0, idx).Trim();
                var val = l.Substring(idx + 1).Trim();
                if (key == "Type") type = val;
                else if (key == "Input") input = val;
                else if (key == "Codes")
                {
                    var parts = val.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    var list = new System.Collections.Generic.List<(string, char)>();
                    foreach (var p in parts)
                    {
                        var pair = p.Split('|');
                        if (pair.Length == 2)
                        {
                            var pr = pair[0];
                            var ch = pair[1].Length > 0 ? pair[1][0] : '\0';
                            list.Add((pr, ch));
                        }
                    }
                    codes = list.ToArray();
                }
            }

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
                var o = (T)(object)new Lab9.Purple.Task4(input, codes);
                o.Review();
                return o;
            }

            return null;
        }
    }
}
