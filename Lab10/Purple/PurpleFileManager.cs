namespace Lab10.Purple;

public abstract class PurpleFileManager<T>: MyFileManager,ISerializer<T> 
    where T : Lab9.Purple.Purple
{
    public PurpleFileManager(string name) : base(name)
    {
    }

    public PurpleFileManager(string name, string folderPath, string fileName, string? fileExtension = null) : base(name, folderPath, fileName, fileExtension)
    {
    }

    public override void EditFile(string content)
    {
        base.EditFile(content);
    }

    public override void ChangeFileExtension(string fileExtension)
    {
        base.ChangeFileExtension(fileExtension);
    }

    public abstract T Deserialize();

    public abstract void Serialize(T item);
}