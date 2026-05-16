using System.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            FolderPath = "";
            FileName = "";
            FileExtension = "txt";
        }

        public MyFileManager(string name, string folder, string fileName, string extension = "txt")
        {
            Name = name;
            FolderPath = folder;
            FileName = fileName;
            FileExtension = extension;
        }

        public virtual void ChangeFileExtension(string input)
        {
            if (File.Exists(FullPath))
            {
                string content = File.ReadAllText(FullPath);
                File.Delete(FullPath);
                FileExtension = input;
                File.WriteAllText(FullPath, content);
            }
            else
            {
                FileExtension = input;
            }
        }

        public virtual void CreateFile()
        {
            if (!string.IsNullOrEmpty(FolderPath) && !Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }
            if (!File.Exists(FullPath))
            {
                File.Create(FullPath).Close();
            }
        }

        public virtual void DeleteFile()
        {
            if (File.Exists(FullPath))
            {
                File.Delete(FullPath);
            }
        }

        public virtual void EditFile(string input)
        {
            File.WriteAllText(FullPath, input);
        }

        public void ChangeFileFormat(string input)
        {
            FileExtension = input;
            if (!string.IsNullOrEmpty(FolderPath) && !string.IsNullOrEmpty(FileName))
            {
                if (!Directory.Exists(FolderPath)) Directory.CreateDirectory(FolderPath);
                if (!File.Exists(FullPath)) File.Create(FullPath).Close();
            }
        }

        public void ChangeFileName(string input)
        {
            FileName = input;
        }

        public void SelectFolder(string input)
        {
            FolderPath = input;
        }
    }
}
