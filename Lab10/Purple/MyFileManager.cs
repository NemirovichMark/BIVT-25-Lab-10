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
        _extension = extension?.TrimStart('.') ?? string.Empty;

        if (!string.IsNullOrEmpty(_nameFolder) && !string.IsNullOrEmpty(_nameFile))
        {
            CreateFile();
        }
    }

    public void ChangeFormat(string format)
    {
        throw new NotImplementedException();
    }

    public void SelectFile(string fileName)
    {
        throw new NotImplementedException();
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

    public virtual void ChangeFileExtension(string newExtension)
    {
        string oldPath = FullPath;
    
        _extension = newExtension?.TrimStart('.') ?? string.Empty;
        string newPath = FullPath;

        if (File.Exists(oldPath) && oldPath != newPath)
        {
            if (File.Exists(newPath))
            {
                File.Delete(newPath);
            }
            File.Move(oldPath, newPath);
        }
    }
}
