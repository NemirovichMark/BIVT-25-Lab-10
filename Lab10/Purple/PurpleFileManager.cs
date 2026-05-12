namespace Lab10.Purple;

public abstract class PurpleFileManager<T> : MyFileManager, ISerializer<T>
    where T : Lab9.Purple.Purple
{
    public PurpleFileManager(string name) : base(name) {}
    public PurpleFileManager(string name, string folder, string fileName, string extension = "")
        : base(name, folder, fileName, extension) {}

    public override void EditFile(string content)
    {
        string path = FullPath;
        if (!string.IsNullOrEmpty(path) && File.Exists(path))
            File.WriteAllText(path, content);
    }

    public override void ChangeFileExtension(string extension)
    {
        string oldPath = FullPath;
        if (string.IsNullOrEmpty(oldPath) || !File.Exists(oldPath)) return;
        string content = File.ReadAllText(oldPath);
        File.Delete(oldPath);
        ChangeFileFormat(extension);
        string newPath = FullPath;
        File.WriteAllText(newPath, content);
    }

    public abstract T Deserialize();
    public abstract void Serialize(T obj);
}