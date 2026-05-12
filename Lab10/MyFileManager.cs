using System.IO;
namespace Lab10;

public abstract class MyFileManager : IFileLifeController, IFileManager
{
    private string _name;
    
    //менеджер
    private string _folderPath;
    private string _fileName;
    private string _fileExtension;

    public string Name => _name;
    
    //менеджер
    public string FolderPath {
        get { return _folderPath; }
        private set { _folderPath = value; }
    }

    public string FileName
    {
        get { return _fileName; }
        private set { _fileName =  value; }
    }

    public string FileExtension
    {
        get { return _fileExtension; }
        private set { _fileExtension =  value; }
    }
    public string FullPath
    {
        get
        {
            return Path.Combine(_folderPath, _fileName)  + '.' + FileExtension;
        }
    }

    //менеджер
    public void SelectFolder(string folderPath)
    {
        _folderPath = folderPath;
    }

    public void ChangeFileName(string newFileName)
    {
        _fileName = newFileName;
    }

    public void ChangeFileFormat(string newFileFormat)
    {
        _fileExtension = newFileFormat;
        if (!File.Exists(FullPath))
        {
            File.Create(FullPath).Close();
        }
    }

    //контроллер
    public void CreateFile()
    {
        Directory.CreateDirectory(FolderPath);
        File.Create(FullPath).Close();
    }

    public void DeleteFile()
    {
        File.Delete(FullPath);
    }

    public virtual void EditFile(string newFileText)
    {
        File.WriteAllText(FullPath, newFileText);
    }

    public virtual void ChangeFileExtension(string newFileExtension)
    {
        // string _path = Path.Combine(_folderPath, _fileName)  + '.' + newFileExtension;
        // File.Create(_path).Close();
        // File.WriteAllText(_path, File.ReadAllText(FullPath));
        // File.Delete(FullPath);
        // _fileExtension = newFileExtension;
        
        string oldpath =  FullPath;
        _fileExtension = newFileExtension;
        File.Move(oldpath, FullPath);
        
    }

    public MyFileManager(string name, string folderPath, string fileName, string fileExtension = "txt")
    {
        _name = name;
        _fileName = fileName;
        _fileExtension = fileExtension;
        
        _folderPath = folderPath;
    }

    public MyFileManager(string name)
    {
        _name = name;
        _fileName = "";
        _fileExtension = "";
        _folderPath = "";
    }
}