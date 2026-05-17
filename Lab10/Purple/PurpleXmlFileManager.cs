using System;
using System.IO;
using System.Xml.Serialization;

namespace Lab10.Purple
{
    public class PurpleXmlFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
    {
        public PurpleXmlFileManager(string name) : base(name) { }
        public PurpleXmlFileManager(string name, string folder, string fileName, string ext = "txt") : base(name, folder, fileName, ext) { }

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
            // change only to xml
            ChangeFileFormat("xml");
        }

        public override void Serialize(T obj)
        {
            var dto = new DTOPurple();
            dto.Type = obj.GetType().Name;
            dto.Input = obj.Input;
            if (obj is Lab9.Purple.Task4 t4)
            {
                dto.Codes = new DTOPair[t4.Codes.Length];
                for (int i = 0; i < t4.Codes.Length; i++)
                {
                    dto.Codes[i] = new DTOPair { pair = t4.Codes[i].Item1, code = t4.Codes[i].Item2.ToString() };
                }
            }

            Directory.CreateDirectory(Path.GetDirectoryName(FullPath) ?? string.Empty);
            var ser = new XmlSerializer(typeof(DTOPurple));
            using (var fs = File.Create(FullPath))
                ser.Serialize(fs, dto);
        }

        public override T? Deserialize()
        {
            if (!File.Exists(FullPath)) return null;
            try
            {
                var ser = new XmlSerializer(typeof(DTOPurple));
                using (var fs = File.OpenRead(FullPath))
                {
                    var dto = (DTOPurple)ser.Deserialize(fs);
                    if (dto == null) return null;
                    var type = dto.Type ?? string.Empty;
                    var input = dto.Input ?? string.Empty;
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
                        var list = new System.Collections.Generic.List<(string, char)>();
                        if (dto.Codes != null)
                        {
                            foreach (var p in dto.Codes)
                            {
                                var ch = string.IsNullOrEmpty(p.code) ? '\0' : p.code[0];
                                list.Add((p.pair, ch));
                            }
                        }
                        var o = (T)(object)new Lab9.Purple.Task4(input, list.ToArray());
                        o.Review();
                        return o;
                    }
                }
            }
            catch
            {
                return null;
            }
            return null;
        }
    }
}
