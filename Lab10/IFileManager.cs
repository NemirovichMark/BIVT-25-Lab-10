namespace Lab10
{
    public interface IFileManager
    {
        string FolderPath { get; }//свойства 
        string FileName { get; }
        string FileExtension { get; }
        string FullPath { get; }

        void SelectFolder(string folderPath);//методы
        void ChangeFileName(string fileName);
        void ChangeFileFormat(string fileExtension);
    }
}
