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
        public string FolderPath
        {
            get => _folderPath;
            private set => _folderPath = value;
        }
        public string FileName => _fileName;
        public string FileExtension => _fileExtension;
        public string FullPath => Path.Combine(_folderPath, _fileName) + "." + _fileExtension;

        public MyFileManager(string name)
        {
            _name = name;
            _folderPath = "";
            _fileName = "";
            _fileExtension = "";
        }

        public MyFileManager(string name, string folderpath, string filename, string fileextension = "txt")
        {
            _name = name;
            _folderPath = folderpath;
            _fileName = filename;
            _fileExtension = fileextension;
        }

        public void SelectFolder(string folder)
        {
            if (folder == null || folder == "") return;
            if (!Directory.Exists(folder)) return;
            _folderPath = folder;
        }

        public void ChangeFileName(string name)
        {
            if (name == null || name == "") return;
            _fileName = name;
        }

        public void ChangeFileFormat(string format)
        {
            if (format == null || format == "") return;
            _fileExtension = format;
            if (!File.Exists(FullPath))
            {
                CreateFile();
            }
        }

        public void CreateFile()
        {
            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }

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

        public virtual void EditFile(string text)
        {
            if (text == null || text == "") return;
            if (!File.Exists(FullPath)) return;
            File.WriteAllText(FullPath, text);
        }

        public virtual void ChangeFileExtension(string extension)
        {
            if (extension == null || extension == "") return;
            if (!File.Exists(FullPath)) return;

            string text = File.ReadAllText(FullPath);
            DeleteFile();
            ChangeFileFormat(extension);
            EditFile(text);
        }
    }
}
