using System.Xml.Serialization;
using Lab9.Purple;

namespace Lab10.Purple;

public class PurpleXmlFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
{
    public PurpleXmlFileManager(string name) : base(name)
    {
    }

    public PurpleXmlFileManager(string name, string folderName, string fileName, string fileExtension = "txt") : base(name, folderName, fileName, fileExtension)
    {
    }

    public override T Deserialize()
    {
        if (FullPath == null || !File.Exists(FullPath)) return null;

        var serializer = new XmlSerializer(typeof(DTOPurple));
        DTOPurple dto;

        using (StreamReader reader = new StreamReader(FullPath))
        {
            dto = (DTOPurple)serializer.Deserialize(reader);
        }

        (string, char)[] codes = null;
        if (dto.CodePairs != null)
        {
            codes = new (string, char)[dto.CodePairs.Length];
            for (int i = 0; i < dto.CodePairs.Length; i++)
            {
                codes[i] = (dto.CodePairs[i].Substring(0, dto.CodePairs[i].Length - 2), dto.CodePairs[i][dto.CodePairs[i].Length - 1]);
                Console.WriteLine(codes[i]);
            }
        }
        Lab9.Purple.Purple result;
        if (dto.TypeName == "Task1")
        {
            result = new Task1(dto.Input);
        }
        else if (dto.TypeName == "Task2")
        {
            result = new Task2(dto.Input);
        }
        else if (dto.TypeName == "Task3")
        {
            result = new Task3(dto.Input);
        }
        else if (dto.TypeName == "Task4")
        {
            result = new Task4(dto.Input, codes);
        }
        else
        {
            result = null;
        }

        if (result == null) return null;
        result.Review();
        return (T)result;
    }

    public override void Serialize(T obj)
    {
        if (obj == null || FullPath == null) return;

        var serializer = new XmlSerializer(typeof(DTOPurple));
        using StreamWriter writer = new StreamWriter(FullPath);
        serializer.Serialize(writer, new DTOPurple(obj));
    }

    public override void EditFile(string input)
    {
        T content = Deserialize();
        content.ChangeText(input);
        Serialize(content);
    }

    public override void ChangeFileExtension(string extension)
    {
        T content = Deserialize();
        if (extension == "txt") ChangeFileFormat("txt");
        Serialize(content);
    }

    public class DTOPurple
    {
        public string TypeName { get; set; }
        public string Input { get; set; }
        public string[] CodePairs { get; set; }

        public DTOPurple() { }

        public DTOPurple(Lab9.Purple.Purple obj)
        {
            TypeName = obj.GetType().Name;
            Input = obj.Input;

            (string, char)[] codes;
            if (obj is Task3 task3)
            {
                codes = task3.Codes;
            }
            else
            {
                codes = null;
            }
            if (obj is Task4 task4)
            {
                codes = task4.Codes;
            }
            else
            {
                codes = null;
            }

            if (codes != null)
            {
                CodePairs = new string[codes.Length];
                for (int i = 0; i < codes.Length; i++)
                    CodePairs[i] = codes[i].Item1 + " " + codes[i].Item2;
            }
        }
    }
}