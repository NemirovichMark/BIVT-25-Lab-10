namespace Lab10.Blue
{
    public abstract class BlueFileManager<T> : MyFileManager, ISerializer<T>
        where T : Lab9.Blue.Blue
    {
        public BlueFileManager(string name) : base(name)
        {
        }

        public BlueFileManager(string name, string folderPath, string fileName, string fileExtension = "txt")
            : base(name, folderPath, fileName, fileExtension)
        {
        }

        public override void EditFile(string text)
        {
            if (FullPath == null || FullPath == string.Empty)
            {
                return;
            }

            base.EditFile(text);
        }

        public override void ChangeFileExtension(string fileExtension)
        {
            if (FullPath == null || FullPath == string.Empty)
            {
                return;
            }

            base.ChangeFileExtension(fileExtension);
        }

        public abstract void Serialize(T obj);

        public abstract T Deserialize();
    }
}
