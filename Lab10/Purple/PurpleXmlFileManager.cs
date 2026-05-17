using Lab9.Purple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab10.Purple
{
    public class PurpleXmlFileManager<T> : PurpleFileManager<T>
        where T : Lab9.Purple.Purple
    {
        public PurpleXmlFileManager(string name) : base(name) { }

        public PurpleXmlFileManager(string name, string folderPath, string fileName, string fileExtension = "txt") : base(name, folderPath, fileName, fileExtension) { }

        public override void EditFile(string content)
        {
            T obj = Deserialize();
            if (obj != null)
            {
                obj.ChangeText(content);
                Serialize(obj);
            }
        }
        public override void ChangeFileExtension(string extension)
        {
            T obj = Deserialize();
            ChangeFileFormat("xml");
            if (obj != null)
                Serialize(obj);
        }
        public override void Serialize(T obj)
        {
            if (obj != null && FullPath != null)
            {
                var d = new DTOPurple(obj);
                var serializer = new XmlSerializer(typeof(DTOPurple));
                using (var sw = new StreamWriter(FullPath))
                    serializer.Serialize(sw, d);
            }
        }

        public override T Deserialize()
        {
            var serializer = new XmlSerializer(typeof(DTOPurple));
            if (string.IsNullOrEmpty(FullPath) || !File.Exists(FullPath)) return null;
            using (var sr = new StreamReader(FullPath))
            {
                var d = (DTOPurple)serializer.Deserialize(sr);
                Lab9.Purple.Purple result;
                switch (d.TypeName)
                {
                    case "Task1":
                        result = new Task1(d.Input);
                        break;
                    case "Task2":
                        result = new Task2(d.Input);
                        break;
                    case "Task3":
                        result = new Task3(d.Input);
                        break;
                    case "Task4":
                        result = new Task4(d.Input, d.GetDecodedCodes());
                        break;
                    default:
                        return null;
                }
                result.Review();
                return (T)result;
            }
        }
    }
}
