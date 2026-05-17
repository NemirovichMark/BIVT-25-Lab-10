namespace Lab10.White
{
    public abstract class WhiteFileManager : MyFileManager, IWhiteSerializer
    {
        public WhiteFileManager(string name) : base(name)//вызываем родителя с парам нэйм
        {
        }

        public WhiteFileManager(string name, string folderPath, string fileName, string fileExtension = "txt")
            : base(name, folderPath, fileName, fileExtension)
        {
        }

        public abstract void Serialize(Lab9.White.White obj);
        public abstract Lab9.White.White Deserialize();

        public override void EditFile(string content)
        {
            if (content == null)
            {
                return;
            }

            var path = FullPath;//полный путь к файлу
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            if (!string.IsNullOrEmpty(FolderPath) && !Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);//создаем папку если ее нет
            }

            File.WriteAllText(path, content);//записывает текст в файл
        }

        public override void ChangeFileExtension(string newExtension)
        {
            if (string.IsNullOrEmpty(newExtension))
            {
                return;
            }

            base.ChangeFileExtension(newExtension);//вызываем родителя который меняет расширение и сохр
        }
    }
}