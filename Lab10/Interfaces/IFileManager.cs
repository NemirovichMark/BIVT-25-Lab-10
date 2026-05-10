namespace Lab10.Interfaces
{
    public interface IFileManager
    {
        string FolderPath { get; }
        string FileName { get; }
        string FileExtension { get; }
        string FullPath { get; }

        void SelectFolder(string folder_path);
        void ChangeFileName(string file_name);
        void ChangeFileFormat(string file_extension);
    }
}