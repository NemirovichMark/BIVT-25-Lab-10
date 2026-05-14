using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Purple
{
    public class PurpleJsonFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
    {
        //Конструкторы должны быть идентичны конструкторам базового класса PurpleFileManager<T>
        public PurpleJsonFileManager(string name) : base(name)
        {
        }
        public PurpleJsonFileManager(string name, string folderpath, string filename, string fileExtension = "txt") : base(name, folderpath, filename, fileExtension)
        {
        }
        public override void EditFile(string input)
        {
            T content = Deserialize();
            content.ChangeText(input); // заменяем текст 
            Serialize(content);

        }
        public override void ChangeFileExtension(string extension)
        {
            if (extension == "json")
            {
                ChangeFileFormat("json");

            }
        }
        public override void Serialize(T obj)
        {

            if (obj == null) return;
            var temp = new
            {
                Type = obj.GetType().Name,
                Input = obj.Input,
               Codes = Codes(obj), // елсли обьукт таск4 то у него должн быть codes


            };
            string json = JsonConvert.SerializeObject(temp, Formatting.Indented); // Formatting.Indented - просто ддля красоты чтобы не ыбло все в одну строку 
            ChangeFileExtension("json"); // Ззаменяем тип файла 
            base.EditFile(json);


        }
        private (string, char)[] Codes(T obj)
        {
            (string, char)[] cod;
            if (obj is Lab9.Purple.Task4 obj2)
            {
                cod = obj2.Codes;
            }
            else
            {
                cod = new (string, char)[0];
            }
            return cod;
        }

        public override T Deserialize()
        {
            if (!File.Exists(FullPath)) return null;

            string content = File.ReadAllText(FullPath);

            dynamic data = JsonConvert.DeserializeObject(content); // динамический обьект - позволяет обращаться к обьектам джисон data.type


            string type = data.Type;
            string input = data.Input;
            if (type == "Task1")
            {
                var obj = new Lab9.Purple.Task1(input) as T;
                obj.Review();
                return obj;
            }

            else if (type == "Task2")
            {
                var obj = new Lab9.Purple.Task2(input) as T;
                obj.Review();
                return obj;
            }
            else if (type == "Task3")
            {
                var obj = new Lab9.Purple.Task3(input) as T;
                obj.Review();
                return obj;
            }
            else if (type == "Task4")
            {
                var codes = ((JArray)data.Codes).ToObject<(string, char)[]>();
                // сначала преобразовыем в массив JArray потом с помощью .ToObject говорим какой именно массив у обьекта для инт можно просто .ToObject<int[]> так как нет масива в массиве
                var obj = new Lab9.Purple.Task4(input, codes) as T;
                obj.Review();
                return obj;
            }
            else
            {
                return null;
            }

           
        }
    }

}

