namespace Lab10;

public abstract class MyFileManager : IFileManager, IFileLifeController
{
  public string Name {get; private set;}
  public string FolderPath { get; private set;}
  public string FileName { get; private set;}
  public string FileExtension { get; private set;}
  public string FullPath {
    get
    {
      return Path.Combine(FolderPath, FileName) + "." + FileExtension;
    }
  }

  public MyFileManager(string name)
  {
    Name = name;
    FolderPath = "";
    FileName = "";
    FileExtension = "";
  }

  public MyFileManager(string name, string folderpath, string filename, string ext = "")
  {
    Name = name;
    FolderPath = folderpath;
    FileName = filename;
    FileExtension = ext;
  }

  public void SelectFolder(string path)
  {
    if (string.IsNullOrEmpty(path) || string.IsNullOrWhiteSpace(path)) return;
    if (!Directory.Exists(path)) return; 
    FolderPath = path;
    //path = Directory.GetCurrentDirectory();
    //path = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
    //path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
  }

  public void ChangeFileName(string name)
  {
    if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name)) return;
    FileName = name;}

  public void ChangeFileFormat(string format)
  {
    DeleteFile();
    if (string.IsNullOrEmpty(format) || string.IsNullOrWhiteSpace(format)) return;
    FileExtension = format;
    if (!File.Exists(FullPath))
    {
      CreateFile();
    }
  }

  public void CreateFile()
  {
    if (!Directory.Exists(FolderPath))
    {
      Directory.CreateDirectory(FolderPath);
    }

    if (!File.Exists(FullPath))
    {
      File.Create(FullPath).Close();
    }
  }

  public void DeleteFile()
  {
    if (File.Exists(FullPath))
    {
      File.Delete(FullPath);
    }
  }

  public void EditFile(string data)
  {
    if (data == null) return;
    File.WriteAllText(FullPath, data);
  }

  public void ChangeFileExtension(string ext)
  {
    if (string.IsNullOrWhiteSpace(ext)) return;
    if (!File.Exists(FullPath)) return;
    string data = File.ReadAllText(FullPath);
    DeleteFile();
    ChangeFileFormat(ext);
    EditFile(data);
  }
}





