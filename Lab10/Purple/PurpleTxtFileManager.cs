using System;
using System.IO;

using Task1 = Lab9.Purple.Task1;
using Task2 = Lab9.Purple.Task2;
using Task3 = Lab9.Purple.Task3;
using Task4 = Lab9.Purple.Task4;
using PurpleTask = Lab9.Purple.Purple;

namespace Lab10.Purple
{
    public class PurpleTxtFileManager<T> : PurpleFileManager<T>
        where T : Lab9.Purple.Purple
    {
        public PurpleTxtFileManager(string name) : base(name)
        {
            ChangeFileFormat("txt");
        }

        public PurpleTxtFileManager(string name, string folderName, string fileName, string fileExtension = "txt")
            : base(name, folderName, fileName, fileExtension)
        {
            ChangeFileFormat("txt");
        }

        public override void Serialize(T obj)
        {
            if (obj == null)
            {
                return;
            }

            string text = "";

            text += "Type:" + GetTaskType(obj) + "\n";
            text += "Input:" + GetTaskInput(obj) + "\n";

            (string pair, char code)[] codes = GetTaskCodes(obj);

            for (int i = 0; i < codes.Length; i++)
            {
                text += "Code:" + codes[i].pair + "|" + codes[i].code + "\n";
            }

            base.EditFile(text);
        }

        public override T Deserialize()
        {
            if (!File.Exists(FullPath))
            {
                return null;
            }

            string[] lines = File.ReadAllLines(FullPath);

            string type = "Task1";
            string input = "";
            (string pair, char code)[] codes = new (string, char)[0];

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("Type:"))
                {
                    type = lines[i].Substring(5);
                }
                else if (lines[i].StartsWith("Input:"))
                {
                    input = lines[i].Substring(6);
                }
                else if (lines[i].StartsWith("Code:"))
                {
                    AddCode(ref codes, lines[i].Substring(5));
                }
            }

            return MakeTask(type, input, codes);
        }

        public override void EditFile(string text)
        {
            T task = Deserialize();

            if (task == null)
            {
                return;
            }

            task.ChangeText(text);

            Serialize(task);
        }

        public override void ChangeFileExtension(string text)
        {
            ChangeFileFormat("txt");
        }

        private string GetTaskType(T obj)
        {
            if (obj == null)
            {
                return "";
            }

            return obj.GetType().Name;
        }

        private string GetTaskInput(T obj)
        {
            if (obj == null)
            {
                return "";
            }

            return obj.Input;
        }

        private (string pair, char code)[] GetTaskCodes(T obj)
        {
            if (obj == null)
            {
                return new (string, char)[0];
            }

            if (obj.GetType().Name == "Task3")
            {
                Task3 task3 = obj as Task3;

                if (task3 != null)
                {
                    return ConvertCodes(task3.Codes);
                }
            }

            if (obj.GetType().Name == "Task4")
            {
                Task4 task4 = obj as Task4;

                if (task4 != null)
                {
                    return ConvertCodes(task4.Codes);
                }
            }

            return new (string, char)[0];
        }

        private (string pair, char code)[] ConvertCodes((string, char)[] codes)
        {
            if (codes == null)
            {
                return new (string, char)[0];
            }

            (string pair, char code)[] result = new (string, char)[codes.Length];

            for (int i = 0; i < codes.Length; i++)
            {
                result[i] = (codes[i].Item1, codes[i].Item2);
            }

            return result;
        }

        private void AddCode(ref (string pair, char code)[] codes, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            int index = text.LastIndexOf('|');

            if (index < 0)
            {
                return;
            }

            if (index == text.Length - 1)
            {
                return;
            }

            string pair = text.Substring(0, index);
            char code = text[index + 1];

            Array.Resize(ref codes, codes.Length + 1);
            codes[codes.Length - 1] = (pair, code);
        }

        private T MakeTask(string type, string input, (string pair, char code)[] codes)
        {
            PurpleTask task;

            if (type == "Task2")
            {
                task = new Task2(input);
            }
            else if (type == "Task3")
            {
                task = new Task3(input);
            }
            else if (type == "Task4")
            {
                (string, char)[] taskCodes = new (string, char)[codes.Length];

                for (int i = 0; i < codes.Length; i++)
                {
                    taskCodes[i] = (codes[i].pair, codes[i].code);
                }

                task = new Task4(input, taskCodes);
            }
            else
            {
                task = new Task1(input);
            }

            task.Review();

            return (T)task;
        }
    }
}