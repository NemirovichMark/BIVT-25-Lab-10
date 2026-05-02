namespace Lab10.Purple;

public abstract class PurpleFileManager<T>:MyFileManager,ISerializer<T> where T:Lab9.Purple.Purple
{
    public PurpleFileManager(string name):base(name){}
    public PurpleFileManager(string name, string folderpath, string filename, string fileextension = "txt"):base(name,folderpath,filename,fileextension){}

    public virtual void EditFile(string text)
    {
        if (!File.Exists(FullPath)) return;
        base.EditFile(text);
    }
    public virtual void ChangeFileExtension(string extension)
    {
        if (!File.Exists(FullPath)) return;
        base.ChangeFileExtension(extension);
    }

    public abstract void Serialize(T obj);
    public abstract T Deserialize();
    
}
