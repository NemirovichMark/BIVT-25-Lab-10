using System;
using System.IO;
using Lab10.Interfaces;

namespace Lab10
{
    public abstract class MyFileManager : IFileManager, IFileLifeController
    {
        private readonly string name;
        private string folder_path = string.Empty;
        private string file_name = string.Empty;
        private string file_extension = string.Empty;

        public string Name => this.name;
        public string FolderPath => this.folder_path;
        public string FileName => this.file_name;
        public string FileExtension => this.file_extension;

        public string FullPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.folder_path) || string.IsNullOrEmpty(this.file_name))
                    return string.Empty;
                if (string.IsNullOrEmpty(this.file_extension))
                    return Path.Combine(this.folder_path, this.file_name);
                return Path.Combine(this.folder_path, $"{this.file_name}.{this.file_extension}");
            }
        }

        public MyFileManager(string name)
        {
            this.name = name;
        }

        public MyFileManager(string name, string folder_path, string file_name, string file_extension = "")
        {
            this.name = name;
            this.folder_path = folder_path;
            this.file_name = file_name;
            this.file_extension = file_extension;
        }

        public void SelectFolder(string folder_path)
        {
            this.folder_path = folder_path;
        }

        public void ChangeFileName(string file_name)
        {
            this.file_name = file_name;
        }

        public void ChangeFileFormat(string file_extension)
        {
            this.file_extension = file_extension;
            if (!string.IsNullOrEmpty(this.FullPath))
                this.CreateFile();
        }

        public virtual void CreateFile()
        {
            if (string.IsNullOrEmpty(this.FullPath))
                return;
            if (!string.IsNullOrEmpty(this.folder_path))
                Directory.CreateDirectory(this.folder_path);
            if (!File.Exists(this.FullPath))
                File.Create(this.FullPath).Close();
        }

        public virtual void DeleteFile()
        {
            if (!string.IsNullOrEmpty(this.FullPath) && File.Exists(this.FullPath))
                File.Delete(this.FullPath);
        }

        public virtual void EditFile(string text)
        {
            if (!string.IsNullOrEmpty(this.FullPath) && File.Exists(this.FullPath))
                File.WriteAllText(this.FullPath, text ?? string.Empty);
        }

        public virtual void ChangeFileExtension(string new_extension)
        {
            if (string.IsNullOrEmpty(this.FullPath))
                return;
            string old_path = this.FullPath;
            this.ChangeFileFormat(new_extension);
            string new_path = this.FullPath;
            if (File.Exists(old_path) && old_path != new_path)
                File.Move(old_path, new_path);
        }
    }
}