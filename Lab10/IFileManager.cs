using Lab10;

namespace Lab10
{
    public interface IFileManager : IFileLifeController
    {
        string FileName { get; }
        string FolderPath { get; }
        string FullPath { get; }
        string FileExtension { get; }
        void SelectFolder(string path);
        void ChangeFileName(string name);
        void ChangeFileFormat(string extension);
    }
}