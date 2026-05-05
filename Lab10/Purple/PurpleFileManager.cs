namespace Lab10.Purple;

public abstract class PurpleFileManager<T> : MyFileManager, ISerializer<T>
    where T : Lab9.Purple.Purple
{
    public PurpleFileManager(string name) : base(name)
    {
    }

    public PurpleFileManager(
        string name, 
        string folderPath, 
        string fileName, 
        string fileExtension = "")
        : base(name, folderPath, fileName, fileExtension)
    {
    }

    public abstract T Deserialize();
    public abstract void Serialize(T obj);

    public override void EditFile(string change_file) =>
        base.EditFile(change_file);
    
    public override void ChangeFileExtension(string new_extension) =>
        base.ChangeFileExtension(new_extension);
}
