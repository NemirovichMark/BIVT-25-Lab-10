namespace Lab10;

public interface IFileLifeController
{
    void CreateFile();
    void DeleteFile();
    void EditFile(string file);
    void ChangeFileExtension(string extension);
}