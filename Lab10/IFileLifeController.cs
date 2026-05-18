namespace Lab10;

public interface IFileLifeController
{
    void CreateFile();
    
    void DeleteFile();
    
    void EditFile(string fileName);
    
    void ChangeFileExtension(string extension);
    
}