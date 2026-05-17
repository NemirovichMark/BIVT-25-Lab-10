using System.Xml.Serialization;

namespace Lab10.Purple;

public class PurpleXmlFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
{
    public PurpleXmlFileManager(string name) : base(name, string.Empty, string.Empty, "xml")
    {
    }

    public PurpleXmlFileManager(string name, string folder, string fileName, string fileExtension = "xml")
        : base(name, folder, fileName, string.IsNullOrWhiteSpace(fileExtension) ? "xml" : fileExtension)
    {
    }

    public override void EditFile(string text)
    {
        T? obj = Deserialize();

        if (obj == null)
        {
            return;
        }

        obj.ChangeText(text ?? string.Empty);
        Serialize(obj);
    }

    public override void ChangeFileExtension(string fileExtension)
    {
        if (!string.Equals(fileExtension, "xml", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        T? obj = Deserialize();
        string oldPath = FullPath;

        ChangeFileFormat("xml");

        if (obj != null)
        {
            Serialize(obj);
        }

        if (!string.IsNullOrWhiteSpace(oldPath) &&
            File.Exists(oldPath) &&
            !string.Equals(oldPath, FullPath, StringComparison.OrdinalIgnoreCase))
        {
            File.Delete(oldPath);
        }
    }

    public override void Serialize(T obj)
    {
        if (obj == null || string.IsNullOrWhiteSpace(FullPath))
        {
            return;
        }

        DTOPurple dto = BuildDto(obj);
        XmlSerializer serializer = new(typeof(DTOPurple));

        CreateFile();

        using StreamWriter writer = new(FullPath, false);
        serializer.Serialize(writer, dto);
    }

    public override T? Deserialize()
    {
        if (string.IsNullOrWhiteSpace(FullPath) || !File.Exists(FullPath))
        {
            return null;
        }

        try
        {
            XmlSerializer serializer = new(typeof(DTOPurple));

            using StreamReader reader = new(FullPath);
            DTOPurple? dto = serializer.Deserialize(reader) as DTOPurple;

            return CreateTaskFromDto(dto);
        }
        catch
        {
            return null;
        }
    }
}
