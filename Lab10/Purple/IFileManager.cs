namespace Lab10;

public interface IFileManager
{
    public string FolderPath { get; }
    public string FileName { get; }
    public string FileExtension { get; }
    public string FullPath { get; }


    void SelectFolder(string args);
    void ChangeFileName(string args);
    void ChangeFileFormat(string args);
}