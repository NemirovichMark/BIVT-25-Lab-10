using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab10.Purple
{
    public class PurpleXmlFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
    {
        public PurpleXmlFileManager(string name) : base(name)
        {
        }

        public PurpleXmlFileManager(string name, string folder, string fileName, string extension = "txt") : base(name, folder, fileName, extension)
        {
        }
        public override void EditFile(string fileContent)
        {
            T purple = Deserialize();
            purple.ChangeText(fileContent);
            Serialize(purple);
        }
        public override void ChangeFileExtension(string fileExtention)
        {
            ChangeFileFormat("xml");
        }
        public override void Serialize(T purple)
        {
            DTO tempObject = new DTO(purple);
            var serializer = new XmlSerializer(typeof(DTO));
            CreateFile();
            using (StreamWriter writer = new StreamWriter(FullPath))
            {
                serializer.Serialize(writer, tempObject);
            }
        }

        public override T Deserialize()
        {
            if (!File.Exists(FullPath)) return null;
            if (!File.Exists(FullPath)) return null!;
            Lab9.Purple.Purple purple;

            var serializer = new XmlSerializer(typeof(DTO));
            using (TextReader reader = new StreamReader(FullPath))
            {
                DTO tempObject = serializer.Deserialize(reader) as DTO;
                purple = tempObject.Transform();
            }
            return purple as T;
        }
    }
}
