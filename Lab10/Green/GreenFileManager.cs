namespace Lab10.Green;

public abstract class GreenFileManager : MyFileManager, ISerializer
{

    public override void EditFile(string newFileText)
    {
        File.Exists(FullPath);
        base.EditFile(newFileText);
    }

    public override void ChangeFileExtension(string newFileExtension)
    {
        File.Exists(FullPath);
        base.ChangeFileExtension(newFileExtension);
    }
    
    abstract public void Serialize<T>(T obj) where T : Lab9.Green.Green;
    abstract public T Deserialize<T>() where T : Lab9.Green.Green;

    public GreenFileManager(string name, string folderPath, string fileName, string fileExtension = "txt") : base(name,
        folderPath, fileName, fileExtension){}

    public GreenFileManager(string name) :  base(name)
    {
    }
    
}