namespace Lab10.Purple
{
    public class PurpleTxtFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
    {
        public PurpleTxtFileManager(string name) : base(name) { }

        public PurpleTxtFileManager(string name, string folderPath, string fileName, string fileExtension = "txt")
            : base(name, folderPath, fileName, fileExtension) { }

        public override void EditFile(string text)
        {
            T task = Deserialize();

            if (task == null)
                return;

            task.ChangeText(text );
            Serialize(task);
        }

        public override void ChangeFileExtension(string fileExtension)
        {
            ChangeFileFormat("txt");
        }

        public override void Serialize(T obj)
        {
            ChangeFileFormat("txt");
            obj.Review();

            using (StreamWriter writer = new StreamWriter(FullPath)) {
                writer.WriteLine($"Type: {obj.GetType().AssemblyQualifiedName}");
                writer.WriteLine($"Input: {obj.Input}");

                (string, char)[] codes = ReadCodes(obj);

                if (codes.Length == 0)
                    return;

                writer.WriteLine("Codes: ");

                foreach ((string pair, char code) in codes)
                    writer.WriteLine($"{pair}|{code}"); 
            }
        }

        public override T Deserialize()
        {
            string txtObjectType = null;
            string txtObjectInput = null;
            bool needCodes = false;
            List<(string, char)> codes = new List<(string, char)>();

            using (StreamReader reader = new StreamReader(FullPath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine()!;

                    if (line.StartsWith("Type: "))
                    {
                        txtObjectType = line.Substring("Type: ".Length);
                        needCodes = false;
                    }
                    else if (line.StartsWith("Input: "))
                    {
                        txtObjectInput = line.Substring("Input: ".Length);
                        needCodes = false;
                    }
                    else if (line.StartsWith("Codes: "))
                    {
                        needCodes = true;
                    }
                    else if (needCodes)
                    {
                        string[] pair = line.Split("|");
                        codes.Add((pair[0], pair[1][0]));
                    }
                }
            }
            
            Type objectType = Type.GetType(txtObjectType);

            Lab9.Purple.Purple obj;

            if (objectType == typeof(Lab9.Purple.Task1))
                obj = new Lab9.Purple.Task1(txtObjectInput);
            else if (objectType == typeof(Lab9.Purple.Task2))
                obj = new Lab9.Purple.Task2(txtObjectInput);
            else if (objectType == typeof(Lab9.Purple.Task3))
                obj = new Lab9.Purple.Task3(txtObjectInput);
            else
                obj = new Lab9.Purple.Task4(txtObjectInput, codes.ToArray());

            obj.Review();
            return (T)obj;
        }

        private static (string, char)[] ReadCodes(Lab9.Purple.Purple obj)
        {
            if (obj is Lab9.Purple.Task3 task3 && task3.Codes != null)
                return task3.Codes;

            if (obj is Lab9.Purple.Task4 task4 && task4.Codes != null)
                return task4.Codes;

            return Array.Empty<(string, char)>();
        }
    }
}
