namespace Lab10
{
    public interface IFileManager
    {
        string FolderPath
        {
            get; 
        }

        string FileName
        {
            get; 
        }

        string FileExtension
        {
            get; 
        }

        string FullPath
        {
            get; 
        }
        
        void SelectFolder(string path);
        void ChangeFileName(string name);
        void ChangeFileFormat(string extension);
    }
}
