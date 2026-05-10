using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab10.Purple
{
    public class PurpleTxtFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
    {
        public PurpleTxtFileManager(string name) : base(name) { }
        public PurpleTxtFileManager(string name, string folderPath, string filename, string fileExtension = "") : base(name, folderPath, filename, fileExtension) { }
        public override void EditFile(string a)
        {
            T obj = Deserialize();
            if (obj != null)
            { obj.ChangeText(a); Serialize(obj); }
        }
        public override void ChangeFileExtension(string e)
        {
            ChangeFileExtension("txt");
        }
        public override void Serialize(T obj)
        {
            string txt = $"Input:{obj.Input}\nType:{obj.GetType().FullName}";
            File.WriteAllText(FullPath, txt);
        }
            public override T Deserialize()
        {
            if (!File.Exists(FullPath)) return null;
            string[] l = File.ReadAllLines(FullPath); //Читает файл => массив строк
            if (l.Length == 0) return null;
            var dict = new Dictionary<string, string>();
            foreach (var line in l)
            {
                int c = line.IndexOf(':'); // ищет :
                if (c > 0)
                {
                    string key = line.Substring(0, c).Trim(); // Берёт часть строки - пробелы в начале и конце
                    string value = line.Substring(c + 1).Trim(); 
                    dict[key] = value; //Сохр. пару ключ-значение в dict
                }
            }
            if (!dict.TryGetValue("Type", out string type)) return null; // если ключей нет
            if (!dict.TryGetValue("Input", out string input)) return null;
            switch (type)
            {
                case "Task1":
                    return (T)(object)new Lab9.Purple.Task1(input);

                case "Task2":
                    return (T)(object)new Lab9.Purple.Task2(input);

                case "Task3":
                    return (T)(object)new Lab9.Purple.Task3(input);

                case "Task4":
                    string codes = dict.GetValueOrDefault("Codes", "");
                    var array = PC(codes);
                    return (T)(object)new Lab9.Purple.Task4(input, array);

                default:
                    return null;
            }
        }
            private int[] PC(string a)
        {
            if (a==null) return new int[0];
            string[] p = a.Split(',');
            int[] result = new int[p.Length];
            for (int i = 0; i < p.Length; i++)
            {
                if (int.TryParse(p[i].Trim(), out int value))
                    result[i] = value;
            }
            return result;
        }
    }
}
