using Lab9.Purple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Lab10.Purple
{
    public class PurpleXmlFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
    {
        public PurpleXmlFileManager(string name) : base(name) { }
        public PurpleXmlFileManager(string name, string folderpath, string filename, string fileextension = "xml") : base(name, folderpath, filename, fileextension) { }
        public override void EditFile(string file)
        {
            var obj = Deserialize();
            obj.ChangeText(file);
            Serialize(obj);
        }

        public override void ChangeFileExtension(string extension)
        {
            if (!File.Exists(FullPath)) return;
            var obj = Deserialize();
            ChangeFileFormat("xml");
            Serialize(obj);
        }

        public override void Serialize(T obj)
        {
            if (obj == null || FullPath == null) return;
            var serializer = new XmlSerializer(typeof(DTOPurple));

            DTOPurple dtopurple = new DTOPurple(obj);
            using (var writer = new StreamWriter(FullPath)) // writer - поток,открываем поток чтобы записать 
            {
                serializer.Serialize(writer, dtopurple); // передаем поток после чего то что серелиазуем (обяз дто)
            }


        }
        public override T Deserialize()
        {
            if (!File.Exists(FullPath)) return null;
            var serializer = new XmlSerializer(typeof(DTOPurple));
            DTOPurple purpledto;
            T obj = null;
            // поток для чтения 
            using (var read = new StreamReader(FullPath))
            {

                purpledto = (DTOPurple)serializer.Deserialize(read);
            }
            // десерелизуем обьект -> получает ДТО обьект -> оригинальный обьект 
            if (purpledto.TypeName == "Task1")
            {
                obj = new Lab9.Purple.Task1(purpledto.Input) as T;
                obj.Review();
                return obj;
            }
            else if (purpledto.TypeName == "Task2")
            {
                obj = new Lab9.Purple.Task2(purpledto.Input) as T;
                obj.Review();
                return obj;
            }
            else if (purpledto.TypeName == "Task3")
            {
                obj = new Lab9.Purple.Task3(purpledto.Input) as T;
                obj.Review();
                return obj;
            }
            else if (purpledto.TypeName == "Task4")
            {
                obj = new Lab9.Purple.Task4(purpledto.Input, purpledto.CodePairs) as T;
                obj.Review();
                return obj;
            }

            return null;

        }


    }
    public class DTOPurple // создаем дто класс так как базовым 
    {
        public string Input { get; set; }
        public string TypeName { get; set; }
        public string Output { get; set; }
        public (string, char)[] CodePairs { get; set; }
        public DTOPurple() { }
        public DTOPurple(Lab9.Purple.Purple obj)
        {
            Input = obj.Input;
            TypeName = obj.GetType().Name;
            Output = "";
          
            if (obj is Task4 task4)
            {
                CodePairs = task4.Codes;
            }
            else
            {
                CodePairs = null;
            }
        }
    }

}
