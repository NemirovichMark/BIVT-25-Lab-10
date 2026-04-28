namespace Lab10;

public abstract class MyFileManager : IFileManager, IFileLifeController
{
    public MyFileManager (string name) => Name = name;
    public MyFileManager    (string name,
                            string fileName, 
                            string folderPath, 
                            string fileExtension = "")
    {
        Name = name;
        _fileName = fileName;
        _folderPath = folderPath;
        _fileExtension = fileExtension;
    }

    private string _folderPath;
    private string _fileName;
    private string _fileExtension;
    private string _fileFormat;

    public string Name {get; protected set;}
    public string FolderPath => _folderPath;
    public string FileName => _fileName;
    public string FileExtension => _fileExtension;
    public string FullPath => 
        Path.Combine(_folderPath, $"{_fileName}.{_fileExtension}");

    public void SelectFolder(string path_to_folder) =>
        _folderPath = path_to_folder;
    public void ChangeFileName(string new_file_name) =>
        _fileName = new_file_name;
    public void ChangeFileFormat(string new_file_format) =>
        _fileFormat = new_file_format;

    public void CreateFile()
    {
        Directory.CreateDirectory(_folderPath);
        using FileStream fs = File.Create(FullPath);
    }
    public void DeleteFile()
    {
        if (File.Exists(FullPath))
            File.Delete(FullPath);
    }
    public void EditFile (string change_file)
    {
        if (File.Exists(FullPath))
            File.WriteAllText(FullPath,change_file);
    }
         
    public void ChangeFileExtension(string new_extension)
    {
        File.Move($"{_fileName}.{_fileExtension}", $"{_fileName}.{new_extension}");
        _fileExtension = new_extension;
    }
}