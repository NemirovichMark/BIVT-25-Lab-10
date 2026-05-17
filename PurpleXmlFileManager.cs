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
    public override void EditFile(string changeFile)
    {
        T obj = Deserialize();
        if (obj == null) return;
        obj.ChangeText(changeFile);
        Serialize(obj);
    }

    public override void ChangeFileExtension(string extension)
    {
        T obj = Deserialize();
        if (obj == null) return;
        ChangeFileFormat("xml");
        Serialize(obj);
    }
    public override void Serialize(T obj)
    {
        if (obj == null || FullPath == null) return;

        var serializer = new XmlSerializer(typeof(DTOPurple));
        using StreamWriter writer = new StreamWriter(FullPath);
        serializer.Serialize(writer, new DTOPurple(obj));
    }
    public override T Deserialize()
    {
        if (FullPath == null || !File.Exists(FullPath)) return null;

        var serializer = new XmlSerializer(typeof(DTOPurple));
        DTOPurple dto;
        try
        {
            using (StreamReader reader = new StreamReader(FullPath))
            {
                dto = (DTOPurple)serializer.Deserialize(reader);
            }
        }
        catch { return null; }

        (string, char)[] codes = null;
        if (dto.Codes != null)
        {
            codes = new (string, char)[dto.Codes.Length];
            for (int i = 0; i < dto.Codes.Length; i++)
            {
                codes[i] = (dto.Codes[i].Substring(0, dto.Codes[i].Length - 2), dto.Codes[i][dto.Codes[i].Length - 1]);

            }
        }
        Lab9.Purple.Purple obj = null;
        if (dto.TypeName == "Task1")
        {
            obj = new Task1(dto.Input);
        }
        else if (dto.TypeName == "Task2")
        {
            obj = new Task2(dto.Input);
        }
        else if (dto.TypeName == "Task3")
        {
            obj = new Task3(dto.Input);
        }
        else if (dto.TypeName == "Task4")
        {

            obj = new Task4(dto.Input, codes ?? Array.Empty<(string, char)>());
        }

        if (obj == null) return null;
        obj.Review();
        Console.WriteLine(dto.TypeName);
        Console.WriteLine(obj.ToString());
        Console.WriteLine();
        return (T)obj;
    }


    public class DTOPurple
    {
        public string TypeName { get; set; }
        public string Input { get; set; }
        public string[] Codes { get; set; }

        public DTOPurple() { }

        public DTOPurple(Lab9.Purple.Purple obj)
        {
            TypeName = obj.GetType().Name;
            Input = obj.Input;

            (string, char)[] codes;
            if (obj is Task4 task4)
            {
                codes = task4.Codes;
                Codes = new string[codes.Length];
                for (int i = 0; i < codes.Length; i++)
                    Codes[i] = codes[i].Item1 + " " + codes[i].Item2;
            }
            else
            {
                codes = null;
            }
        }
    }
}