namespace Lab10;

public abstract class MyFileManager : IFileManager, IFileLifeController
{
    private readonly string _name;
    private string _folderPath;
    private string _fileName;
    private string _fileExtension;

    public MyFileManager(string name) : this(name, string.Empty, string.Empty, string.Empty)
    {
    }

    public MyFileManager(string name, string folder, string fileName, string fileExtension = "")
    {
        _name = name ?? string.Empty;
        _folderPath = folder ?? string.Empty;
        _fileName = fileName ?? string.Empty;
        _fileExtension = NormalizeExtension(fileExtension);
    }

    public string Name => _name;

    public string FolderPath => _folderPath;

    public string FileName => _fileName;

    public string FileExtension => _fileExtension;

    public string FullPath
    {
        get
        {
            return TryGetFullPath(out string path) ? path : string.Empty;
        }
    }

    public void SelectFolder(string folderPath)
    {
        _folderPath = folderPath ?? string.Empty;
    }

    public void ChangeFileName(string fileName)
    {
        _fileName = fileName ?? string.Empty;
    }

    public virtual void ChangeFileFormat(string fileExtension)
    {
        _fileExtension = NormalizeExtension(fileExtension);

        if (TryGetFullPath(out _))
        {
            CreateFile();
        }
    }

    public virtual void CreateFile()
    {
        if (!TryGetFullPath(out string path))
        {
            return;
        }

        string? directory = Path.GetDirectoryName(path);

        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        if (File.Exists(path))
        {
            return;
        }

        using FileStream _ = File.Create(path);
    }

    public virtual void EditFile(string text)
    {
        if (!TryGetFullPath(out string path) || !File.Exists(path))
        {
            return;
        }

        File.WriteAllText(path, text ?? string.Empty);
    }

    public virtual void ChangeFileExtension(string fileExtension)
    {
        if (!TryGetFullPath(out string oldPath))
        {
            return;
        }

        string content = File.Exists(oldPath) ? File.ReadAllText(oldPath) : string.Empty;

        _fileExtension = NormalizeExtension(fileExtension);

        if (!TryGetFullPath(out string newPath))
        {
            return;
        }

        string? directory = Path.GetDirectoryName(newPath);

        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        if (File.Exists(oldPath))
        {
            File.WriteAllText(newPath, content);

            if (!string.Equals(oldPath, newPath, StringComparison.OrdinalIgnoreCase))
            {
                File.Delete(oldPath);
            }

            return;
        }

        CreateFile();
    }

    public virtual void DeleteFile()
    {
        if (!TryGetFullPath(out string path) || !File.Exists(path))
        {
            return;
        }

        File.Delete(path);
    }

    private bool TryGetFullPath(out string path)
    {
        path = string.Empty;

        if (string.IsNullOrWhiteSpace(_fileName))
        {
            return false;
        }

        string fileWithExtension = string.IsNullOrWhiteSpace(_fileExtension)
            ? _fileName
            : $"{_fileName}.{_fileExtension}";

        path = string.IsNullOrWhiteSpace(_folderPath)
            ? fileWithExtension
            : Path.Combine(_folderPath, fileWithExtension);

        return !string.IsNullOrWhiteSpace(path);
    }

    private static string NormalizeExtension(string? fileExtension)
    {
        return string.IsNullOrWhiteSpace(fileExtension)
            ? string.Empty
            : fileExtension.Trim().TrimStart('.');
    }
}
