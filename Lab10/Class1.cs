using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    public abstract class MyFileManager : IFileManager, IFileLifeController
    {
        private string _name;
        private string _folder;
        private string _filename;
        private string _fileextension;


        public string FolderPath
        {
            get => _folder;
            private set => _folder = value;
        }
        public string FileExtension => _fileextension;
        public string Name => _name;
        public string FileName => _filename;
        

        public string FullPath => Path.Combine(_folder, $"{_filename}.{_fileextension}");

        public MyFileManager(string name)
        {
            _name = name ?? "nl";
            _folder ="nl";
            _filename = "nl";
            _fileextension = "nl";
        }
        public MyFileManager(string name, string folder, string filename,string extension = "txt")
        {
            _name = name ?? "nl";
            _folder = folder ?? "nl";
            _filename = filename ?? "nl";
            _fileextension = extension ?? "nl";
        }

        public void CreateFile()
        {
            if (!FullPath.Contains("nl"))
            {
                if (_folder != "" && !Directory.Exists(_folder))
                    Directory.CreateDirectory(_folder);

                File.Create(FullPath).Close();
            }
        }
        public void DeleteFile()
        {
            if (!FullPath.Contains("nl"))
            {
                if (File.Exists(FullPath))
                    File.Delete(FullPath);
            }
        }
        public virtual void ChangeFileExtension(string newExtension)
        {
            string oldPath = FullPath;
            _fileextension = newExtension ?? "nl";
            if (!oldPath.Contains("nl"))
            {
                if (_folder != "" && !Directory.Exists(_folder))
                    Directory.CreateDirectory(_folder);

                if (File.Exists(FullPath))
                    File.Delete(FullPath);

                if (File.Exists(oldPath))
                    File.Move(oldPath, FullPath);
            }


        }
        public virtual void EditFile(string text)
        {
            if (!FullPath.Contains("nl"))
                File.WriteAllText(FullPath, text ?? "nl");
        }

        public void SelectFolder(string folder)
        {

            FolderPath = folder ?? "nl";
        }
        public void ChangeFileName(string filename)
        {
            _filename = filename ?? "nl";
        }
        public void ChangeFileFormat(string newExtension)
        {
            _fileextension = newExtension ?? "nl";
            if (!FullPath.Contains("nl"))
            {
                if (_folder != "" && !Directory.Exists(_folder))
                    Directory.CreateDirectory(_folder);

                if (!File.Exists(FullPath))
                    File.Create(FullPath).Close();
            }
        }
    }
}
