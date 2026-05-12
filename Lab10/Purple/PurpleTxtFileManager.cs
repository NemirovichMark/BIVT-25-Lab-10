using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Purple
{
    public class PurpleTxtFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
    {
        public PurpleTxtFileManager(string name) : base(name)
        {
        }

        public PurpleTxtFileManager(string name, string folder, string fileName, string extension = "txt") : base(name, folder, fileName, extension)
        {
        }
        public override void EditFile(string fileContent)
        {
            T purple = Deserialize();
            purple.ChangeText(fileContent);
            Serialize(purple);
        }
        public override void ChangeFileExtension(string fileExtention)
        {
            ChangeFileFormat("txt");
        }
        public override void Serialize(T purple)
        {
            using (TextWriter writer = new StreamWriter(FullPath))
            {
                writer.WriteLine($"Type:{purple.GetType().AssemblyQualifiedName}");
                writer.WriteLine($"Input:{purple.Input}");
                if (purple is Lab9.Purple.Task4 task4)
                {
                    writer.WriteLine("Codes:");
                    foreach (var code in task4.Codes)
                    {
                        writer.WriteLine($"{code.Item1} {code.Item2}");
                    }
                }
            }
        }

        public override T Deserialize()
        {
            if (!File.Exists(FullPath)) return null;
            string objectType = null;
            string input = null;
            var codes = new (string, char)[5];

            using (TextReader reader = new StreamReader(FullPath))
            {
                string currentLine = reader.ReadLine();
                while (currentLine!= null)
                {
                    if (currentLine.Split(':',2)[0] == "Type")
                        objectType = currentLine.Split(':',2)[1];
                    if (currentLine.Split(':',2)[0] == "Input")
                        input = currentLine.Split(':', 2)[1];
                    if (currentLine == "Codes:")
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            currentLine = reader.ReadLine();
                            codes[i] = (currentLine.Split()[0], currentLine.Split()[1][0]);
                        }
                    }
                    currentLine = reader.ReadLine();
                }
            }
            Type object_type = Type.GetType(objectType);

            Lab9.Purple.Purple purple;
            if (object_type == typeof(Lab9.Purple.Task1))
            {
                purple = new Lab9.Purple.Task1(input);
            }
            else if (object_type == typeof(Lab9.Purple.Task2))
            {
                purple = new Lab9.Purple.Task2(input);
            }
            else if (object_type == typeof(Lab9.Purple.Task3))
            {
                purple = new Lab9.Purple.Task3(input);
            }
            else purple = new Lab9.Purple.Task4(input, codes.ToArray());

            purple.Review();
            return purple as T;
        }
    }
}
