namespace Lab10;

public abstract class MyFileManager : IFileManager, IFileLifeController
{
    public string Name { get; private set;}
    public string FolderPath { get; private set; }
    public string FileName { get; private set; }
    public string FileExtension { get; private set; }
    public string FullPath
    {
        get
        {
            string folder = FolderPath ?? "";
            string file = FileName ?? "";
            string ext = FileExtension ?? "";
            if (string.IsNullOrEmpty(ext))
                return Path.Combine(folder, file);
            return Path.Combine(folder, file + "." + ext);
        }
    }

    public MyFileManager(string name)
    {
        Name = name;
        FolderPath = "";
        FileName = "";
        FileExtension = "";
    }
    public MyFileManager(string name, string folderPath, string fileName, string fileExtension = "")
    {
        Name = name ?? "";
        FolderPath = folderPath ?? "";
        FileName = fileName ?? "";
        FileExtension = fileExtension ?? "";
    }

    public void SelectFolder(string folder)
    {
        FolderPath = folder ?? "";
    }

    public void ChangeFileName(string fileName)
    {
        FileName = fileName ?? "";
    }


    public virtual void ChangeFileExtension(string extension)
    {
        string oldPath = FullPath;
        if (string.IsNullOrEmpty(oldPath) || !File.Exists(oldPath)) return;
        string content = File.ReadAllText(oldPath);
        File.Delete(oldPath);
        ChangeFileFormat(extension);
        string newPath = FullPath;
        File.WriteAllText(newPath, content);
    }

    public void ChangeFileFormat(string fileFormat)
    {
        FileExtension = fileFormat ?? "";
        string path = FullPath;
        if (string.IsNullOrEmpty(path)) return;
        string? dir = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            Directory.CreateDirectory(dir);
        if (!File.Exists(path))
            File.Create(path).Close();
    }
    
    public void CreateFile()
    {
        string path = FullPath;
        if (string.IsNullOrEmpty(path)) return;
        string? dir  = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            Directory.CreateDirectory(dir);
        if (!File.Exists(path))
            File.Create(path).Close();
    }

    public void DeleteFile()
    {
        if (!string.IsNullOrEmpty(FullPath) && File.Exists(FullPath))
            File.Delete(FullPath);
    }

    public virtual void EditFile(string content)
    {
        if (string.IsNullOrEmpty(FullPath) || !File.Exists(FullPath)) return;
        File.WriteAllText(FullPath, content);
    }
}