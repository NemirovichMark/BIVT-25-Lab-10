using Lab9.Purple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using Lab10.Purple;

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
            base.ChangeFileExtension("txt");
        }
        public override void Serialize(T obj)
        {
            if (obj == null || FullPath == null) return;
            var p = new Dictionary<string, string>();
            p["Type"] = obj.GetType().Name;
            p["Input"] = obj.Input;
            if (obj is Task3 task3)
            {
                p["Count"] = task3.Codes.Length.ToString();
                for (int i = 0; i < task3.Codes.Length; i++)
                     p[$"Code{i}"] = task3.Codes[i].Item1 + "|" + task3.Codes[i].Item2;
            }             // Символ | разделяет строку и символ
            else if (obj is Task4 task4)
            {
                p["Count"] = task4.Codes.Length.ToString();
                for (int i = 0; i < task4.Codes.Length; i++)
                    p[$"Code{i}"] = task4.Codes[i].Item1 + "|" + task4.Codes[i].Item2;
            }
            string[] l = new string[p.Count];
            int ind = 0;
            foreach (var i in p)
                l[ind++] = i.Key + ":" + i.Value; // словарь => массив строк вида "Type:Task1"
            File.WriteAllLines(FullPath, l);
        }
        public override T Deserialize()
        {
            if (string.IsNullOrEmpty(FullPath) || !File.Exists(FullPath)) return null;
            var pairs = new Dictionary<string, string>();
            using (StreamReader reader = new StreamReader(FullPath))
            // Открываем файл для чтения (using => файл закроется автоматически).
            {
                string l;
                while ((l = reader.ReadLine()) != null)
                {
                    int ind = l.IndexOf(':');
                    if (ind >= 0)
                        pairs[l.Substring(0, ind)] = l.Substring(ind + 1); // разбиваем строку на ключ : значение
                }
            }
            if (!pairs.ContainsKey("Type") || !pairs.ContainsKey("Input")) return null;
            string tName = pairs["Type"];
            string input = pairs["Input"];
            (string, char)[] codes = null;
            if (pairs.ContainsKey("Count")) //  Task3, Task4
            {
                int count = int.Parse(pairs["Count"]); // Читаем количество кодов
                codes = new (string, char)[count];
                for (int i = 0; i < count; i++)
                {
                    string value = pairs[$"Code{i}"];
                    codes[i] = (value.Split("|")[0], value[value.Length - 1]);
                }
            }
            Lab9.Purple.Purple obj;
            if (tName == "Task1") obj = new Task1(input);
            else if (tName == "Task2") obj = new Task2(input);
            else if (tName == "Task3") obj = new Task3(input);
            else if (tName == "Task4") obj = new Task4(input, codes);
            else return null;
            obj.Review();
            return (T)obj;
        }
    }
}
