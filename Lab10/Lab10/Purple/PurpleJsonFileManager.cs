using System;
using System.Collections.Generic;
using System.Linq;
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
            T obj=Deserialize();
            if (obj != null)
            { obj.ChangeText(a); Serialize(obj); }
        }
        public override void ChangeFileExtension(string e)
        {
            ChangeFileExtension("json");
        }
        public override void Serialize(T obj)
        { 
            if (obj == null) return;
            var d = new
            {
                input = obj.Input,
                type = obj.GetType().FullName, // полное имя 
            };
            string json=JsonSerializer.Serialize(d); // dto => json 
            File.WriteAllText(FullPath, json);
        }
        public override T Deserialize()
        {
            if (!File.Exists(FullPath)) return null;
            string json =File.ReadAllText(FullPath);  // Чтение содержимого файла
            if (json==null) return null;
            JObject jObj = JObject.Parse(json); // Парсинг JSON в JObject
            string tjObj = jObj["Type"].ToString(); // Получение имени типа из поля "Type"
            Type typeObject = Type.GetType($"Lab9.Purple.{tjObj}, Lab9"); // Получение Type объекта по имени
            jObj.Remove("Type");  // Удаление поля "Type" из JSON
            var obj = jObj.ToObject(tjObj); // Преобразование JSON в объект
            return (T)obj;
        }
    }
}
