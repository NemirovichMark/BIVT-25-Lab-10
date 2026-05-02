namespace Lab10;

public abstract class MyFileManager:IFileManager,IFileLifeController
{
    public string Name { get; private set; }
    public string FolderPath { get; private set; }
    public string FileName { get; private set; }
    public string FileExtension { get; private set; }

    public string FullPath => Path.Combine(FolderPath, FileName) + "." + FileExtension;

    public MyFileManager(string name)
    {
        Name = name;
        FolderPath = "";
        FileName = "";
        FileExtension = "";
    }

    public MyFileManager(string name, string folderpath, string filename, string fileextension = "txt")
    {
        Name = name;
        FolderPath = folderpath;
        FileName = filename;
        FileExtension = fileextension;
    }

    public void SelectFolder(string folder)
    {
        if (string.IsNullOrEmpty(folder) || string.IsNullOrWhiteSpace(folder)) return;
        if (!Directory.Exists(folder)) return; 
        FolderPath = folder;
    }

    public void ChangeFileName(string name)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name)) return;
        FileName = name;
    }

    public void ChangeFileFormat(string format)
    {
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

    public void EditFile(string text)
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text)) return;
        File.WriteAllText(FullPath, text);
    }

    public void ChangeFileExtension(string extension)
    {
        if (string.IsNullOrEmpty(extension) || string.IsNullOrWhiteSpace(extension)) return;
        if (!(extension == "txt" || extension == "json" || extension == "xml")) return;
        if (!File.Exists(FullPath)) return;
        string content = File.ReadAllText(FullPath);
        DeleteFile();
        ChangeFileFormat(extension);
        EditFile(content);
    }
}
