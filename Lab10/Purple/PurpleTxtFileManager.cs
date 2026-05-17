using Lab9.Purple;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Purple
{
    public class PurpleTxtFileManager<T> : PurpleFileManager<T>
        where T : Lab9.Purple.Purple
    {
        public PurpleTxtFileManager(string name) : base(name) { }

        public PurpleTxtFileManager(string name, string folderPath, string fileName, string fileExtension = "txt")
            : base(name, folderPath, fileName, fileExtension) { }

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
            ChangeFileFormat("txt");
        }

        public override void Serialize(T obj)
        {
            if (obj != null)
            {
                var dict = new Dictionary<string, string>();
                dict["Type"] = obj.GetType().Name;
                dict["Input"] = obj.Input;

                if (obj is Task3 task3)
                {
                    if (task3.Table != null)
                    {
                        dict["Count"] = task3.Table.Length.ToString();
                        for (int i = 0; i < task3.Table.Length; i++)
                        {
                            dict[$"Code{i}"] = task3.Table[i].Item1 + "|" + task3.Table[i].Item2;
                        }
                    }
                }
                else if (obj is Task4 task4)
                {
                    if (task4.Table != null)
                    {
                        dict["Count"] = task4.Table.Length.ToString();
                        for (int i = 0; i < task4.Table.Length; i++)
                        {
                            dict[$"Code{i}"] = task4.Table[i].Item1 + "|" + task4.Table[i].Item2;
                        }
                    }
                }

                string[] result = new string[dict.Count];
                int z = 0;
                foreach (var el in dict)
                {
                    result[z++] = el.Key + ": " + el.Value;
                }

                File.WriteAllLines(FullPath, result);
            }
        }

        public override T Deserialize()
        {
            if (File.Exists(FullPath))
            {
                var dict = new Dictionary<string, string>();
                using (StreamReader reader = new StreamReader(FullPath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        int i = line.IndexOf(':');
                        if (i >= 0)
                            dict[line.Substring(0, i)] = line.Substring(i + 1).Trim();
                    }
                }

                if (!dict.ContainsKey("Type") || !dict.ContainsKey("Input"))
                    return null;

                string type = dict["Type"];
                string input = dict["Input"];

                (string, char)[] table = null;
                if (dict.ContainsKey("Count"))
                {
                    int count = int.Parse(dict["Count"]);
                    table = new (string, char)[count];
                    for (int x = 0; x < count; x++)
                    {
                        string temp = dict[$"Code{x}"];
                        var parts = temp.Split('|');
                        if (parts.Length == 2 && parts[1].Length > 0)
                        {
                            table[x] = (parts[0], parts[1][0]);
                        }
                    }
                }

                Lab9.Purple.Purple obj;
                switch (type)
                {
                    case "Task1":
                        obj = new Task1(input);
                        break;
                    case "Task2":
                        obj = new Task2(input);
                        break;
                    case "Task3":
                        obj = new Task3(input);
                        break;
                    case "Task4":
                        obj = new Task4(input, table);
                        break;
                    default:
                        return null;
                }

                obj.Review();
                return (T)obj;
            }
            else
                return null;
        }
    }
}
