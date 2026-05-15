using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Purple
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
        public string FullPath => Path.Combine(_folderPath, _fileName, _fileExtension);

        public MyFileManager(string name) 
        {
            _name = name;
        }

        public MyFileManager(string name, string folderName, string fileName, string fileExtension = "txt")
        {
            _name = name;
            _folderPath = folderName;
            _fileName = fileName;
            _fileExtension = fileExtension;
        }

        public void SelectFolder(string path) 
        {
            _folderPath = path;
        }

        public void ChangeFileName(string name) 
        {
            _fileName = name;
        }

        public virtual void ChangeFileFormat(string format) 
        {
            string newPath = Path.ChangeExtension(FullPath, format);
            _fileExtension = format;
        }

        public void CreateFile() 
        {
            if (!File.Exists(FullPath))
            {
                File.Create(FullPath).Close();
            }
        }

        public void DeleteFile() 
        {
            if (File.Exists(FullPath))
            {
                File.Delete(FullPath);
            }
        }

        public virtual void EditFile(string content)
        {
            if (File.Exists(FullPath))
            {
                File.WriteAllText(FullPath, content);
            }
        }

        public virtual void ChangeFileExtension(string extension) 
        {
            if (File.Exists(FullPath))
            {
                string newPath = Path.Combine(FolderPath, FileName, extension);

                File.Move(FullPath, newPath);
            }
            _fileExtension = extension;
        }
    }
}
