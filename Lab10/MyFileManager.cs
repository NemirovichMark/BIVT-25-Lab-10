using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    public abstract class MyFileManager : IFileManager, IFileLifeController
    {
        private string _name;
        private string _folderPath;
        private string _fileName;
        private string _fileExtension;

        public string Name => _name;
        public string FolderPath => _folderPath;

        public string FileName => _fileName;

        public string FileExtension => _fileExtension;

        public string FullPath => Path.Combine(FolderPath, FileName) + "." + _fileExtension;

        public MyFileManager(string name) {
            _name = name;
        }

        public MyFileManager(string name, string folderPath, string fileName, string fileExtension = "txt")
        {
            _name = name;
            _folderPath = folderPath;
            _fileName = fileName;
            _fileExtension = fileExtension;
        }
        

        public void SelectFolder(string folder)
        {
            if (!Directory.Exists(folder)) return;
            _folderPath = folder;
        }

        public void ChangeFileName(string fileName)
        {
            _fileName = fileName;
        }
        public void ChangeFileFormat(string changeFormat)
        {
            _fileExtension = changeFormat;
            if (!File.Exists(FullPath))
            {
                CreateFile();
            }
        }
        public void CreateFile()
        {
            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }

            File.Create(FullPath).Close();
        }

        public void DeleteFile()
        {
            if (File.Exists(FullPath))
                File.Delete(FullPath);
        }

        public virtual void EditFile(string text)
        {
            File.WriteAllText(FullPath, text);
        }

        public virtual void ChangeFileExtension(string fileExtension)
        {
            if (fileExtension != _fileExtension)
            {
                if (File.Exists(FullPath))
                {
                    string newPath = Path.Combine(FolderPath, FileName) + "." + fileExtension;
                    if (File.Exists(newPath))
                    {
                        File.Delete(newPath);
                    }
                    File.Move(FullPath, newPath);

                }
                _fileExtension = fileExtension;
            }
        }


    }
}
