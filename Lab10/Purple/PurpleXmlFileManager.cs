using Lab9.Purple;
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
        public PurpleXmlFileManager(string name) : base(name) { }
        public PurpleXmlFileManager(string name, string folderPath, string fileName, string fileExtension = "txt")
            : base(name, folderPath, fileName, fileExtension) { }

        public override void EditFile(string text)
        {
            T obj = Deserialize();
            obj.ChangeText(text);
            Serialize(obj);
        }
        public override void ChangeFileExtension(string extension)
        {
            base.ChangeFileExtension("xml");
        }
        public override void Serialize(T obj)
        {
            if (obj == null) return;

            DTOPurple dtopurple = new DTOPurple(obj);

            var serializer = new XmlSerializer(typeof(DTOPurple));
            using (var writer = new StreamWriter(FullPath))
            {
                serializer.Serialize(writer, dtopurple);
            }
        }
        public override T Deserialize()
        {
            if (!File.Exists(FullPath)) return null;

            DTOPurple objDTO;
            T obj = null;

            var serializer = new XmlSerializer(typeof(DTOPurple));
            using (var reader = new StreamReader(FullPath))
            {
                objDTO = (DTOPurple)serializer.Deserialize(reader);
            }
            if (objDTO == null) return null; 

            if (objDTO.Type == "Task1")
            {
                obj = (new Task1(objDTO.Input) as T);
            }
            else if (objDTO.Type == "Task2")
            {
                obj = (new Task2(objDTO.Input) as T);
            }
            else if (objDTO.Type == "Task3")
            {
                obj = (new Task3(objDTO.Input) as T);
            }
            else if (objDTO.Type == "Task4")
            {
                obj = (new Task4(objDTO.Input, objDTO.CodesT4) as T);
            }
            
            if (obj == null) return null;
            obj.Review();

            return obj;
        }
    }
    public class DTOPurple
    {
        public string Type { get; set; }
        public string Input { get; set; }
        public (string, char)[] CodesT4 { get; set; }
        public DTOPurple() { }
        public DTOPurple(Lab9.Purple.Purple obj)
        {
            Type = obj.GetType().Name;
            Input = obj.Input;
            if (obj is Task4 o)
            {
                CodesT4 = o.Codes;
            }
        }
    }
}
