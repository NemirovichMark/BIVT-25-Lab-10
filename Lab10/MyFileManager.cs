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
        public string FullPath => Path.Combine(FolderPath ?? "", $"{FileName ?? ""}.{FileExtension ?? ""}");

        
        public MyFileManager(string name)
        {
            _name = name ?? "";
            _folderPath = "";
            _fileName = "";
            _fileExtension = "txt";
        }

        public MyFileManager(string name, string folderPath, string fileName, string extension = "txt")
        {
            _name = name ?? "";
            _folderPath = folderPath ?? "";
            _fileName = fileName ?? "";
            _fileExtension = extension ?? "txt";
        }

        public void SelectFolder(string path) { if (path == null) return; _folderPath = path; }
        public void ChangeFileName(string name) { if (name == null) return; _fileName = name; }

        
        public void ChangeFileFormat(string format)
        {
            if (format == null) return;
            _fileExtension = format;
            CreateFile();
        }

        public virtual void CreateFile()
        {
            if (string.IsNullOrEmpty(FullPath)) return;
            string dir = Path.GetDirectoryName(FullPath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            if (!File.Exists(FullPath))
                File.Create(FullPath).Close();
        }
        public virtual void DeleteFile()
        {
            if (!string.IsNullOrEmpty(FullPath) && File.Exists(FullPath))
                File.Delete(FullPath);
        }

        public virtual void EditFile(string content)
        {
            if (string.IsNullOrEmpty(FullPath)) return;
            CreateFile();
            File.WriteAllText(FullPath, content ?? "");
        }

        public virtual void ChangeFileExtension(string newExtension)
        {
            if (newExtension == null) return;
            if (!string.IsNullOrEmpty(FullPath) && File.Exists(FullPath))
            {
                string oldPath = FullPath;
                string content = File.ReadAllText(oldPath);
                _fileExtension = newExtension; 
                File.WriteAllText(FullPath, content);
                if (oldPath != FullPath && File.Exists(oldPath))
                    File.Delete(oldPath);
            }
            else
            {
                _fileExtension = newExtension;
            }
        }


    }
}
