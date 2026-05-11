namespace Lab10
{
    public interface IFileLifeController
    {
         void SelectFolder(string folderName);
         void CreateFile();
         void DeleteFile();
         void EditFile(string content);
         void ChangeFileExtension(string newExtension);
         void CreateFolder(string folderName);
    
        // void CreateFile();
        // void DeleteFile();
        // void RenameFile(string path);
    }
}