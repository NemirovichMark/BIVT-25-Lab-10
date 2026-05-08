namespace Lab10.Purple
{
    public abstract class PurpleFileManager<T> : MyFileManager, ISerializer<T> where T : Lab9.Purple.Purple
    {
        public PurpleFileManager(string name) : base(name) { }

        public PurpleFileManager(string name, string folderPath, string fileName, string fileExtension = "txt")
            : base(name, folderPath, fileName, fileExtension) { }

        public override void EditFile(string text)
        {
            base.EditFile(text);
        }

        public override void ChangeFileExtension(string fileExtension)
        {
            base.ChangeFileExtension(fileExtension);
        }

        public abstract void Serialize(T obj);
        public abstract T Deserialize();
    }
}
