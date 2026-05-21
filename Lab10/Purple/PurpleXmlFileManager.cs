using System.Reflection;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Lab10.Purple
{
    public class PurpleXmlFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
    {
        public PurpleXmlFileManager(string name) : base(name) { }

        public PurpleXmlFileManager(string name, string folder_path, string file_name, string file_extension = "xml")
            : base(name, folder_path, file_name, file_extension) { }

        public override void Serialize(T obj)
        {
            if (obj == null || string.IsNullOrEmpty(this.FullPath)) return;

            if (!string.IsNullOrEmpty(this.FolderPath))
                Directory.CreateDirectory(this.FolderPath);

            var dto = new DTOPurple
            {
                Type = obj.GetType().AssemblyQualifiedName ?? "",
                Input = obj.Input ?? "",
                Output = obj.ToString() ?? ""
            };

            if (obj is Lab9.Purple.Task4 task4)
            {
                var codes_list = task4.Codes.Select(pair_code => new { pair = pair_code.Item1, code = pair_code.Item2.ToString() }).ToList();
                dto.Codes = JsonConvert.SerializeObject(codes_list);
            }

            XmlSerializer serializer = new XmlSerializer(typeof(DTOPurple));
            using FileStream stream = new FileStream(this.FullPath, FileMode.Create);
            serializer.Serialize(stream, dto);
        }

        public override T Deserialize()
        {
            if (string.IsNullOrEmpty(this.FullPath) || !File.Exists(this.FullPath))
                return null!;

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(DTOPurple));
                using FileStream stream = new FileStream(this.FullPath, FileMode.Open);

                if (serializer.Deserialize(stream) is not DTOPurple dto || string.IsNullOrEmpty(dto.Type))
                    return null!;

                Type? type = Type.GetType(dto.Type);
                if (type == null || !typeof(T).IsAssignableFrom(type))
                    return null!;

                object? obj = null;

                if (type == typeof(Lab9.Purple.Task4))
                {
                    List<(string, char)> codes_list = [];
                    if (!string.IsNullOrEmpty(dto.Codes))
                    {
                        var deserialized = JsonConvert.DeserializeObject<List<dynamic>>(dto.Codes);
                        if (deserialized != null)
                        {
                            foreach (var item in deserialized)
                            {
                                string pair = item.pair;
                                string code_str = item.code;
                                char code = code_str.Length > 0 ? code_str[0] : '\0';
                                codes_list.Add((pair, code));
                            }
                        }
                    }
                    obj = Activator.CreateInstance(type, dto.Input, codes_list.ToArray());
                }
                else
                {
                    obj = Activator.CreateInstance(type, dto.Input);
                }

                if (obj == null)
                    return null!;

                ((Lab9.Purple.Purple)obj).Review();

                return (T)obj;
            }
            catch
            {
                return null!;
            }
        }

        public override void EditFile(string text)
        {
            T obj = this.Deserialize();
            if (obj == null) return;
            obj.ChangeText(text);
            this.Serialize(obj);
        }

        public override void ChangeFileExtension(string new_extension)
        {
            T obj = this.Deserialize();
            if (obj == null) return;
            this.ChangeFileFormat("xml");
            this.Serialize(obj);
        }
    }
}