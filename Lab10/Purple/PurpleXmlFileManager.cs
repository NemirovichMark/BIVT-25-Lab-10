using System.Xml.Serialization;
using Lab9;

namespace Lab10.Purple;

public class PurpleXmlFileManager<T> : PurpleFileManager<T>
    where T : Lab9.Purple.Purple
{
    public PurpleXmlFileManager(string name) : base(name)
    {
    }

    public PurpleXmlFileManager(
        string name,
        string folderPath,
        string fileName,
        string fileExtension = "")
        : base(name, folderPath, fileName, fileExtension)
    {
    }

    public override void EditFile(string change_file)
    {
        T obj = Deserialize();
        obj.ChangeText(change_file);
        Serialize(obj);
    }

    public override void ChangeFileExtension(string new_extension) =>
        base.ChangeFileFormat("xml");
    

    public override void Serialize(T obj)
    {
        if (obj == null) return;
        Directory.CreateDirectory(FolderPath);

        var dto_object = new DTOPurple(obj);
        var ser = new XmlSerializer(typeof(DTOPurple));

        using (StreamWriter sw = new StreamWriter(FullPath))
        {
            ser.Serialize(sw, dto_object);
        }
    }

    public override T Deserialize()
    {
        if (!File.Exists(FullPath)) return null!;
        Lab9.Purple.Purple purple_object;

        var ser = new XmlSerializer(typeof(DTOPurple));
        using (var sr = new StreamReader(FullPath))
        {
            DTOPurple? dto_object = ser.Deserialize(sr) as DTOPurple;
            if (dto_object == null) return null!;

            purple_object = dto_object.GetPurpleTask();
            if (purple_object == null) return null!;
        }
        return (T)purple_object;
    }
}
