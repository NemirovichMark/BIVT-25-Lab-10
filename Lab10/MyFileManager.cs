using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Lab10
{
  public abstract class MyFileManager : IFileManager, IFileLifeController
  {
    public string Name { get; private set; }
    public string FolderPath { get; private set; }
    public string FileName { get; private set; }
    public string FileExtension { get; private set; }
    public string FullPath
    {
      get
      {
        string fullPath = Path.Combine(FolderPath, FileName);
        if (!string.IsNullOrEmpty(FileExtension))
          fullPath += FileExtension;
        return fullPath;
      }
    }

    public MyFileManager(string name)
    {
      Name = name;
      FolderPath = string.Empty;
      FileName = string.Empty;
      FileExtension = string.Empty;
    }
    public MyFileManager(string name, string folder, string file, string extension = "")
    {
      Name = name;
      FolderPath = folder;
      FileName = file;
      if (!string.IsNullOrEmpty(extension))
        FileExtension = extension[0] == '.' ? extension : "." + extension;
      else
        FileExtension = string.Empty;
    }

    // IFileManager
    public void SelectFolder(string path)
    {
      FolderPath = path;
    }
    public void ChangeFileName(string fileName)
    {
      FileName = fileName;
    }
    public void ChangeFileFormat(string format)
    {
      DeleteFile();
      if (string.IsNullOrEmpty(format) || string.IsNullOrWhiteSpace(format)) return;
      FileExtension = format;
      if (!File.Exists(FullPath))
        CreateFile();
    }
    // IFileLifeController
    public void CreateFile()
    {
      string directory = Path.GetDirectoryName(FullPath);
      if (!Directory.Exists(directory))
        Directory.CreateDirectory(directory);
      if (!File.Exists(FullPath))
        File.Create(FullPath).Close();
    }
    public void DeleteFile()
    {
      if (File.Exists(FullPath))
        File.Delete(FullPath);
      if (Directory.Exists(FullPath))
        Directory.Delete(FullPath, true);
    }
    public virtual void EditFile(string countent)
    {
      File.WriteAllText(FullPath, countent);
    }
    public virtual void ChangeFileExtension(string format)
    {
      if (string.IsNullOrWhiteSpace(format)) return;
      if (!File.Exists(FullPath)) return;
      string data = File.ReadAllText(FullPath);
      DeleteFile();
      ChangeFileFormat(format);
      EditFile(data);
    }
  }
}