using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab10
{
    public abstract class MyFileManager : IFileLifeController, IFileManager
    {
        public string Name { get; }
        public string FolderPath { get; private set; }

        public string FileName { get; private set; }

        public string FileExtension {get; private set; }

        public string FullPath
        {
            get
            {
                if (string.IsNullOrEmpty(FolderPath) || string.IsNullOrEmpty(FileName))
                    return string.Empty;

                string extension = string.IsNullOrEmpty(FileExtension) ? "" : $".{FileExtension}";
                return Path.Combine(FolderPath, $"{FileName}{extension}");
            }
        }

        public MyFileManager(string name)
        {
            Name = name;
            FolderPath = string.Empty;
            FileName = string.Empty;
            FileExtension = string.Empty;
        }
        public MyFileManager(string name,string folderPath,string fileName, string fileExtension = "txt")
        {
            Name = name;
            FolderPath = folderPath;
            FileName = fileName;
            FileExtension = fileExtension;
        }
        public virtual void ChangeFileExtension(string newExtention)
        {
            string newFilePath = Path.ChangeExtension(FullPath, newExtention);
            File.Move(FullPath, newFilePath);
            FileExtension = newExtention;
        }

        public void ChangeFileFormat(string format)
        {

            if (string.IsNullOrEmpty(format)) return;

            string oldPath = FullPath;
            string oldExtension = FileExtension;

            if (!File.Exists(oldPath))
            {
                using (File.Create(oldPath)) { }
            }

            FileExtension = format;
            string newPath = FullPath;


            if (File.Exists(oldPath) && oldExtension != format)
            {
                File.Move(oldPath, newPath);
            }
        }
        public void ChangeFileName(string newFileName)
        {
            FileName = newFileName;
        }

        public void CreateFile()
        {
            if (!File.Exists(FullPath))
            {
                using (File.Create(FullPath)) { }
            }
        }

        public void DeleteFile()
        {
            File.Delete(FullPath);
        }

        public virtual void EditFile(string newContent)
        {

            File.WriteAllText(FullPath, newContent);
        }

        public void SelectFolder(string folderPath)
        {
            FolderPath = folderPath;
        }
    }
}
