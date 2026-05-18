namespace Lab10
{
    public abstract class MyFileManager : IFileManager, IFileLifeController
    {
        private string _name;
        private string _folderPath;
        private string _fileName;
        private string _fileExtension;

        public string Name => _name;
        public string FolderPath => _folderPath;
        public string FileName => _fileName;
        public string FileExtension => _fileExtension;

        public string FullPath
        {
            get
            {
                if (_folderPath == null || _fileName == null || _fileExtension == null)
                    return string.Empty;//если что то равнво 0 то возвращаем "" пустую строку
                return Path.Combine(_folderPath, _fileName + "." + _fileExtension);//C:/folder/myfile.txt
            }
        }

        public MyFileManager(string name)
        {
            _name = name ?? string.Empty;//если null то возвращаем пустую строку
            _folderPath = string.Empty;//пока пустое
            _fileName = string.Empty;
            _fileExtension = "txt";
        }

        public MyFileManager(string name, string folderPath, string fileName, string fileExtension = "txt")
        {
            _name = name ?? string.Empty;
            _folderPath = folderPath ?? string.Empty;
            _fileName = fileName ?? string.Empty;
            _fileExtension = string.IsNullOrEmpty(fileExtension) ? "txt" : fileExtension;//если пустое то txt иначе то что передали
        }

        public void SelectFolder(string folderPath)
        {
            if (folderPath == null)
                return;//если ничего то ничего
            _folderPath = folderPath;
        }

        public void ChangeFileName(string fileName)
        {
            if (fileName == null)
                return;
            _fileName = fileName;
        }

        public void ChangeFileFormat(string fileExtension)
        {
            if (string.IsNullOrEmpty(fileExtension))
                return;
            _fileExtension = fileExtension;//если ест расширение то сохраняем его и создаем новый файл с таким разрешением 
            CreateFile();
        }

        public void CreateFile()
        {
            if (string.IsNullOrEmpty(_folderPath) || string.IsNullOrEmpty(_fileName) || string.IsNullOrEmpty(_fileExtension)){
                return;//папка или имя или расширение 
            }
            if (!Directory.Exists(_folderPath)){
                Directory.CreateDirectory(_folderPath);//если папка не сущ то создаем ее 
            }
            var path = FullPath;//полный путь к файлу
            if (!File.Exists(path)){
                File.Create(path).Dispose();//если файла не сущ то создаем его и закрываем чтобы работать
            }
        }

        public void DeleteFile()
        {
            var path = FullPath;
            if (string.IsNullOrEmpty(path))
                return;

            if (File.Exists(path))
                File.Delete(path);//если файл есть удаляем его 
        }

        public virtual void EditFile(string content)//можем переопределить 
        {
            if (content == null)
                return;//берем строку с которой работаем если ее нет то ничего 

            var path = FullPath;
            if (string.IsNullOrEmpty(path))
                return;

            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }
            File.WriteAllText(path, content);//если файла нет то создаем его и записываем если есть то перезаписываем полностью 
        }

        public virtual void ChangeFileExtension(string newExtension)
        {
            if (string.IsNullOrEmpty(newExtension))
                return;//если преедаем новое расширение и оно пустое то ничего 

            var oldPath = FullPath;//старый путь
            string content = string.Empty;//создаем содержимое файла и делаем его пустым 

            if (!string.IsNullOrEmpty(oldPath) && File.Exists(oldPath))//если путь не пусто и файл сущ 
            {
                content = File.ReadAllText(oldPath);//вес текст из старого файла читаем и сохраняем 
                File.Delete(oldPath);//удаляем старый файл 
            }

            _fileExtension = newExtension;//новое расшир

            var newPath = FullPath;//новый полный путь 
            if (string.IsNullOrEmpty(newPath))
                return;

            if (!Directory.Exists(_folderPath))//если папки нет то создаем
                Directory.CreateDirectory(_folderPath);

            File.WriteAllText(newPath, content);//заполняем теми же данными 
        }
    }
}
