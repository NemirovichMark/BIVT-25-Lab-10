namespace Lab10;

public abstract class MyFileManager : IFileManager, IFileLifeController
{
    private string _name;
    private string _nameFolder;
    private string _nameFile;
    private string _extension;

    public string Name => _name;
    public string FolderPath => _nameFolder;
    public string FileName => _nameFile;
    public string FileExtension => _extension;

    public string FullPath => Path.Combine(_nameFolder, _nameFile + _extension);

    public MyFileManager(string name)
    {
        _name = name;
    }

    public MyFileManager(string name, string nameFolder, string nameFile, string extension = "")
    {
        _name = name;
        _nameFolder = nameFolder;
        _nameFile = nameFile;
        if (string.IsNullOrEmpty(extension))
            _extension = string.Empty;
        else
            _extension = extension.StartsWith(".") ? extension : "." + extension;
    }
    
    public void SelectFolder(string nameFolder)
    {
        _nameFolder = nameFolder;
    }

    public void ChangeFileName(string nameFile)
    {
        _nameFile = nameFile;
    }

    public void ChangeFileFormat(string extension)
    {
        _extension = string.IsNullOrEmpty(extension) 
            ? string.Empty 
            : (extension.StartsWith(".") ? extension : "." + extension);
    }

    public void CreateFile()
    {
        if (!string.IsNullOrEmpty(_nameFolder) && !Directory.Exists(_nameFolder))
            Directory.CreateDirectory(_nameFolder);
        
        if (!File.Exists(FullPath))
            File.Create(FullPath).Close();
    }

    public void DeleteFile()
    {
        if (File.Exists((FullPath)))
            File.Delete(FullPath);
    }

    public virtual void EditFile(string text)
    {
        if (File.Exists(FullPath))
            File.WriteAllText(FullPath, text);
    }

    public virtual void ChangeFileExtension(string text)
    {
        string oldPath = FullPath;
        ChangeFileFormat(text);
        string newPath = FullPath;
        
        if (Path.Exists(oldPath) && oldPath != newPath)
            File.Move(oldPath, newPath);
    }
}