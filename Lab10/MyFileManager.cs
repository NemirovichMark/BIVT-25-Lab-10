using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Lab10
{
    public abstract class MyFileManager : IFileManager, IFileLifeController
    {
        protected bool IOE(string text)
        {
            return string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text);
        }

        private string _folderPath;
        private string _fileName;
        protected string _fileExtension;

        public string Name { get; private set; }
        public string FolderPath => _folderPath;
        public string FileName => _fileName;
        public string FileExtension => _fileExtension;
        public string FullPath => Path.Combine(_folderPath, _fileName + (!IOE(_fileExtension)?'.' + _fileExtension:""));

        protected JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };

        public MyFileManager(string name) { 
            Name = name;
            _folderPath = "";
            _fileName = "";
            _fileExtension = "";
        }
        public MyFileManager(string name, string fileName, string folderPath, string fileExtension = "") { 
            Name = name;
            _folderPath = folderPath;
            _fileName = fileName;
            _fileExtension = fileExtension;
        }

        public void SelectFolder(string folderPath)
        {
            if(!IOE(folderPath)) _folderPath = folderPath;
        }
        public void ChangeFileName(string name)
        {
            if(!IOE(name)) _fileName = name;
        }
        public void ChangeFileFormat(string format)
        {
            if (IOE(format)) return;
            _fileExtension = format;
            if (!File.Exists(FullPath)) CreateFile();
        }

        public void CreateFile()
        {
            if(!Directory.Exists(FolderPath)) Directory.CreateDirectory(FolderPath);
            if (!File.Exists(FullPath)) File.Create(FullPath).Close();
        }
        public void DeleteFile()
        {
            if (File.Exists(FullPath)) File.Delete(FullPath);
        }
        public virtual void EditFile(string text)
        {
            if (!IOE(text)) File.WriteAllText(FullPath, text);
        }
        public virtual void ChangeFileExtension(string extension)
        {
            if (IOE(extension)) return;
            if (!File.Exists(FullPath)) return;
            if (extension == "txt" || extension == "json")
            {
                var cont = File.ReadAllText(FullPath);
                DeleteFile();
                ChangeFileFormat(extension);
                EditFile(cont);
            }
        }
    }
}
