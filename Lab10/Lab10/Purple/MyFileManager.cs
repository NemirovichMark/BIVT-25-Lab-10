using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Purple
{
    public abstract class MyFileManager : IFileManager, IFileLifeController
    {
        private string _name; private string _folderPath;
        private string _filename; private string _fileExtension;
        public string Name => _name;
        public MyFileManager(string name)
        { 
            _name = name; _folderPath = ""; _filename = ""; _fileExtension = "";
        }
        public MyFileManager(string name, string folderPath, string filename, string fileExtension="")
        {
            _name = name ?? ""; _folderPath = folderPath ?? ""; _filename=filename ?? "";
        }
        public string FolderPath => _folderPath; // название папки
        public string FileName => _filename;    // название файла 
        public string FileExtension => _fileExtension;  // параметр для расширения файла
        public string FullPath => Path.Combine(_folderPath, _filename + _fileExtension);
        public void ChangeFileFormat(string a)
        {
            if (a == null) return;
            _fileExtension = a;
        }
        public void ChangeFileName(string a)
        {
            if (a == null) return;
            _filename = a;
        }
        public void SelectFolder(string a)
        {
            if (a == null) return;
            _folderPath = a;
        }
        public void CreateFile()
        {
            string d = Path.GetDirectoryName(FullPath);
            if (!string.IsNullOrEmpty(FullPath) && ! Directory.Exists(FullPath))
            Directory.CreateDirectory(d);
            if (!File.Exists(FullPath))
                File.Create(FullPath).Close();
        }
        public void DeleteFile()
        {
            if (File.Exists(FullPath))
                File.Delete(FullPath);
        }
        public virtual void EditFile(string a)
        {
            if (File.Exists(FullPath))
                File.WriteAllText(FullPath, a);
        }
        public virtual void ChangeFileExtension(string a)
        {
            string lPath = FullPath;
            ChangeFileExtension(a);
            string nPath=FullPath;
            if (File.Exists(lPath)) File.Move(lPath, nPath);            
        }
    }
}
