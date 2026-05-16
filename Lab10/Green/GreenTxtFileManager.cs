using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Green
{
    public class GreenTxtFileManager : GreenFileManager
    {
        public GreenTxtFileManager(string name) : base(name)
        {
        }

        public GreenTxtFileManager(string name, string folderpath, string filename, string fileextension = "txt") : base(name, folderpath, filename, fileextension)
        {
        }

        public override void EditFile(string text)
        {
            if (text == null || text == "" || !File.Exists(FullPath)) return;

            Lab9.Green.Green obj = Deserialize<Lab9.Green.Green>();

            obj.ChangeText(text);
            Serialize(obj);
        }

        public override void ChangeFileExtension(string extension)
        {
            if (extension == null || extension == "" || extension != "txt") return;

            ChangeFileFormat(extension);
        }

        public override T Deserialize<T>()
        {
            if (!File.Exists(FullPath)) return null;

            string[] lines = File.ReadAllLines(FullPath);

            string type = null;
            string input = null;
            string pattern = null;

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("Type:"))
                {
                    type = lines[i].Substring(5);
                }
                if (lines[i].StartsWith("Input:"))
                {
                    input = lines[i].Substring(6);
                }
                if (lines[i].StartsWith("Pattern:"))
                {
                    pattern = lines[i].Substring(8);
                }
            } 

            Lab9.Green.Green obj = null;

            switch (type)
            {
                case "Task1":
                    obj = new Lab9.Green.Task1(input);
                    break;
                case "Task2":
                    obj = new Lab9.Green.Task2(input);
                    break;
                case "Task3":
                    obj = new Lab9.Green.Task3(input, pattern);
                    break;
                case "Task4":
                    obj = new Lab9.Green.Task4(input);
                    break;
            }

            if (obj != null) (obj).Review();

            return (T)obj;
        }

        public override void Serialize<T>(T obj)
        {
            if (!Directory.Exists(FolderPath)) Directory.CreateDirectory(FolderPath);

            string txt = "";

            txt += "Type:" + obj.GetType().Name + Environment.NewLine;
            txt += "Input:" + obj.Input + Environment.NewLine;

            if (obj is Lab9.Green.Task3 task3)
            {
                txt += "Pattern:" + task3.Pattern + Environment.NewLine;
            }

            File.WriteAllText(FullPath, txt);


        }
    }
}
