using System;
using System.IO;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Lab10
{
    public abstract class MyFileManager : IFileManager, IFileLifeController
    {
        public string Name { get; private set; }
        public string FolderPath { get; private set; }
        public string FileName { get; private set; }
        public string FileExtension { get; private set; }

        public string FullPath => Path.Combine(FolderPath, $"{FileName}.{FileExtension}");

        public MyFileManager(string name)
        {
            Name = name;
            FolderPath = string.Empty;
            FileExtension = string.Empty;
            FileName = string.Empty;
        }

        public MyFileManager(string name, string foldername, string filename, string extension = "txt")
        {
            Name = name;
            FolderPath = foldername;
            FileName = filename;
            FileExtension = extension;
        }

        public void SelectFolder(string path)
        {
            FolderPath = path;
        }

        public void ChangeFileName(string change_name)
        {
            FileName = change_name;

        }

        public void ChangeFileFormat(string change_format)
        {
            DeleteFile();
            FileExtension = change_format;
            CreateFile();

        }

        public void CreateFile()
        {
            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }
            if (!File.Exists(FullPath))
            {
                File.Create(FullPath).Close();
            }
        }

        public void DeleteFile()
        {
            if (!File.Exists(FullPath)) return;
            File.Delete(FullPath);
        }

        public virtual void EditFile(string str)
        {
            File.WriteAllText(FullPath, str);
        }

        public virtual void ChangeFileExtension(string change_extension)
        {
            string str = File.ReadAllText(FullPath);
            ChangeFileFormat(change_extension);
            EditFile(str);
        }
    }
}