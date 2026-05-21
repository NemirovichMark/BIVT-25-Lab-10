using System;
using System.IO;
using System.Linq;

namespace Lab10
{
    public abstract class MyFileManager : IFileManager, IFileLifeController
    {
        //实现两个接口，并且abstract是抽象类，不能直接实例化
        private string _name;
        private string _folderPath;
        private string _fileName;
        private string _fileExtension;

        public string Name => _name;

        public string FolderPath
        {
            get => _folderPath;
            private set => _folderPath = value;//setter只能在内部调用
            //类似，filename和file extension也是可读，私有可写
        }

        public string FileName
        {
            get => _fileName;
            private set => _fileName = value;
        }

        public string FileExtension
        {
            get => _fileExtension;
            private set => _fileExtension = value;
        }

        public string FullPath
        {
            get
            {
                return Path.Combine(_folderPath, _fileName + "." + _fileExtension);
                //智能组合路径（自动处理分隔符)
                
            }
        }

        public MyFileManager(string name)
        {
            _name = name;
            _folderPath = string.Empty;
            _fileName = string.Empty;
            _fileExtension = "txt";
        }

        public MyFileManager(string name, string folderPath, string fileName, string fileExtension = "txt")
        {
            //这个是可选参数，调用时可省略
            _name = name;
            _folderPath = folderPath;
            _fileName = fileName;
            _fileExtension = fileExtension;
        }

        public virtual void SelectFolder(string folderPath)
        {
            if (folderPath != null)//防御
            {
                _folderPath = folderPath;//更新私有字段
            }
        }

        public virtual void ChangeFileName(string fileName)
        {
            if (fileName != null)
            {
                _fileName = fileName;
            }
        }

        public virtual void ChangeFileFormat(string fileExtension)
        {
            if (fileExtension == null)//参数无效时提前退出
                return;

            _fileExtension = fileExtension;//只改扩展名

            CreateFile();//用新格式创造文件
        }

        public virtual void CreateFile()
        {
            if (FullPath == string.Empty)
                return;

            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }

            if (!File.Exists(FullPath))
            {
                File.Create(FullPath).Close();
                //close是必须的，不然文件会被锁定
            }
        }

        public virtual void DeleteFile()
        {
            if (File.Exists(FullPath))
            {
                File.Delete(FullPath);
            }
        }

        public virtual void EditFile(string content)
        {
            File.WriteAllText(FullPath, content);
        }

        public virtual void ChangeFileExtension(string newExtension)
        {
            if (newExtension == null)
                return;

            if (!File.Exists(FullPath))
            {
                _fileExtension = newExtension;
                return;
            }

            string oldPath = FullPath;

            string content = File.ReadAllText(oldPath);

            _fileExtension = newExtension;

            string newPath = FullPath;

            File.WriteAllText(newPath, content);

            File.Delete(oldPath);
        }
    }
}