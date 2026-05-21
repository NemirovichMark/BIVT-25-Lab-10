namespace Lab10.Blue
{
    public abstract class BlueFileManager<T> : MyFileManager, ISerializer<T> where T : Lab9.Blue.Blue
    {
        public BlueFileManager(string name) : base(name)
        {
        }

        public BlueFileManager(string name, string folderPath, string fileName, string fileExtension = "txt")
            : base(name, folderPath, fileName, fileExtension)
        {
        }

        public abstract T? Deserialize();
        public abstract void Serialize(T obj);

        public override void EditFile(string content)
        {
            if (string.IsNullOrWhiteSpace(FullPath))
            {
                return;
            }

            base.EditFile(content);
        }

        public override void ChangeFileExtension(string fileExtension)
        {
            if (string.IsNullOrWhiteSpace(FullPath))
            {
                ChangeFileFormat(fileExtension);
                return;
            }

            base.ChangeFileExtension(fileExtension);
        }
    }
}
