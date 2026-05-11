using Lab10.Purple;
using Lab9.Purple;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab10.Purple
{
    public  class PurpleJsonFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
    {
        public PurpleJsonFileManager(string name) : base(name) { }
        public PurpleJsonFileManager(string name, string folderPath, string filename, string fileExtension = ".json") : base(name, folderPath, filename, fileExtension) { }
        public override void EditFile(string a)
        {
            T obj=Deserialize(); // Загружаем объект из файла
            if (obj != null)
            { obj.ChangeText(a); Serialize(obj); } // Меняем текст у объекта,
                                                   // Сохраняем объект обратно в файл
        }
        public override void ChangeFileExtension(string e)
        {
            base.ChangeFileExtension("json");
        }
        public override void Serialize(T obj)
        {
            if (obj == null) return;
            JObject jobj = JObject.FromObject(obj); // obj => JObject 
            jobj.Add("Type", obj.GetType().Name); // + поле "Type" с именем класса
            File.WriteAllText(FullPath, jobj.ToString());
        }
        public override T Deserialize()
        {
            if (FullPath == null || !File.Exists(FullPath)) return null;
            string a = File.ReadAllText(FullPath);
            var json = JObject.Parse(a); // строка JSON => объект JObject (парсинг JSON)
            string tName = json["Type"].ToString(); 
            string input = json["Input"].ToString(); 
            if (tName is null || input is null) return null;
            T obj = tName switch
            {
                "Task1" => (T)(object)new Task1(input),
                "Task2" => (T)(object)new Task2(input),
                "Task3" => (T)(object)new Task3(input),
                "Task4" => (T)(object)new Task4(input,
                    json["Codes"] ?.ToObject<(string, char)[]>() ?? Array.Empty<(string, char)>())
                // Берём поле "Codes", превращаем JSON-массив в C#-массив пар (строка, символ),
                // 
                //(string, char)[] codes;
                //if (json["Codes"] == null)
                //      codes = new (string, char)[0];
                //else   codes = json["Codes"].ToObject<(string, char)[]>();
                //new Task4(input, codes);
                };
            obj.Review(); // формирует output
            return obj;
        }
    }
}
