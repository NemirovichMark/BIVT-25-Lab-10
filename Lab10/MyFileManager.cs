namespace Lab10;

public abstract class MyFileManager : IFileManager, IFileLifeController
{
    public string Name { get; }
    public string FolderPath { get; private set;  }
    public string FileName { get; private set; }
    public string FileExtension { get; private set; }

    public string FullPath
    {
        get
        {
            if (string.IsNullOrEmpty(FileName))
                return FolderPath ?? Directory.GetCurrentDirectory();
            else if (string.IsNullOrEmpty(FileExtension))
                return Path.Combine(FolderPath ?? Directory.GetCurrentDirectory(), FileName);
            else return Path.Combine(FolderPath ?? Directory.GetCurrentDirectory(), $"{FileName}.{FileExtension}");
        }
    }

    public void SelectFolder(string path)
    {
        FolderPath = path;
    }

    public void ChangeFileName(string fileName)
    {
        FileName = fileName;
    }

    public virtual void ChangeFileFormat(string format)
    {
        if (string.IsNullOrEmpty(format)) return;
        FileExtension = format;
        if (!File.Exists(FullPath)) CreateFile();

    }

    public void CreateFile()
    {
        Directory.CreateDirectory(FolderPath);

        if (!File.Exists(FullPath))
        {
            using (File.Create(FullPath))
            {
            }
        }
    }

    public void DeleteFile()
    {
        if (File.Exists(FullPath))
        {
            File.Delete(FullPath);
        }
    }

    public virtual void EditFile(string file)
    {
        File.WriteAllText(FullPath, file);
    }

    public virtual void ChangeFileExtension(string extension)
    {
        string newFilePath = Path.ChangeExtension(FullPath, extension);
        if (File.Exists(newFilePath)) return;
        File.Move(FullPath, newFilePath);
        FileExtension = extension;
    }

    public MyFileManager(string name)
    {
        Name = name;
    }
    
    public MyFileManager(string name, string folderName, string fileName, string fileExtension = "txt") : this(name)
    {
        FolderPath = folderName;
        FileName = fileName;
        FileExtension = fileExtension;
    }
}