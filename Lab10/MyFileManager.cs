// using System;
// using System.IO;
// using Lab10;
// namespace Lab10;
// // public interface IFileManager 
// // {
// //     string FolderPath { get; }
// //     string FileName { get; }
// //     string FileExtension { get; }
// //     string FullPath { get; }
// //     void SelectFolder(string path);
// //     void ChangeFileName(string name);
// //     void ChangeFileFormat(string extension);
// // }
// //
// // public interface IFileLifeController 
// // {
// //     void CreateFile();
// //     void DeleteFile();
// //     void EditFile(string content);
// //     void ChangeFileExtension(string newExtension);
// // }
//
// public abstract class MyFileManager : IFileManager, IFileLifeController 
// {
//     private string _folderPath;
//     private string _fileName;
//     private string _fileExtension;
//     private string _name; 
//
//     public MyFileManager(string name) => _name = name;
//     public MyFileManager(string name, string folder, string file, string ext = "") 
//     {
//         _name = name;
//         _folderPath = folder;
//         _fileName = file;
//         _fileExtension = ext;
//     }
//
//     public string FolderPath => _folderPath;
//     public string FileName => _fileName;
//     public string FileExtension => _fileExtension;
//     public string FullPath => Path.Combine(_folderPath ?? "", (_fileName ?? "") + (_fileExtension ?? ""));
//
//     public void SelectFolder(string path) => _folderPath = path;
//     public void ChangeFileName(string name) => _fileName = name;
//     public void ChangeFileFormat(string extension) => _fileExtension = extension;
//
//     public void CreateFile() 
//     {
//         if (!string.IsNullOrEmpty(FolderPath)) Directory.CreateDirectory(FolderPath);
//         File.WriteAllText(FullPath, "");
//     }
//
//     public void DeleteFile() {
//         if (File.Exists(FullPath)) File.Delete(FullPath);
//     }
//
//     public abstract void EditFile(string content);
//     public abstract void ChangeFileExtension(string newExtension);
// }


namespace Lab10;

public abstract class MyFileManager : IFileManager, IFileLifeController
{
    protected string _name;
    protected string _folderPath;
    protected string _fileName;
    protected string _fileExtension;

    // Конструктор
    public MyFileManager(string name)
    {
        _fileName = name;
        _folderPath = System.IO.Directory.GetCurrentDirectory();
        _fileExtension = "";
    }

    public MyFileManager(string name, string folder, string file, string ext)
    {
        _name = name;
        _fileName = file;
        _folderPath = folder;
        _fileExtension = ext;
    }

    // Свойства из интерфейса IFileManager
    public string FolderPath => _folderPath;
    public string FileName => _fileName;
    public string FileExtension => _fileExtension;
    public string FullPath => System.IO.Path.Combine(_folderPath, _fileName + _fileExtension);

    // Реализация методов IFileLifeController
    public void CreateFolder(string path) 
    {
        _folderPath = path;
        if (!System.IO.Directory.Exists(path)) System.IO.Directory.CreateDirectory(path);
    }

    public void SelectFolder(string path) => _folderPath = path;

    public void ChangeFileName(string name) => _fileName = name;

    public void ChangeFileFormat(string extension) 
    {
        _fileExtension = extension.StartsWith(".") ? extension : "." + extension;
    }

    // Эти методы можно оставить пустыми или реализовать базу
    public virtual void CreateFile() { }
    public virtual void DeleteFile() { }
    public virtual void EditFile(string content) { }
    public virtual void ChangeFileExtension(string newExtension) => ChangeFileFormat(newExtension);
}