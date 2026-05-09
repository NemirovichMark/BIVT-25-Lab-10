using System.IO;

namespace Lab10.Blue
{
    public class BlueTxtFileManager<T> : BlueFileManager<T>
        where T : Lab9.Blue.Blue
    {
        private class BlueData
        {
            public string Type { get; set; }
            public string Input { get; set; }
            public string Sequence { get; set; }
        }

        public BlueTxtFileManager(string name) : base(name)
        {
            ChangeFileFormat("txt");
        }

        public BlueTxtFileManager(string name, string folderPath, string fileName, string fileExtension = "txt")
            : base(name, folderPath, fileName, fileExtension)
        {
            ChangeFileFormat("txt");
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
            ChangeFileFormat("txt");
        }

        public override void Serialize(T obj)
        {
            if (obj == null || FullPath == null || FullPath == string.Empty)
            {
                return;
            }

            string text = "Type:" + obj.GetType().Name + "\n" +
                          "Input:" + obj.Input + "\n" +
                          "Sequence:" + GetSequence(obj);

            string folder = FolderPath;

            if (folder != null && folder != string.Empty && !Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            File.WriteAllText(FullPath, text);
        }

        public override T Deserialize()
        {
            if (FullPath == null || FullPath == string.Empty || !File.Exists(FullPath))
            {
                return null;
            }

            string[] lines = File.ReadAllLines(FullPath);
            BlueData data = new BlueData();
            data.Type = string.Empty;
            data.Input = string.Empty;
            data.Sequence = string.Empty;

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == null)
                {
                    continue;
                }

                if (lines[i].StartsWith("Type:"))
                {
                    data.Type = lines[i].Substring(5);
                }
                else if (lines[i].StartsWith("Input:"))
                {
                    data.Input = lines[i].Substring(6);
                }
                else if (lines[i].StartsWith("Sequence:"))
                {
                    data.Sequence = lines[i].Substring(9);
                }
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
