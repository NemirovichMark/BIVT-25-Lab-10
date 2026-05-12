namespace Lab10;

public abstract class MyFileManager : IFileManager, IFileLifeController
{
    private string _name;
    private string _folderPath;
    private string _fileName;
    private string _fileExtension;
    
    public string Name => _name;
    public string FolderPath => _folderPath;
    public string FileName => _fileName;
    public string FileExtension => _fileExtension;
    public string FullPath => _folderPath + "/" + _fileName + "." + _fileExtension;

    public MyFileManager(string name)
    {
        _name = name;
        _folderPath = "";
        _fileName = "";
        _fileExtension = "";
    }

    public MyFileManager(string name, string folderPath, string fileName, string fileExtension = "")
    {
        _name = name;
        _folderPath = folderPath;
        _fileName = fileName;
        _fileExtension = fileExtension;
    }
    
    public void SelectFolder(string folderPath)
    {
        _folderPath = folderPath;
    }

    public void ChangeFileName(string fileName)
    {
        _fileName = fileName;
    }

    public void ChangeFileFormat(string fileExtension)
    {
        _fileExtension = fileExtension;

        if (!string.IsNullOrEmpty(_folderPath))
        {
            Directory.CreateDirectory(_folderPath);

            if (!File.Exists(FullPath))
            {
                File.Create(FullPath).Close();
            }
        }
    }

    public void CreateFile()
    {
        Directory.CreateDirectory(_folderPath);
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

    public virtual void EditFile(string text)
    {
        if (File.Exists(FullPath))
        {
            File.WriteAllText(FullPath, text);
        }
    }

    public virtual void ChangeFileExtension(string newExtension)
    {
        if (File.Exists(FullPath))
        {
            string text = File.ReadAllText(FullPath);
            File.Delete(FullPath);
            _fileExtension = newExtension;
            File.Create(FullPath).Close();
            File.WriteAllText(FullPath, text);
        }
    }
}