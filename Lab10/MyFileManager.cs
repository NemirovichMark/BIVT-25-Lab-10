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
        public string Name => _name;
        public string FolderPath { get; private set; }

        public string FileName { get; private set; }

        public string FileExtension { get; private set; }

        public string FullPath
        {
            get
            {
                return Path.Combine(FolderPath, $"{FileName}.{FileExtension}");
                //string result = FolderPath + FileName + "."+ FileExtension;
                //return result;
            }
        }

        public MyFileManager(string name)
        {
            _name = name; 
        }

        public MyFileManager(string name, string folderName, string fileName, string fileExtension = "txt")
        {
            _name = name;
            FolderPath = folderName;
            FileName = fileName;
            FileExtension = fileExtension;
        }
        public virtual void ChangeFileExtension(string extension)
        {
            string oldPath = FullPath;
            if (string.IsNullOrEmpty(oldPath) || !File.Exists(FullPath)) return;
            string content = File.ReadAllText(FullPath);
            File.Delete(FullPath);
            ChangeFileFormat(extension);
            File.WriteAllText(FullPath, content);
        }

        public void ChangeFileFormat(string format)
        {
            FileExtension = format ?? "";
            string path = FullPath;
            if (string.IsNullOrEmpty(path)) return;
            string dir = Path.GetDirectoryName(FullPath);
            if (!string.IsNullOrEmpty(dir))
            {
                Directory.CreateDirectory(dir);
            }
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }

        }

        public void ChangeFileName(string name)
        {
            FileName = name ?? "";
        }

        public virtual void CreateFile()
        {
            string? directory = Path.GetDirectoryName(FullPath);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }
            using (File.Create(FullPath));
        }

        public virtual void DeleteFile()
        {
            File.Delete(FullPath);
        }

        public virtual void EditFile(string input)
        {
            File.WriteAllText(FullPath, input);
        }

        public void SelectFolder(string path)
        {
            FolderPath = path;
        }
    }
}
