namespace Lab10;

public interface IFileLifeController
{
    void CreateFile();
    void DeleteFile();
    void EditFile(string change_file);
    void ChangeFileExtension(string new_extension);
}
