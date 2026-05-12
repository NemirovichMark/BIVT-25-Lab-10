namespace Lab10;

public abstract class MyFileManager: IFileManager, IFileLifeController
{
    private string _folderPath;
    private string _fileName;
    private string _name = string.Empty;
    private string _fileExtension;
    
    public string FolderPath => _folderPath;
    
    public string Name => _name;
    public string FileName => _fileName;
    public string FileExtension => _fileExtension;

    public string FullPath
    {
        get
        {
            if (string.IsNullOrEmpty(_folderPath))
                return null;
            
            if (string.IsNullOrEmpty(_fileName)) 
                return _folderPath;
            
            if (string.IsNullOrEmpty(_fileExtension)) 
                return Path.Combine(_folderPath, _fileName);
            
            return Path.Combine(_folderPath, $"{_fileName}.{_fileExtension}");//ставит нужный /
        }
    }
    
    public MyFileManager(string name)
    {
        _fileName = name;
    }
    
    public MyFileManager(string name, string folderPath, string fileName, string? fileExtension = null)
    {
        _folderPath = folderPath;
        _name = name;
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
        ChangeFileExtension(fileExtension);

        if (!File.Exists(FullPath))
        
            CreateFile();
    }
    
    public void CreateFile()
    {
        string fullPath = FullPath;
        if (string.IsNullOrEmpty(fullPath))
            return;
        
        string folderPath = Path.GetDirectoryName(fullPath)!;
        
        if  (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);
        
        string fileName = Path.GetFileName(fullPath);
        
        if (!File.Exists(fullPath))
            File.Create(fullPath).Dispose(); //освобождает
    }

    public void DeleteFile()
    {
        string fullPath = FullPath;
        if (string.IsNullOrEmpty(fullPath))
            return;
        
        if (File.Exists(fullPath))
            File.Delete(fullPath);
    }

    public virtual void EditFile(string content)
    {
        string fullPath = FullPath;
        if (string.IsNullOrEmpty(fullPath))
            return;
        
        if (!File.Exists(fullPath))
            return;
        
        File.WriteAllText(fullPath, content);
    }
    
    public virtual void ChangeFileExtension(string fileExtension)
    {
        string fullPath = FullPath;
        if (string.IsNullOrEmpty(fullPath))
            return;
        
        if (_fileExtension == fileExtension)
            return;
        
        _fileExtension = fileExtension;
        
        string newFullPath = FullPath;
        
        if (File.Exists(fullPath))
            File.Move(fullPath, newFullPath);
    }
}