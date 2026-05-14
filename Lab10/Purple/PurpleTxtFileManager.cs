using Lab9.Purple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Purple
{
    public class PurpleTxtFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
    {
        public PurpleTxtFileManager(string name) : base(name)
        {
        }
        public PurpleTxtFileManager(string name, string folderpath, string filename, string fileExtension = "txt") : base(name, folderpath, filename, fileExtension)
        {
        }
        public override void EditFile(string file)
        {
            T obj = Deserialize();
            obj.ChangeText(file);
            Serialize(obj);
        }
        public override void ChangeFileExtension(string file)
        {
            ChangeFileFormat("txt");
        }
        public override void Serialize(T obj)
        {
            if (obj == null || FullPath == null) return;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Input"] = obj.Input;
            dic["Type"] = obj.GetType().Name;

            if (dic["Type"] == "Task1")
            {
                var obj1 = obj as Lab9.Purple.Task1;
                dic["Output"] = obj1.Output;
            }
            else if (dic["Type"] == "Task2")
            {
                var obj1 = obj as Lab9.Purple.Task2;
                dic["Output"] = string.Join(Environment.NewLine, obj1.Output);
            }

            else if (dic["Type"] == "Task3")
            {
                var obj1 = obj as Lab9.Purple.Task3;
                dic["Output"] = obj1.Output;
            }
            else if (dic["Type"] == "Task4")
            {
                var obj1 = obj as Lab9.Purple.Task4;
                dic["Output"] = obj1.Output;
                for (int i = 0; i < obj1.Codes.Length; i++)
                {
                    dic["Codes" + "" + i] = obj1.Codes[i].Item1 + " " + obj1.Codes[i].Item2;
                }

            }
            //string[] lines = new string[dic.Count];
            //int index = 0;
            string lines = "";
            foreach (var pair in dic)
            {
                lines += pair.Key + ":" + pair.Value + "\n";
                //lines[index++] = pair.Key + ":" + pair.Value;
            }

            // File.WriteAllLines(FullPath, lines); // просто строку подать нкльзя тольько массив строк 
            File.WriteAllText(FullPath, lines);// принимает только строки 
        }
        public override T Deserialize()
        {
            if (FullPath == null || !File.Exists(FullPath))
                return null;

            Dictionary<string, string> dic = new Dictionary<string, string>();
            string text = File.ReadAllText(FullPath);

            string[] stroki = text.Split('\n');

            for (int i = 0; i < stroki.Length; i++)
            {
                string[] str = stroki[i].Split(':', 2); // разделить по первому чтобы было только две части 
                if (str.Length == 2)
                {
                    dic[str[0].Trim()] = str[1].Trim();
                }
            }

            if (dic["Type"] == "Task1")
            {
                var obj = new Lab9.Purple.Task1(dic["Input"]) as T;

                obj.Review();

                return obj;
            }
            else if (dic["Type"] == "Task2")
            {
                var obj = new Lab9.Purple.Task2(dic["Input"]) as T;

                obj.Review();

                return obj;
            }
            else if (dic["Type"] == "Task3")
            {
                var obj = new Lab9.Purple.Task3(dic["Input"]) as T;

                obj.Review();

                return obj;
            }
            else if (dic["Type"] == "Task4")
            {
                (string, char)[] codes = new (string, char)[0];
                string[] stroki2 = text.Split("\n");
                for (int i = 0; i < stroki2.Length; i++)
                {

                    if (stroki2[i].Contains("Codes"))
                    {
                        string str = stroki2[i].Replace(":", " : ");

                        string[] cod = str.Split();

                        string trimone = cod[2].Trim();

                        char trimtwo = cod[3].Trim()[0];
                        var itog = (trimone, trimtwo);
                        Array.Resize(ref codes, codes.Length + 1);
                        codes[codes.Length - 1] = itog;
                    }

                }
                var obj = new Lab9.Purple.Task4(dic["Input"], codes) as T;
                obj.Review();
                return obj;

            }

            return null;
        }








    }
}
