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
            if (string.IsNullOrEmpty(path) || string.IsNullOrWhiteSpace(path)) return;
            if (!Directory.Exists(path)) return;
            FolderPath = path;
        }

        public void ChangeFileName(string change_name)
        {
            if (string.IsNullOrEmpty(change_name) || string.IsNullOrWhiteSpace(change_name)) return;
            FileName = change_name;
            CreateFile();
        }

        public void ChangeFileFormat(string change_format)
        {
            if (string.IsNullOrEmpty(change_format)) return;
            FileExtension = change_format;
            if (!File.Exists(FullPath))
            {
                CreateFile();
            }
        }

        public void CreateFile()
        {
            string folderpath = Path.GetDirectoryName(FullPath);
            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
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
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(str)) return;
            File.WriteAllText(FullPath, str);
        }

        public virtual void ChangeFileExtension(string change_extension)
        {
            if (!File.Exists(FullPath))
            {
                ChangeFileFormat(change_extension);
                return;
            }
            string str = File.ReadAllText(FullPath);
            DeleteFile();
            ChangeFileFormat(change_extension);
            EditFile(str);
        }
    }
}