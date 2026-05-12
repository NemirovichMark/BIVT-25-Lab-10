using Lab9.Purple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab10.Purple
{
    public class PurpleXmlFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
    {
        public PurpleXmlFileManager(string name) : base(name) { }
        public PurpleXmlFileManager(string name, string folder, string fileName, string ext = "txt") : base(name, folder, fileName, ext)
        {
        }

        public override void EditFile(string text)
        {
            var obj = Deserialize();
            obj.ChangeText(text);
            Serialize(obj);
        }

        public override void ChangeFileExtension(string fileExtension)
        {
            fileExtension = "xml";
            var obj = Deserialize();
            ChangeFileExtension(fileExtension);
            Serialize(obj);
        }

        public override void Serialize(T obj)
        {
            var DTObj = new DTOPurple(obj);
            var ser = new XmlSerializer(typeof(DTOPurple));

            using (var fs = new StreamWriter(FullPath))
            {
                ser.Serialize(fs, DTObj);
            }

        }
        public override T Deserialize()
        {
            if (!File.Exists(FullPath)) return null;

            var ser = new XmlSerializer(typeof(DTOPurple));
            using (var fs = new StreamReader(FullPath))
            {

                var dtoObj = ser.Deserialize(fs) as DTOPurple;
                var resObj = dtoObj.GetResultObj();
                return (T)resObj;
            }
            
        }


    }

   
}
