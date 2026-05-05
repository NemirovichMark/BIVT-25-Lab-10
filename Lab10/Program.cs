using System.Security.Cryptography.X509Certificates;

namespace Lab10
{
    public class Program
    {
        public void Main()
        {


        }
        public abstract class MyFileManager : IFileManager, IFileLifeController
        {

            protected string _fileName;
            protected string _fileExtension;
            protected string _folderPath;
            protected string _fullPath;
            public MyFileManager(string FileName)
            {
                _fileName = FileName;
                _folderPath = "";
                _fileExtension = "txt";
                _fullPath = "";
            }
            public MyFileManager(string FileName, string FolderName, string format = "txt")
            {
                _folderPath = FolderName;
                _fileExtension = format;
                _fileName = FileName;
                _fullPath = _folderPath + "\\" + _fileName + "." + _fileExtension;

            }
            public string FolderPath => _folderPath;
            public string FileName => _fileName;
            public string FileExtension => _fileExtension;
            public string FullPath => _fullPath;

            public void ChangeFileFormat(string format)
            {
                _fileExtension = format;
                _fullPath = _folderPath + "\\" + _fileName + "." + _fileExtension;
            }
            public void ChangeFileName(string name)
            {
                _fileName = name;
                _fullPath = _folderPath + "\\" + _fileName + "." + _fileExtension;
            }
            public void SelectFolder(string path)
            {
                _folderPath = path;
                _fullPath = _folderPath + "\\" + _fileName + "." + _fileExtension;
            }

            public void CreateFile()
            {
                Directory.CreateDirectory(_folderPath);
                File.Create(_fullPath).Close();
            }
            public void DeleteFile()
            {
                File.Delete(_fullPath);
            }
            public void EditFile(string text)
            {
                File.WriteAllText(_fullPath, text);
            }
            public void ChangeFileExtension(string extension)
            {
                try
                {
                    string newFullPath = _folderPath + "\\" + _fileName + "." + extension;
                    File.Create(newFullPath).Close();//закрываем файл, чтобы не было блокировки, а то при попытке записи в него будет ошибка
                    string old_text = File.ReadAllText(_fullPath);
                    File.WriteAllText(newFullPath, old_text);
                    // проверили что все создалось, удаляем старый файл

                    File.Delete(_fullPath);
                    _fileExtension = extension;
                    _fullPath = newFullPath;
                }
                catch { }
                // если что-то пошло не так, то оставляем все как было
            }



        }




    }
}