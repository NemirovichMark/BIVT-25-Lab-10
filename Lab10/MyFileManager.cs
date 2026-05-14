using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Lab10
{
    public abstract class MyFileManager : IFileLifeController, IFileManager
    {

        private string _folderPath;
        private string _fileName;
        private string _fileExtension;
        // private string _fullpath;
        public string Name { get; protected set; }
        public string FolderPath => _folderPath;// папка
        public string FileName => _fileName;// имя файла
        public string FileExtension => _fileExtension;// расширение файла 
        // FullPath должно возвращать полный путь к файлу: путь к папке, имя файла и расширение файла.
        public string FullPath => Path.Combine(FolderPath, FileName) + "." + FileExtension;




        public MyFileManager(string name)
        {
            Name = name;
            _folderPath = "";
            _fileName = "";
            _fileExtension = "";

        }
        public MyFileManager(string name, string folderpath, string filename, string fileExtension = "txt")
        {
            Name = name;
            _folderPath = folderpath;
            _fileName = filename;
            _fileExtension = fileExtension;


        }


        public void CreateFile()
        {
            // создаем папку если она не создана еще 
            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }
            // создаем в жтой папке файл 
            if (!File.Exists(FullPath))
            {
                File.Create(FullPath).Close();
            }
        }
        public void DeleteFile()
        {
            // удаляем файл по пути FullPath(удаляем папку)
            if (File.Exists(FullPath))
            {
                File.Delete(FullPath); // удаляем файл
            }
        }
        public virtual void EditFile(string file)
        {
            if (File.Exists(FullPath))
            {
                File.WriteAllText(FullPath, file); // заменяет старый текст на новый ( создает или перезаписывает )
            }
        }


        public virtual void ChangeFileExtension(string file) // заменить расширение файла 
        {
            if (File.Exists(FullPath))
            {
                string file2 = Path.Combine(FolderPath, FileName) + "." + file;
                File.Move(FullPath, file2); // меняем путь у старого на новый (если бы меняли папку то перенеслось бы в другую папку) 
                                            // CreateFile();
            }
            _fileExtension = file; // заменили расширение у метода

        }

        public void SelectFolder(string folder)
        {
            _folderPath = folder;
        }
        public void ChangeFileName(string name)
        {
            _fileName = name;
        }
        public void ChangeFileFormat(string format)
        {
            _fileExtension = format;
            ChangeFileExtension(format);
            if (!File.Exists(FullPath))
            {
                CreateFile();
            }
        }

    }
}
