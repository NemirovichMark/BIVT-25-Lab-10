using System.Xml.Serialization;

namespace Lab10.Purple;

public class PurpleXmlFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
{
    public PurpleXmlFileManager(string name) : base(name) { }
    public PurpleXmlFileManager(string name, string folder, string fileName, string extension)
        : base(name, folder, fileName, extension) { }

    public override void EditFile(string content)
    {
        if (string.IsNullOrEmpty(FullPath) || !File.Exists(FullPath)) return;
        T obj = Deserialize();
        if (obj == null) return;
        obj.ChangeText(content);
        Serialize(obj);
    }

    public override void ChangeFileExtension(string extension)
    {
        ChangeFileFormat("xml");
    }

    public override void Serialize(T obj)
    {
        if (obj == null || FullPath == null) return;

        var dto = new DTOPurple(obj);
        var serializer = new XmlSerializer(typeof(DTOPurple));
        using (var sw = new StreamWriter(FullPath))
            serializer.Serialize(sw, dto);
    }

    public override T Deserialize()
    {
        var serializer = new XmlSerializer(typeof(DTOPurple));
        using (var sr = new StreamReader(FullPath))
        {
            var dto = (DTOPurple)serializer.Deserialize(sr);
            var obj = dto.Type switch
            {
                nameof(Lab9.Purple.Task1) => (T)(object)new Lab9.Purple.Task1(dto.Input),
                nameof(Lab9.Purple.Task2) => (T)(object)new Lab9.Purple.Task2(dto.Input),
                nameof(Lab9.Purple.Task3) => (T)(object)new Lab9.Purple.Task3(dto.Input),
                nameof(Lab9.Purple.Task4) => (T)(object)new Lab9.Purple.Task4(dto.Input, dto.Codes ?? Array.Empty<(string, char)>()),
                _ => null
            };
            obj?.Review();
            return obj;
        }
    }
}

public class DTOPurple
{
    public string Type { get; set; }
    public string Input { get; set; }
    public (string, char)[] Codes { get; set; }

    public DTOPurple() { }

    public DTOPurple(Lab9.Purple.Purple purple)
    {
        Type = purple.GetType().Name;
        Input = purple.Input;
        if (purple is Lab9.Purple.Task4 t4 && t4.Codes != null)
        {
            Codes = new (string, char)[t4.Codes.Length];
            for (int i = 0; i < Codes.Length; i++)
                Codes[i] = (t4.Codes[i].Item1, t4.Codes[i].Item2);
        }
    }
}