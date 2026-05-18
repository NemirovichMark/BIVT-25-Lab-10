using System.IO;
using System.Text.Json;

namespace Lab10.Blue
{
    public class BlueJsonFileManager<T> : BlueFileManager<T>
        where T : Lab9.Blue.Blue
    {
        private class BlueData
        {
            public string Type { get; set; }

            public string Input { get; set; }

            public string Sequence { get; set; }
        }

        public BlueJsonFileManager(string name) : base(name)
        {
            ChangeFileFormat("json");
        }

        public BlueJsonFileManager(string name, string folder, string file, string ext = "json")
            : base(name, folder, file, ext)
        {
            ChangeFileFormat("json");
        }

        public override void EditFile(string content)
        {
            T obj = Deserialize();

            if (obj == null)
            {
                return;
            }

            obj.ChangeText(content);

            Serialize(obj);
        }

        public override void ChangeFileExtension(string extension)
        {
            ChangeFileFormat("json");
        }

        public override void Serialize(T obj)
        {
            if (obj == null)
            {
                return;
            }

            BlueData data = new BlueData();

            data.Type = obj.GetType().Name;
            data.Input = obj.Input;

            if (obj is Lab9.Blue.Task2 task2)
            {
                data.Sequence = task2.Sequence;
            }
            else
            {
                data.Sequence = string.Empty;
            }

            string json = JsonSerializer.Serialize(data);

            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }

            File.WriteAllText(FullPath, json);
        }

        public override T Deserialize()
        {
            if (!File.Exists(FullPath))
            {
                return null;
            }

            string json = File.ReadAllText(FullPath);

            BlueData data = JsonSerializer.Deserialize<BlueData>(json);

            if (data == null)
            {
                return null;
            }

            Lab9.Blue.Blue task = null;

            if (data.Type == "Task1")
            {
                task = new Lab9.Blue.Task1(data.Input);
            }
            else if (data.Type == "Task2")
            {
                task = new Lab9.Blue.Task2(data.Input, data.Sequence);
            }
            else if (data.Type == "Task3")
            {
                task = new Lab9.Blue.Task3(data.Input);
            }
            else if (data.Type == "Task4")
            {
                task = new Lab9.Blue.Task4(data.Input);
            }

            if (task != null)
            {
                task.Review();
            }

            return (T)task;
        }
    }
}
