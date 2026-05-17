namespace Lab10;

public interface IFileLifeController
{
    void CreateFile();

    void EditFile(string text);

    void ChangeFileExtension(string fileExtension);

    void DeleteFile();
}
