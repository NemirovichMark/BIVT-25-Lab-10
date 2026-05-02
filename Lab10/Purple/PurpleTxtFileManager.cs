namespace Lab10.Purple;

public class PurpleTxtFileManager<T> : PurpleFileManager<T>
    where T : Lab9.Purple.Purple
{
    public PurpleTxtFileManager(string name) : base(name)
    {
    }
    public PurpleTxtFileManager(
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
        base.ChangeFileExtension("txt");

    public override void Serialize(T obj)
    {
    }

    public override T Deserialize()
    {
        return null!;
    }
}
