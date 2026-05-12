using System.Xml.Serialization;

namespace Lab10.Purple
{
    public class PurpleXmlFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
    {
        public PurpleXmlFileManager(string name) : base(name) { }

        public PurpleXmlFileManager(string name, string folderPath, string fileName, string fileExtension = "txt")
            : base(name, folderPath, fileName, fileExtension) { }

        public override void EditFile(string text)
        {
            T task = Deserialize();
            task.ChangeText(text );
            Serialize(task);
        }

        public override void ChangeFileExtension(string fileExtension)
        {
            T task = Deserialize();
            ChangeFileFormat("xml");
            Serialize(task);
        }

        public override void Serialize(T obj)
        {
            ChangeFileFormat("xml");
            XmlSerializer serializer = new XmlSerializer(typeof(DTOPurple));

            using (StreamWriter writer = new StreamWriter(FullPath)) {

                serializer.Serialize(writer, new DTOPurple(obj));
            }
        }

        public override T Deserialize()
        {
            if (!File.Exists(FullPath))
                return null;

            XmlSerializer serializer = new XmlSerializer(typeof(DTOPurple));

            Lab9.Purple.Purple obj;

            using (StreamReader reader = new StreamReader(FullPath)) 
            {
                DTOPurple dto = serializer.Deserialize(reader) as DTOPurple;
                obj = dto.GetTask();
            }
            return (T)obj;
        }
    }
}
