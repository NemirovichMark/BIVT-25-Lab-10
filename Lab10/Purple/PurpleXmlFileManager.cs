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
        base.ChangeFileExtension("xml");
    

    public override void Serialize(T obj)
    {
    }

    public override T Deserialize()
    {
        return null!;
    }
}
