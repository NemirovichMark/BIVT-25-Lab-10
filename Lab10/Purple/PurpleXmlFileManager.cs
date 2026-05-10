using System.Reflection;
using System.Xml.Serialization;

namespace Lab10.Purple
{
    public class PurpleXmlFileManager<T> : PurpleFileManager<T> where T : global::Lab9.Purple.Purple
    {
        public PurpleXmlFileManager(string name) : base(name) { }

        public PurpleXmlFileManager(string name, string folder_path, string file_name, string file_extension = "xml")
            : base(name, folder_path, file_name, file_extension) { }

        public override void Serialize(T obj)
        {
            if (obj == null || string.IsNullOrEmpty(this.FullPath))
                return;

            if (!string.IsNullOrEmpty(this.FolderPath))
                Directory.CreateDirectory(this.FolderPath);

            DTOPurple dto = new DTOPurple
            {
                Type = obj.GetType().AssemblyQualifiedName ?? "",
                Input = obj.Input ?? "",
                Output = obj.ToString()
            };

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
                DTOPurple? dto = serializer.Deserialize(stream) as DTOPurple;

                if (dto == null || string.IsNullOrEmpty(dto.Type))
                    return null!;

                Type? type = Type.GetType(dto.Type);
                if (type == null || !typeof(T).IsAssignableFrom(type))
                    return null!;

                object? obj;
                if (type == typeof(global::Lab9.Purple.Task4))
                    obj = Activator.CreateInstance(type, dto.Input, null);
                else
                    obj = Activator.CreateInstance(type, dto.Input);

                if (obj == null)
                    return null!;

                FieldInfo? field = type.GetField("_output", BindingFlags.NonPublic | BindingFlags.Instance);
                if (field != null)
                {
                    if (field.FieldType == typeof(string))
                        field.SetValue(obj, dto.Output);
                    else if (field.FieldType == typeof(string[]))
                        field.SetValue(obj, dto.Output.Split('\n'));
                }

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
            if (obj == null)
                return;
            obj.ChangeText(text);
            this.Serialize(obj);
        }

        public override void ChangeFileExtension(string new_extension)
        {
            T obj = this.Deserialize();
            if (obj == null)
                return;
            this.ChangeFileFormat("xml");
            this.Serialize(obj);
        }
    }
}