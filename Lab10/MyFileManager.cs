using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    public abstract class MyFileManager : IFileManager, IFileLifeController
    {
        public string FolderPath { get; private set; }
        public string FileName { get;private set; }
        public string FileExtension { get; private set; }
        public string FullPath { get => Path.Combine(FolderPath, FileName) + "." + FileExtension;}
        public string Name {  get; private set; }


        public MyFileManager(string name)
        {
            Name = name;
        }
        public MyFileManager(string name, string folder, string fileName, string extension = "txt" ) 
        { 
            Name = name;
            FolderPath = folder;
            FileName = fileName;
            FileExtension = extension;
        }

        public virtual void ChangeFileExtension(string fileExtention)
        {
            string content;
            if (File.Exists(FullPath))
            {
                using (TextReader reader = new StreamReader(FullPath))
                {
                    content = reader.ReadToEnd();
                }
                DeleteFile();
                FileExtension = fileExtention;
                CreateFile();
                EditFile(content);
            }
            
        }

        public void ChangeFileFormat(string fileFormat)
        {
            if (File.Exists(FullPath))
                ChangeFileExtension(fileFormat);
            FileExtension = fileFormat;
            if (!File.Exists(FullPath))
                CreateFile();
        }

        public void ChangeFileName(string fileName)
        {
            FileName = fileName;
        }

        public void CreateFile()
        {
            Directory.CreateDirectory(FolderPath);
            File.Create(FullPath).Close();
        }

        public void DeleteFile()
        {
            File.Delete(FullPath);
        }

        public virtual void EditFile(string fileContent)
        {
            using (TextWriter writer = new StreamWriter(FullPath))
            {
                writer.Write(fileContent);
            }
        }

        public void SelectFolder(string folderPath)
        {
            FolderPath = folderPath;
        }
    }
}
