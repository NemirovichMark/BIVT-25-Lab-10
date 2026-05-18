namespace Lab10.Blue
{
    public abstract class BlueFileManager<T> : MyFileManager, ISerializer<T> where T : Lab9.Blue.Blue
    { 
        public BlueFileManager(string name) : base(name)
        {
        }
    
        public BlueFileManager(string name, string folderPath, string fileName, string fileExtension = "") : 
            base(name, folderPath, fileName, fileExtension)
        {
        }

        public override void EditFile(string file)
        {
            if (string.IsNullOrEmpty(FullPath) || !File.Exists(FullPath)) return;
            base.EditFile(file);
        }

        public override void ChangeFileExtension(string extension)
        {
            if (string.IsNullOrEmpty(FullPath) || !File.Exists(FullPath)) return;
            base.ChangeFileExtension(extension);
        }
        
        public abstract void Serialize(T obj);
        
        public abstract T Deserialize();
    }
}