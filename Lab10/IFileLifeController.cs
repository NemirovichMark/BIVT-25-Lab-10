namespace Lab10
{
    public interface IFileLifeController //интерфейс обязывает класс реализовать набор мотодов и свойств 
    {
        void CreateFile();//методы
        void DeleteFile();
        void EditFile(string content);
        void ChangeFileExtension(string newExtension);
    }
}
