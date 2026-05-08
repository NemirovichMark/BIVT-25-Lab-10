namespace Lab10;

public interface IFileManager
{
  string FolderPath {get; }
  string FileName {get; }
  string FileExtension {get; }
  string FullPath {get; }

  public void SelectFolder(string name);
  public void ChangeFileName(string name);
  public void ChangeFileFormat(string format);
}
