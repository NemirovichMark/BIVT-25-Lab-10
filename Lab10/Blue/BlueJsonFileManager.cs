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

        public BlueJsonFileManager(string name, string folderPath, string fileName, string fileExtension = "json")
            : base(name, folderPath, fileName, fileExtension)
        {
            ChangeFileFormat("json");
        }

        public override void EditFile(string text)
        {
            if (FullPath == null || FullPath == string.Empty || !File.Exists(FullPath))
            {
                return;
            }

            T obj = Deserialize();

            if (obj == null)
            {
                return;
            }

            obj.ChangeText(text);
            Serialize(obj);
        }

        public override void ChangeFileExtension(string fileExtension)
        {
            ChangeFileFormat("json");
        }

        public override void Serialize(T obj)
        {
            if (obj == null || FullPath == null || FullPath == string.Empty)
            {
                return;
            }

            BlueData data = new BlueData();
            data.Type = obj.GetType().Name;
            data.Input = obj.Input;
            data.Sequence = GetSequence(obj);

            string json = JsonSerializer.Serialize(data);

            string folder = FolderPath;

            if (folder != null && folder != string.Empty && !Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            File.WriteAllText(FullPath, json);
        }

        public override T Deserialize()
        {
            if (FullPath == null || FullPath == string.Empty || !File.Exists(FullPath))
            {
                return null;
            }

            string json = File.ReadAllText(FullPath);
            BlueData data = JsonSerializer.Deserialize<BlueData>(json);

            if (data == null)
            {
                return null;
            }

            return CreateTask(data.Type, data.Input, data.Sequence);
        }

        private string GetSequence(T obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            if (obj is Lab9.Blue.Task2 task2)
            {
                return task2.Sequence;
            }

            return string.Empty;
        }

        private T CreateTask(string type, string input, string sequence)
        {
            if (type == "Task1")
            {
                return (T)(Lab9.Blue.Blue)new Lab9.Blue.Task1(input);
            }

            if (type == "Task2")
            {
                return (T)(Lab9.Blue.Blue)new Lab9.Blue.Task2(input, sequence);
            }

            if (type == "Task3")
            {
                return (T)(Lab9.Blue.Blue)new Lab9.Blue.Task3(input);
            }

            if (type == "Task4")
            {
                return (T)(Lab9.Blue.Blue)new Lab9.Blue.Task4(input);
            }

            return null;
        }
    }
}
