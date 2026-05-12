namespace Lab10;

public interface IFileLifeController
{
    void CreateFile();
    void DeleteFile();
    
    void EditFile(string newFileName);
    void ChangeFileExtension(string newFileExtension);
}