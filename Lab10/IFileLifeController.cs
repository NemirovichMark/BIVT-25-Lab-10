namespace Lab10;

public interface IFileLifeController
{
  public void CreateFile();
  public void DeleteFile();

  public void EditFile(string data);
  public void ChangeFileExtension(string ext);

}
