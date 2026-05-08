namespace Lab10.Blue;

public abstract class BlueFileManager<T> : MyFileManager, ISerializer<T> where T:Lab9.Blue.Blue
{
  public BlueFileManager(string name) : base(name){}
  public BlueFileManager(string name, string folderpath, string filename, string ext = "") : base(name, folderpath, filename, ext){}

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
