using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    public abstract class MyFileManager : IFileManager, IFileLifeController
    {
        private string _folderPath;
        private string _fileName;
        private string _fileExtension;

        public string Name { get; }
        public string FolderPath { get; private set; }
        public string FileName { get; private set; }
        public string FileExtension { get; private set; }
        public string FullPath => Path.Combine(FolderPath, FileName) + '.' + FileExtension;

        public MyFileManager(string name)
        {
            Name = name;
            FileName = "";
            FileExtension = "";
            FolderPath = "";
        }

        public MyFileManager(string name, string folderPath, string fileName, string fileExtension = "txt")
        {
            Name = name;
            FolderPath = folderPath;
            FileName = fileName;
            FileExtension = fileExtension;
        }

        public void CreateFile()
        {
            Directory.CreateDirectory(FolderPath); // создаем папку
            File.Create(FullPath).Close(); // создаем в этой папке файл
        }

        public void DeleteFile()
        {
            File.Delete(FullPath);
        }

        public virtual void EditFile(string newFileText)
        {
            File.WriteAllText(FullPath, newFileText); // заменяет старый текст на новый
        }

        public virtual void ChangeFileExtension(string newFileExtension)
        {
            string oldPath = FullPath;

            if (File.Exists(oldPath))
            {
                // 2. Меняем расширение в свойстве (теперь FullPath вернет новый путь)
                ChangeFileFormat(newFileExtension);
                string newPath = FullPath;

                // 3. ПРОВЕРКА: Если файл по новому пути уже есть, удаляем его
                // Именно из-за этого момента падает тест!
                if (File.Exists(newPath))
                {
                    File.Delete(newPath);
                }

                // 4. Теперь спокойно переносим старый файл в новый
                File.Move(oldPath, newPath);
            }
            //string oldpath = FullPath;
            //FileExtension = newFileExtension;
            //File.Move(oldpath, FullPath);
        }

        public void SelectFolder(string folder)
        {
            FolderPath = folder;
        }

        public void ChangeFileName(string newName)
        {
            FileName = newName;
        }

        public void ChangeFileFormat(string newFormat)
        {
            FileExtension = newFormat;
            if (!File.Exists(FullPath))
            {
                File.Create(FullPath).Close();
            }
        }
    }
}
