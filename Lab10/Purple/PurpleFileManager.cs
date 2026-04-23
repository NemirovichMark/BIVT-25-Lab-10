namespace Lab10.Purple;

public abstract class PurpleFileManager<T> : MyFileManager, ISerializer<T>
    where T : Lab9.Purple.Purple
{
    protected PurpleFileManager(string name) : base(name)
    {
    }

    protected PurpleFileManager(string name, string nameFolder, string nameFile, string extension = "") : base(name, nameFolder, nameFile, extension)
    {
    }

    public override void EditFile(string text)
    {
        if (string.IsNullOrEmpty(FullPath)) return;
        
        base.EditFile(text);
    }

    public override void ChangeFileExtension(string text)
    {
        if (string.IsNullOrEmpty(FullPath)) return;
        
        base.ChangeFileExtension(text);
    }

    public abstract T Deserialize();

    public abstract void Serialize(T obj);
}