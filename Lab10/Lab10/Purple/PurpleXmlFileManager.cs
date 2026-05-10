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
        public PurpleXmlFileManager(string name, string folderPath, string filename, string fileExtension = "") : base(name, folderPath, filename, fileExtension) { }
        public override void EditFile(string a)
        {
            T obj = Deserialize();
            if (obj != null)
            {
                obj.ChangeText(a);
                Serialize(obj);
            }
        }
        public override void ChangeFileExtension(string e)
        {
            ChangeFileExtension("xml");
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
