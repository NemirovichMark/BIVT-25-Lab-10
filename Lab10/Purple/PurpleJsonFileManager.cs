using System;
using System.IO;
using System.Text.Json;

using Task1 = Lab9.Purple.Task1;
using Task2 = Lab9.Purple.Task2;
using Task3 = Lab9.Purple.Task3;
using Task4 = Lab9.Purple.Task4;
using PurpleTask = Lab9.Purple.Purple;

namespace Lab10.Purple
{
    public class PurpleJsonFileManager<T> : PurpleFileManager<T>
        where T : Lab9.Purple.Purple
    {
        public PurpleJsonFileManager(string name) : base(name)
        {
            ChangeFileFormat("json");
        }

        public PurpleJsonFileManager(string name, string folderName, string fileName, string fileExtension = "json")
            : base(name, folderName, fileName, fileExtension)
        {
            ChangeFileFormat("json");
        }

        public override void Serialize(T obj)
        {
            if (obj == null)
            {
                return;
            }

            JsonPurpleData data = new JsonPurpleData();

            data.Type = GetTaskType(obj);
            data.Input = GetTaskInput(obj);
            data.Codes = MakeCodeData(GetTaskCodes(obj));

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;

            string json = JsonSerializer.Serialize(data, options);

            base.EditFile(json);
        }

        public override T Deserialize()
        {
            if (!File.Exists(FullPath))
            {
                return null;
            }

            try
            {
                string json = File.ReadAllText(FullPath);

                JsonPurpleData data = JsonSerializer.Deserialize<JsonPurpleData>(json);

                if (data == null)
                {
                    return null;
                }

                return MakeTask(data.Type, data.Input, ReadCodeData(data.Codes));
            }
            catch
            {
                return null;
            }
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
            ChangeFileFormat("json");
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

        private JsonCodeData[] MakeCodeData((string pair, char code)[] codes)
        {
            if (codes == null)
            {
                return new JsonCodeData[0];
            }

            JsonCodeData[] result = new JsonCodeData[codes.Length];

            for (int i = 0; i < codes.Length; i++)
            {
                result[i] = new JsonCodeData();

                result[i].Pair = codes[i].pair;
                result[i].Code = codes[i].code.ToString();
            }

            return result;
        }

        private (string pair, char code)[] ReadCodeData(JsonCodeData[] codes)
        {
            if (codes == null || codes.Length == 0)
            {
                return new (string, char)[0];
            }

            (string pair, char code)[] result = new (string, char)[codes.Length];

            for (int i = 0; i < codes.Length; i++)
            {
                string pair = "";
                char code = '\0';

                if (codes[i] != null)
                {
                    if (codes[i].Pair != null)
                    {
                        pair = codes[i].Pair;
                    }

                    if (!string.IsNullOrEmpty(codes[i].Code))
                    {
                        code = codes[i].Code[0];
                    }
                }

                result[i] = (pair, code);
            }

            return result;
        }

        private T MakeTask(string type, string input, (string pair, char code)[] codes)
        {
            PurpleTask task;

            if (input == null)
            {
                input = "";
            }

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

    public class JsonPurpleData
    {
        public string Type { get; set; }
        public string Input { get; set; }
        public JsonCodeData[] Codes { get; set; }

        public JsonPurpleData()
        {
            Type = "";
            Input = "";
            Codes = new JsonCodeData[0];
        }
    }

    public class JsonCodeData
    {
        public string Pair { get; set; }
        public string Code { get; set; }

        public JsonCodeData()
        {
            Pair = "";
            Code = "";
        }
    }
}