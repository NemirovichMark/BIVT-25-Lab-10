using System.IO;

namespace Lab10.Blue
{
    public class BlueTxtFileManager<T> : BlueFileManager<T>
        where T : Lab9.Blue.Blue
    {
        public BlueTxtFileManager(string name) : base(name)
        {
            ChangeFileFormat("txt");
        }

        public BlueTxtFileManager(string name, string folder, string file, string ext = "txt")
            : base(name, folder, file, ext)
        {
            ChangeFileFormat("txt");
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
            ChangeFileFormat("txt");
        }

        public override void Serialize(T obj)
        {
            if (obj == null)
            {
                return;
            }

            string sequence = string.Empty;

            if (obj is Lab9.Blue.Task2 task2)
            {
                sequence = task2.Sequence;
            }

            string text =
                "Type:" + obj.GetType().Name + "\n" +
                "Input:" + obj.Input + "\n" +
                "Sequence:" + sequence;

            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }

            File.WriteAllText(FullPath, text);
        }

        public override T Deserialize()
        {
            if (!File.Exists(FullPath))
            {
                return null;
            }

            string[] lines = File.ReadAllLines(FullPath);

            string type = string.Empty;
            string input = string.Empty;
            string sequence = string.Empty;

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
                else if (lines[i].StartsWith("Sequence:"))
                {
                    sequence = lines[i].Substring(9);
                }
            }

            Lab9.Blue.Blue task = null;

            if (type == "Task1")
            {
                task = new Lab9.Blue.Task1(input);
            }
            else if (type == "Task2")
            {
                task = new Lab9.Blue.Task2(input, sequence);
            }
            else if (type == "Task3")
            {
                task = new Lab9.Blue.Task3(input);
            }
            else if (type == "Task4")
            {
                task = new Lab9.Blue.Task4(input);
            }

            if (task != null)
            {
                task.Review();
            }

            return (T)task;
        }
    }
}
